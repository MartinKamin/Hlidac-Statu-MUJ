﻿using System.Globalization;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using CsvHelper;
using HlidacStatu.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Serilog;
using Serilog.Events;
using MemoryStream = System.IO.MemoryStream;
using StreamReader = System.IO.StreamReader;

class Program
{
    private static AsyncRetryPolicy<HttpResponseMessage> _retryPolicy = HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(5, _ => TimeSpan.FromSeconds(1));


    public static async Task Main(string[] args)
    {
        #region init

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Is(LogEventLevel.Debug)
            .WriteTo.Console()
            //.AddLogStash(new Uri(Global.LogStashUrl))
            .Enrich.WithProperty("hostname", Environment.MachineName)
            .Enrich.WithProperty("codeversion",
                System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version?.ToString())
            .Enrich.WithProperty("application_name", "VolbyDownloader")
            .CreateLogger();

        var logger = Log.ForContext<Program>();

        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false)
            .Build();
        Devmasters.Config.Init(configuration); // for db
        CultureInfo.DefaultThreadCurrentCulture = HlidacStatu.Util.Consts.czCulture;
        CultureInfo.DefaultThreadCurrentUICulture = HlidacStatu.Util.Consts.csCulture;

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // for csv encoding

        string ovmUrl = "https://www.czechpoint.cz/spravadat/ovm/datafile.do?format=xml&service=seznamovm";
        string cuzkUrl = "https://services.cuzk.cz/sestavy/VO/";

        using var httpClient = new HttpClient();

        #endregion

        //todo: jak to bude s mazáním dat před downloadem??        
        
        // nechci je spouštět najednou, protože habruje připojení k sql :(
        await DownloadOvmsAsync(httpClient, ovmUrl, logger);
        await DownloadCuzkAsync(httpClient, cuzkUrl, logger);

        await FixAddresses(logger);

        Console.WriteLine("Done");
    }

    private static async Task FixAddresses(ILogger logger)
    {
        logger.Information("Updating incorrect address references");
        using (var db = new DbEntities())
        {
            //doplnění
            await db.Database.ExecuteSqlRawAsync("update dbo.OrganVerejneMoci set AdresaOvmId = 75701855 where Zkratka = 'Podvihov'");
            await db.Database.ExecuteSqlRawAsync("update dbo.OrganVerejneMoci set AdresaOvmId = 3134717 where Zkratka = 'Hrabova'");
            await db.Database.ExecuteSqlRawAsync("update dbo.OrganVerejneMoci set AdresaOvmId = 7530200 where Zkratka = 'PrdbceVIII'");
            await db.Database.ExecuteSqlRawAsync("update dbo.OrganVerejneMoci set AdresaOvmId = 9401008 where Zkratka = 'Bonkov'");
            await db.Database.ExecuteSqlRawAsync("update dbo.OrganVerejneMoci set AdresaOvmId = 440434 where Zkratka = 'Bosovice'");
            await db.Database.ExecuteSqlRawAsync("update dbo.OrganVerejneMoci set AdresaOvmId = 15675912 where Zkratka = 'Svojsin'");
            await db.Database.ExecuteSqlRawAsync("update dbo.OrganVerejneMoci set AdresaOvmId = 5265967 where Zkratka = 'KrznviceCh'");
            await db.Database.ExecuteSqlRawAsync("update dbo.OrganVerejneMoci set AdresaOvmId = 16729668 where Zkratka = 'LukavecLi'");
            await db.Database.ExecuteSqlRawAsync("update dbo.OrganVerejneMoci set AdresaOvmId = 18026834 where Zkratka = 'Lukova'");
            await db.Database.ExecuteSqlRawAsync("update dbo.OrganVerejneMoci set AdresaOvmId = 18652255 where Zkratka = 'Mastnik'");
            await db.Database.ExecuteSqlRawAsync("update dbo.OrganVerejneMoci set AdresaOvmId = 6661220 where Zkratka = 'Pohnani'");
            await db.Database.ExecuteSqlRawAsync("update dbo.OrganVerejneMoci set AdresaOvmId = 3766217 where Zkratka = 'ChlmKrhvce'");
            await db.Database.ExecuteSqlRawAsync("update dbo.OrganVerejneMoci set AdresaOvmId = 18201831 where Zkratka = 'Charovice'");
            await db.Database.ExecuteSqlRawAsync("update dbo.OrganVerejneMoci set AdresaOvmId = 14116707 where Zkratka = 'HorniLomna'");
            await db.Database.ExecuteSqlRawAsync("update dbo.OrganVerejneMoci set AdresaOvmId = 27506908 where Zkratka = 'JrsvNzrkou'");
            await db.Database.ExecuteSqlRawAsync("update dbo.OrganVerejneMoci set AdresaOvmId = 12105287 where Zkratka = 'JlvDrzkova'");
            //oprava
            await db.Database.ExecuteSqlRawAsync("update dbo.OrganVerejneMoci set AdresaOvmId = 83068392 where Zkratka = 'Pesvice'");
            await db.Database.ExecuteSqlRawAsync("update dbo.OrganVerejneMoci set AdresaOvmId = 79245129 where Zkratka = 'Veprova'");
            await db.Database.ExecuteSqlRawAsync("update dbo.OrganVerejneMoci set AdresaOvmId = 5982341 where Zkratka = 'Kutrovice'");
        }
    }

    public static async Task DownloadCuzkAsync(HttpClient httpClient, string cuzkUrl, ILogger logger)
    {
        using var responseMessage = await _retryPolicy.ExecuteAsync(() => httpClient.GetAsync(cuzkUrl));

        if (!responseMessage.IsSuccessStatusCode)
        {
            logger.Error(
                "Error during {methodName}. Server responded with [{statusCode}] status code. Reason phrase [{reasonPhrase}].",
                nameof(DownloadCuzkAsync), responseMessage.StatusCode, responseMessage.ReasonPhrase);
            throw new Exception($"Can't download data from {cuzkUrl}");
        }

        var page = await responseMessage.Content.ReadAsStringAsync();
        var matchGroupName = "Url";
        var pattern = @$"href=""(?<{matchGroupName}>https://services.cuzk.cz/sestavy/VO/\d*.zip)""";

        Regex regex = new Regex(pattern, RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        var matches = regex.Matches(page);

        var culture = CultureInfo.GetCultureInfo("cs");
        var encoding = Encoding.GetEncoding(1250);
        string delimiter = ";";


        // následující zakomentovaná sekce nefungovala, kvůli padajícímu poolu (timeout) na SQL serveru. 
        // var semaphoreSlim = new SemaphoreSlim(1, 1);
        // List<Task> downloadTasks = new();
        foreach (Match match in matches)
        {
            var url = match.Groups[matchGroupName].Value;

            // await semaphoreSlim.WaitAsync();
            await DownloadSestavaAsync(httpClient, url, culture, encoding, delimiter, logger);
            // try
            // {
            //     downloadTasks.Add(DownloadSestavaAsync(httpClient, url, culture, encoding, delimiter));
            // }
            // finally
            // {
            //     semaphoreSlim.Release();
            // }
        }

        // await Task.WhenAll(downloadTasks.ToArray());
    }

    private static async Task DownloadSestavaAsync(HttpClient httpClient, string url, CultureInfo culture,
        Encoding encoding, string delimiter, ILogger logger)
    {
        logger.Information("Downloading {url}", url);
        using var responseMessage = await _retryPolicy.ExecuteAsync(() => httpClient.GetAsync(url));

        if (!responseMessage.IsSuccessStatusCode)
        {
            logger.Error(
                "Error during {methodName}. Server responded with [{statusCode}] status code. Reason phrase [{reasonPhrase}].",
                nameof(DownloadSestavaAsync), responseMessage.StatusCode, responseMessage.ReasonPhrase);
            throw new Exception($"Can't download data from {url}");
        }

        var stream = await responseMessage.Content.ReadAsStreamAsync();

        using var zipArchive = new ZipArchive(stream, ZipArchiveMode.Read);
        var csvEntries = zipArchive.Entries.Where(e =>
            e.FullName.EndsWith(".csv", StringComparison.InvariantCultureIgnoreCase));
        foreach (var csvEntry in csvEntries)
        {
            var entryStream = csvEntry.Open();

            var csvConfiguration = new CsvHelper.Configuration.CsvConfiguration(culture)
            {
                HasHeaderRecord = true,
                Delimiter = delimiter
            };

            using (var reader = new StreamReader(entryStream, encoding))
            using (var csv = new CsvReader(reader, csvConfiguration))
            {
                var records = csv.GetRecords<AdresniMisto>();

                var chunks = records.Chunk(100);
                foreach (var chunk in chunks)
                {
                    using (var db = new DbEntities())
                    {
                        db.AdresniMisto.AddRange(chunk);

                        try
                        {
                            await db.SaveChangesAsync();
                        }
                        catch (Exception e)
                        {
                            logger.Error(e, "problém při zápisu dat z {url}, entryname={entryName} ", url,
                                csvEntry.FullName);
                            throw;
                        }
                    }
                }
            }
        }
    }

    public static async Task DownloadOvmsAsync(HttpClient httpClient, string ovmUrl, ILogger logger)
    {
        using var responseMessage = await _retryPolicy.ExecuteAsync(() => httpClient.GetAsync(ovmUrl));

        if (!responseMessage.IsSuccessStatusCode)
        {
            logger.Error(
                "Error during {methodName}. Server responded with [{statusCode}] status code. Reason phrase [{reasonPhrase}].",
                nameof(DownloadOvmsAsync), responseMessage.StatusCode, responseMessage.ReasonPhrase);
            throw new Exception($"Can't download data from {ovmUrl}");
        }

        var stream = await responseMessage.Content.ReadAsStreamAsync();
        using (var gZipStream = new GZipStream(stream, CompressionMode.Decompress))
        using (var decompressedStream = new MemoryStream())
        {
            await gZipStream.CopyToAsync(decompressedStream);
            decompressedStream.Position = 0;
            using var text = new XmlTextReader(decompressedStream);
            text.Namespaces = false;


            XmlSerializer serializer = new XmlSerializer(typeof(SeznamOvmIndex));
            var test = (SeznamOvmIndex)serializer.Deserialize(text);

            Dictionary<int, TypOvm> typyOvm = new();
            Dictionary<string, PravniFormaOvm> pfOvm = new();
            Dictionary<int, AdresaOvm> adresyOvm = new();

            using (var db = new DbEntities())
            {
                foreach (var ovm in test.Subjekt)
                {
                    // replace with correct unique record
                    if (typyOvm.TryAdd(ovm.TypOvm.Id, ovm.TypOvm) == false)
                        ovm.TypOvm = typyOvm[ovm.TypOvm.Id];

                    if (pfOvm.TryAdd(ovm.PravniFormaOvm.Id, ovm.PravniFormaOvm) == false)
                        ovm.PravniFormaOvm = pfOvm[ovm.PravniFormaOvm.Id];

                    if (adresyOvm.TryAdd(ovm.AdresaOvm.Id, ovm.AdresaOvm) == false)
                        ovm.AdresaOvm = adresyOvm[ovm.AdresaOvm.Id];


                    db.OrganVerejneMoci.Add(ovm);
                }

                await db.SaveChangesAsync();
            }
   
        }
    }
}