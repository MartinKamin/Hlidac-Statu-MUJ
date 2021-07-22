using System.Security.Principal;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HlidacStatu.Web.Framework
{
    public static class ViewContextExtensions
    {
        public static string GetRequestPath(this ViewContext viewContext)
        {
            return viewContext.HttpContext.Request.Path;
        }
        
        public static string GetRequestPathAndQuery(this ViewContext viewContext)
        {
            return viewContext.HttpContext.Request.GetEncodedPathAndQuery();
        }

        public static bool IsAuthenticatedRequest(this ViewContext viewContext)
        {
            return viewContext.HttpContext.User.Identity?.IsAuthenticated ?? false;
        }
        
        public static IIdentity? GetUserIdentity(this ViewContext viewContext)
        {
            return viewContext.HttpContext.User.Identity;
        }

        public static string GetDisplayUrl(this ViewContext viewContext)
        {
            return viewContext.HttpContext.Request.GetDisplayUrl();
        }
        
        
    }
}