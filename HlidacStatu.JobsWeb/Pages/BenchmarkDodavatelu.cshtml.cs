using HlidacStatu.JobsWeb.Models;
using HlidacStatu.JobsWeb.Services;
using HlidacStatu.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HlidacStatu.JobsWeb.Pages
{
    [Authorize(Roles = "Admin")]
    public class BenchmarkDodavateluModel : PageModel
    {
        public string Ico { get; set; }
        public string Nazev { get; private set; }
        public YearlyStatisticsGroup.Key? Key { get; set; }
        

        public IActionResult OnGet(string id)
        {
            if (HttpContext.HasAccess() == false)
                return Redirect("/");


            Ico = id;
            Nazev = FirmaRepo.NameFromIco(Ico, true);

            Key = HttpContext.TryFindKey();

            return Page();
        }

    }
}