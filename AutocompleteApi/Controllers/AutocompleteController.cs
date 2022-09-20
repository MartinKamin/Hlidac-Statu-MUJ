﻿using System.Collections.Generic;
using HlidacStatu.AutocompleteApi.Services;
using HlidacStatu.Entities;
using HlidacStatu.Lib.Analysis.KorupcniRiziko;
using Microsoft.AspNetCore.Mvc;

namespace HlidacStatu.AutocompleteApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AutocompleteController : ControllerBase
    {
        private readonly Caches _cacheService;

        public AutocompleteController(Caches cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet]
        public IEnumerable<Autocomplete> Autocomplete(string q)
        {
            return _cacheService.FullAutocomplete.Search(q, 5);
        }

        public IEnumerable<SubjectNameCache> Kindex(string q)
        {
            //Web/controllers/KindexController
            return _cacheService.Kindex.Search(q, 10);
        }
        
        public IEnumerable<Autocomplete> Companies(string q)
        {
            //Web/controllers/apiv1controller
            return _cacheService.Company.Search(q, 5);
        }

        public IEnumerable<StatniWebyAutocomplete> UptimeServer(string q)
        {
            //HlidacStatu.Repositories.uptimeserverrepo
            return _cacheService.UptimeServer.Search(q, 20);
        }
        
    }
}