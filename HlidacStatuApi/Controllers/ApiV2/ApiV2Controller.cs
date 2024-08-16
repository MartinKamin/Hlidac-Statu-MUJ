﻿using System.ComponentModel;
using System.Net;
using HlidacStatu.Repositories;
using HlidacStatuApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HlidacStatuApi.Controllers.ApiV2
{
    [SwaggerTag("Core")]
    [ApiController]
    [Route("api/v2")]
    public class ApiV2Controller : ControllerBase
    {
        public const int DefaultResultPageSize = 25;
        public const int MaxResultsFromES = 5000;

        private readonly Serilog.ILogger _logger = Serilog.Log.ForContext<ApiV2Controller>();
        /*
        Atributy pro API
        [SwaggerOperation(Tags = new[] { "Beta" })] - zarazeni metody do jine skupiny metod, pouze na urovni methody
        [ApiExplorerSettings(IgnoreApi = true)] - neni videt v dokumentaci, ani ve swagger file
        [SwaggerTag("Core")] - Tag pro vsechny metody v controller
        */


        // /api/v2/{id}
        [Authorize]
        [HttpGet("ping/{text}")]
        public ActionResult<string> Ping([FromRoute] string text)
        {
            return "pong " + text;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("tracking")]
        public async Task<ActionResult> Tracking([FromBody] TrackingData data)
        {
            //possible incomming objects:
            // object 1
            // trackingData.selectedValue = value.split('÷')[0];
            // trackingData.lastQuery = autocompleteLastQuery; 
            // trackingData.source = window.location.href;
            // trackingData.type = 'partialAutocomplete'

            // object 2
            // trackingData.selectedValues = selectedValues;
            // trackingData.source = window.location.href;
            // trackingData.type = 'fullAutocomplete'

            _logger.Information("Tracking search request from: {source}, " +
                                "caller type: {type}, " +
                                "selected values: {selectedValues}, " +
                                "selected value: {selectedValue}, " +
                                "last query: {lastQuery}",
                data.Source,
                data.Type,
                data.SelectedValues,
                data.SelectedValue,
                data.LastQuery);

            return StatusCode(200);
        }

        //[ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]
        [HttpGet("getexception")]
        public ActionResult<string> GetError(string exceptionText = null)
        {
            exceptionText = exceptionText ?? "test autogenerated API exception";
            throw new ApplicationException(exceptionText);
            return StatusCode(500, $"error {exceptionText}");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]
        [HttpGet("geterror/{id?}")]
        public ActionResult<string> GetError([FromRoute] int? id = 200, [FromQuery] long waitSec = 0)
        {
            if (waitSec > 0)
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(waitSec));
            return StatusCode(id ?? 200, $"error {id}");
        }

        //[ApiExplorerSettings(IgnoreApi = true)]
        [Authorize]
        [HttpGet("getmyip")]
        public ActionResult<string> GetIp()
        {
            return HlidacStatu.Util.RealIpAddress.GetIp(HttpContext)?.ToString();
        }

        [Authorize]
        [HttpGet("dumps")]
        public ActionResult<DumpInfoModel[]> Dumps()
        {
            return GetDumps();
        }


        [Authorize]
        [HttpGet("dump/{datatype}")]
        public ActionResult<HttpResponseMessage> Dump([FromRoute] string datatype)
        {
            return Dump(datatype, "");
        }

        [Authorize]
        [HttpGet("dump/{datatype}/{date?}")]
        public ActionResult<HttpResponseMessage> Dump([FromRoute] string datatype,
            [FromRoute(Name = "date")] [DefaultValue("")] string? date = "null")
        {
            if (datatype.Contains("..") || datatype.Contains(Path.DirectorySeparatorChar))
            {
                _logger.Error("Wrong datatype name");
                return StatusCode(466);
            }

            DateTime? specificDate = Devmasters.DT.Util.ToDateTime(date, "yyyy-MM-dd");
            string onlyfile = $"{datatype}.dump" +
                              (specificDate.HasValue ? "-" + specificDate.Value.ToString("yyyy-MM-dd") : "");
            string fn = StaticData.Dumps_Path + $"{onlyfile}" + ".zip";

            if (System.IO.File.Exists(fn))
            {
                try
                {
                    return File(System.IO.File.ReadAllBytes(fn), "application/zip", Path.GetFileName(fn), true);
                }
                catch (Exception e)
                {
                    _logger.Error(e, "DUMP exception?" + date);
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }
            }
            else
            {
                _logger.Error("API DUMP : not found file " + fn);
                return NotFound($"Dump {datatype} for date:{date} nenalezen.");
            }
        }

        private static DumpInfoModel[] GetDumps()
        {
            string baseUrl = "https://api.hlidacstatu.cz/api/v2/";

            List<DumpInfoModel> data = new List<DumpInfoModel>();

            foreach (var fi in new DirectoryInfo(StaticData.Dumps_Path).GetFiles("*.zip"))
            {
                var fn = fi.Name;
                var regexStr = @"((?<type>(\w*))? \.)? (?<name>(\w|-)*)\.dump -? (?<date>\d{4} - \d{2} - \d{2})?.zip";
                DateTime? date =
                    Devmasters.DT.Util.ToDateTimeFromCode(
                        Devmasters.RegexUtil.GetRegexGroupValue(fn, regexStr, "date"));
                string name = Devmasters.RegexUtil.GetRegexGroupValue(fn, regexStr, "name");
                string dtype = Devmasters.RegexUtil.GetRegexGroupValue(fn, regexStr, "type");
                if (!string.IsNullOrEmpty(dtype))
                    name = dtype + "." + name;
                data.Add(
                    new DumpInfoModel()
                    {
                        url = baseUrl + $"dump/{name}/{date?.ToString("yyyy-MM-dd") ?? ""}",
                        created = fi.LastWriteTimeUtc,
                        date = date,
                        fulldump = date.HasValue == false,
                        size = fi.Length,
                        dataType = name
                    }
                );
                ;
            }

            return data.ToArray();
        }
    }

    public class TrackingData
    {
        public string[]? SelectedValues { get; set; }
        public string? SelectedValue { get; set; }
        public string? LastQuery { get; set; }
        public string? Source { get; set; }
        public string? Type { get; set; }
    }
}