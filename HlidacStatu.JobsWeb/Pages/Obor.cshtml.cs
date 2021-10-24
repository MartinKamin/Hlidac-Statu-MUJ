using HlidacStatu.JobsWeb.Models;
using HlidacStatu.JobsWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HlidacStatu.JobsWeb.Pages
{
    [Authorize(Roles = "Admin")]
    public class OborModel : PageModel
    {

        public YearlyStatisticsGroup.Key? Key { get; set; }
        public string Obor { get; set; }
        
        public void OnGet(string id)
        {
            Obor = id;
            Key = HttpContext.TryFindKey();
        }
        
    }
}