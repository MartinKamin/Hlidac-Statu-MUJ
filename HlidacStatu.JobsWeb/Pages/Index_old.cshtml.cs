﻿using WatchdogAnalytics.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WatchdogAnalytics.Models;

namespace WatchdogAnalytics.Pages
{
    public class Index_oldModel : PageModel
    {
        
        public YearlyStatisticsGroup.Key? Key { get; set; }
        
        public void OnGet()
        {
            Key = HttpContext.TryFindKey();
        }
    }
}