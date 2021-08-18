﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HlidacStatu.Lib.Data.External.Camelot
{

    public class ClientLow
        : IAsyncDisposable, IDisposable
    {

        public enum Commands
        {
            stream,
            lattice
        }

        public string SessionId { get; private set; } = null;

        private bool disposedValue;

        public string ApiEndpoint { get; private set; } = null;

        private static Devmasters.Logging.Logger logger = new Devmasters.Logging.Logger("Camelot.ClientLow");
        public ClientLow(string apiEndpoint)
        {
            this.ApiEndpoint = apiEndpoint;
        }

        public async Task<ApiResult<string>> StartSessionAsync(string pdfUrl, Commands command, CamelotResult.Formats format, string pages = "all")
        {
            try
            {
                    using (System.Net.WebClient wc = new System.Net.WebClient())
                    {
                        string baseUrl = ApiEndpoint;
                        string url = baseUrl + "/Camelot/StartSessionWithUrl?url=" + System.Net.WebUtility.UrlEncode(pdfUrl);
                        url += "&command=" + command.ToString().ToLower();
                        url += "&format=" + format.ToString().ToLower();
                        url += "&pages=" + pages;

                        var json = await wc.DownloadStringTaskAsync(new Uri(url));
                        var res = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<string>>(json);

                        return res;
                    }

            }
            catch (Exception e)
            {
                return new ApiResult<string>(false);
            }
        }

        public async Task<ApiResult<CamelotResult>> GetSessionAsync(string sessionId)
        {
            try
            {
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    string url = ApiEndpoint + "/Camelot/GetSession?sessionId=" + System.Net.WebUtility.UrlEncode(sessionId);

                    var json = await wc.DownloadStringTaskAsync(new Uri(url));
                    var res = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<CamelotResult>>(json);

                    return res;
                }

            }
            catch (Exception e)
            {
                return new ApiResult<CamelotResult>(false);
            }
        }

        public async Task<ApiResult> EndSessionAsync(string sessionId)
        {
            try
            {
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    string url = ApiEndpoint + "/Camelot/EndSession?sessionId=" + System.Net.WebUtility.UrlEncode(sessionId);

                    var json = await wc.DownloadStringTaskAsync(new Uri(url));
                    var res = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<CamelotResult>>(json);

                    return res;
                }

            }
            catch (Exception e)
            {
                return new ApiResult<CamelotResult>(false);
            }
        }
        public async Task<ApiResult<CamelotVersion>> VersionAsync()
        {
            try
            {
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    string url = ApiEndpoint + "/Camelot/Version";

                    var json = await wc.DownloadStringTaskAsync(new Uri(url));
                    var res = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<CamelotVersion>>(json);

                    return res;
                }

            }
            catch (Exception e)
            {
                return new ApiResult<CamelotVersion>(false);
            }
        }
        public async Task<ApiResult<CamelotStatistics>> StatisticAsync()
        {
            try
            {
                using (System.Net.WebClient wc = new System.Net.WebClient())
                {
                    string url = ApiEndpoint + "/Camelot/Statistic";

                    var json = await wc.DownloadStringTaskAsync(new Uri(url));
                    var res = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResult<CamelotStatistics>>(json);

                    return res;
                }

            }
            catch (Exception e)
            {
                return new ApiResult<CamelotStatistics>(false);
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ClientLow()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            Dispose(false);
        }
    }
}
