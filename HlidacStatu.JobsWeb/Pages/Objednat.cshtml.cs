﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WatchdogAnalytics.Pages
{
    public class ObjednatModel : PageModel
    {
        private readonly ILogger<ObjednatModel> _logger;

        public ObjednatModel(ILogger<ObjednatModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}