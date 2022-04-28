﻿using HlidacStatu.Entities;
using HlidacStatu.Repositories;
using HlidacStatu.Web.Filters;
using HlidacStatu.Web.Models.Apiv2;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace HlidacStatu.Web.Controllers
{
    [SwaggerTag("Smlouvy")]

    [Route("api/v2/smlouvy")]
    public class ApiV2SmlouvyController : ApiV2AuthController
    {
        /// <summary>
        /// Vyhledá smlouvy v databázi smluv
        /// </summary>
        /// <param name="dotaz">fulltext dotaz dle <a href="https://www.hlidacstatu.cz/napoveda">syntaxe</a> </param>
        /// <param name="strana">stránka, max. hodnota je 250</param>
        /// <param name="razeni">
        /// pořadí výsledků:<br />
        /// 0: podle relevance<br />
        /// 1: nově zveřejněné první<br />
        /// 2: nově zveřejněné poslední<br />
        /// 3: nejlevnější první<br />
        /// 4: nejdražší první<br />
        /// 5: nově uzavřené první<br />
        /// 6: nově uzavřené poslední<br />
        /// 7: nejvíce chybové první<br />
        /// 8: podle odběratele<br />
        /// 9: podle dodavatele<br />
        /// </param>
        /// <returns></returns>
        [HttpGet("hledat")]
        [Authorize]
        public ActionResult<SearchResultDTO<Smlouva>> Hledat([FromQuery] string? dotaz = null, [FromQuery] int? strana = null, [FromQuery] int? razeni = null)
        {
            if (strana is null || strana < 1)
                strana = 1;
            if (razeni is null)
                razeni = 0;

            if (strana * ApiV2Controller.DefaultResultPageSize > ApiV2Controller.MaxResultsFromES)
            {
                return BadRequest($"Hodnota 'strana' nemůže být větší než {ApiV2Controller.MaxResultsFromES / ApiV2Controller.DefaultResultPageSize}");
            }

            Repositories.Searching.SmlouvaSearchResult result = null;

            if (string.IsNullOrWhiteSpace(dotaz))
            {
                return BadRequest($"Hodnota dotaz chybí.");
            }

            bool? platnyzaznam = null; //1 - nic defaultne
            if (
                System.Text.RegularExpressions.Regex.IsMatch(dotaz.ToLower(), "(^|\\s)id:")
                ||
                dotaz.ToLower().Contains("idverze:")
                ||
                dotaz.ToLower().Contains("idsmlouvy:")
                ||
                dotaz.ToLower().Contains("platnyzaznam:")
                )
                platnyzaznam = null;

            result = SmlouvaRepo.Searching.SimpleSearchAsync(dotaz, strana.Value,
                ApiV2Controller.DefaultResultPageSize,
                (SmlouvaRepo.Searching.OrderResult)razeni.Value,
                platnyZaznam: platnyzaznam);

            if (result.IsValid == false)
            {
                return BadRequest($"Špatně nastavená hodnota dotaz=[{dotaz}]");
            }
            else
            {
                var filtered = result.ElasticResults.Hits
                    .Select(m =>
                        Smlouva.Export(m.Source,
                            HttpContext.User.IsInRole("Admin") || HttpContext.User.IsInRole("KomercniLicence"),
                            HttpContext.User.IsInRole("Admin") || HttpContext.User.IsInRole("KomercniLicence")
                            )
                        )
                    .ToArray();

                return new SearchResultDTO<Smlouva>(result.Total, result.Page, filtered);
            }
        }

        /// <summary>
        /// Vrátí detail jedné smlouvy.
        /// </summary>
        /// <param name="id">id smlouvy</param>
        /// <returns>detail smlouvy</returns>
        [HttpGet("{id?}")]
        [Authorize]
        public ActionResult<Smlouva> Detail([FromRoute] string? id = null)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest($"Hodnota id chybí.");
            }

            var smlouva = SmlouvaRepo.LoadAsync(id);
            if (smlouva == null)
            {
                return NotFound($"Smlouva nenalezena");
            }
            var s = Smlouva.Export(smlouva, HttpContext.User.IsInRole("Admin") || HttpContext.User.IsInRole("KomercniLicence"), HttpContext.User.IsInRole("Admin") || HttpContext.User.IsInRole("KomercniLicence"));

            return s;
        }


        /// <summary>
        /// všechna ID platných verzí smluv. (API pouze pro komerční licence)
        /// </summary>
        /// <returns>detail smlouvy</returns>
        [HttpGet("vsechnaID")]
        [Authorize(Roles = "Admin,KomercniLicence")]
        public ActionResult<string[]> VsechnaID()
        {
            return SmlouvaRepo.AllIdsFromDB(false).ToArray();
        }


        [HttpGet("text/{id?}")]
        [Authorize]
        public ActionResult<IEnumerable<string>> Text([FromRoute] string? id = null, [FromQuery] int? addPredmet = null)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest($"Hodnota id chybí.");
            }

            var smlouva = SmlouvaRepo.LoadAsync(id);
            if (smlouva == null)
            {
                return NotFound($"Smlouva nenalezena");
            }

            var prilohy = smlouva.Prilohy.Select(p => p.PlainTextContent).ToList();

            if (addPredmet is 1)
            {
                prilohy.Add(smlouva.predmet);
            }

            return prilohy;
        }
        
        [HttpGet("predmet/{id?}")]
        [Authorize]
        public ActionResult<string> Predmet([FromRoute] string? id = null)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest($"Hodnota id chybí.");
            }

            var smlouva = SmlouvaRepo.LoadAsync(id);
            if (smlouva == null)
            {
                return NotFound($"Smlouva nenalezena");
            }

            return smlouva.predmet;
        }
    }
}
