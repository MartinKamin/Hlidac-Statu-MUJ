using HlidacStatu.Repositories;
using Microsoft.AspNetCore.Mvc;
using PlatyUredniku.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatyUredniku.Controllers;

public class HomeController : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewData["platy"] = await PuRepo.GetPlatyAsync(PuRepo.DefaultYear);

        return View();
    }

    public async Task<IActionResult> DlePlatu(int id)
    {
        (int Min, int Max) range = id switch
        {
            2 => (20_000, 40_000),
            3 => (40_000, 70_000),
            4 => (70_000, 1000_000_000),
            _ => (0, 20_000)
        };


        var platy = await PuRepo.GetPoziceDlePlatuAsync(range.Min, range.Max, PuRepo.DefaultYear);

        ViewData["platy"] = platy;
        ViewData["context"] = $"Dle platů {range.Min} - {range.Max},-";

        return View(platy);
    }

    public async Task<IActionResult> Oblast(string id)
    {
        var organizace = await PuRepo.GetOrganizaceForTagAsync(id);

        ViewData["platy"] = organizace.SelectMany(o => o.Platy).ToList();
        ViewData["oblast"] = id;
        ViewData["context"] = $"{id}";

        return View(organizace);
    }

    public async Task<IActionResult> Oblasti()
    {
        var oblasti = PuRepo.MainTags;

        List<Breadcrumb> breadcrumbs = new()
        {
            new Breadcrumb()
            {
                Name = nameof(Oblasti),
                Link = $"{nameof(Oblasti)}"
            }
        };

        ViewData["breadcrumbs"] = breadcrumbs;

        return View(oblasti);
    }

    public async Task<IActionResult> Detail2(string id, int rok = PuRepo.DefaultYear)
    {
        var detail = await PuRepo.GetFullDetailAsync(id);
        ViewData["platy"] = detail.Platy.ToList();
        ViewData["context"] = detail.FirmaDs.DsSubjName;

        return View(detail);
    }

    public async Task<IActionResult> Detail(string id, int? rok = null)
    {
        var detail = await PuRepo.GetFullDetailAsync(id);

        ViewData["platy"] = detail.Platy.ToList();
        ViewData["rok"] = rok ?? (detail.Platy.Any() ? detail.Platy.Max(m => m.Rok) : PuRepo.DefaultYear);
        ViewData["id"] = id;
        ViewData["context"] = detail.FirmaDs.DsSubjName;

        return View(detail);
    }

    public async Task<IActionResult> Plat(int id)
    {
        var detail = await PuRepo.GetPlatAsync(id);
        ViewData["context"] = $"{detail.NazevPozice} v organizaci {detail.Organizace.FirmaDs.DsSubjName}";

        return View(detail);
    }

    public IActionResult Statistiky()
    {
        return View();
    }
}