using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

using System.Linq;

namespace HlidacStatu.Web.Framework
{
    public class LimitedAccess
    {
        public class MyTestCrawlerRule : Devmasters.Net.Crawlers.CrawlerBase
        {
            public override string Name => "MyTestCrawlerRule";

            public override string[] IP => new string[] {
                //"77.93.208.131/32", "94.124.109.246/32",
                "127.0.0.1/32" };

            public override string[] HostName => null;

            public override string[] UserAgent => new string[] { "Mozilla/5.0" };
        }

        public static Devmasters.Net.Crawlers.ICrawler[] allCrawl = Devmasters.Net.Crawlers.Helper
            .AllCrawlers
            .Union(new Devmasters.Net.Crawlers.ICrawler[] { new MyTestCrawlerRule() })
            .ToArray();

        public static bool IsAuthenticatedOrSearchCrawler(HttpRequest req)
        {
            return req.HttpContext.User.Identity?.IsAuthenticated == true
                   || allCrawl.Any(cr => cr.IsItCrawler(req.HttpContext.Connection.RemoteIpAddress?.ToString(), req.Headers[HeaderNames.UserAgent]));
            //return req.IsAuthenticated || MajorCrawler.Crawlers.Any(cr=>cr.Detected(req.UserHostAddress, req.UserAgent));
        }

        public static bool IsSearchCrawler(HttpRequest req)
        {
            return allCrawl.Any(cr => cr.IsItCrawler(req.HttpContext.Connection.RemoteIpAddress?.ToString(), req.Headers[HeaderNames.UserAgent]));
            //return req.IsAuthenticated || MajorCrawler.Crawlers.Any(cr=>cr.Detected(req.UserHostAddress, req.UserAgent));
        }
    }
}