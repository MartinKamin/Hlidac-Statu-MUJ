using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Devmasters;
using Devmasters.Cache.LocalMemory;
using HlidacStatu.Connectors;
using HlidacStatu.Entities;
using HlidacStatu.Util;
using Microsoft.EntityFrameworkCore;

namespace HlidacStatu.Repositories
{
    public static partial class OsobaRepo
    {
        private static string AppDataPath = null;
        
        public static AutoUpdatedLocalMemoryCache<List<Osoba>> PolitickyAktivni = null;
        public static AutoUpdatedLocalMemoryCache<List<Osoba>> Politici = null;

        static OsobaRepo()
        {
            AppDataPath = Init.WebAppDataPath;
            if (string.IsNullOrEmpty(AppDataPath))
            {
                throw new ArgumentNullException("App_Data_Path");
            }

            

            Consts.Logger.Info("Static data - Politici");
            Politici = new AutoUpdatedLocalMemoryCache<List<Osoba>>(
                TimeSpan.FromHours(36), "politiciOnly", (obj) =>
                {
                    List<Osoba> osoby = null;

                    using (DbEntities db = new DbEntities())
                    {
                        osoby = db.Osoba
                            .AsNoTracking()
                            .Where(m => m.Status == (int) Osoba.StatusOsobyEnum.Politik)
                            .ToArray()
                            .OrderBy(o =>
                            {
                                var index = OsobaRepo.Searching.PolitikImportanceOrder.IndexOf(o.Status);
                                return index == -1 ? int.MaxValue : index;
                            })
                            .ToList();
                        ;
                        //return osoby;
                        return osoby;
                    }
                }
            );
            PolitickyAktivni = new AutoUpdatedLocalMemoryCache<List<Osoba>>(
                TimeSpan.FromHours(36), "politickyAktivni", (obj) =>
                {
                    List<Osoba> osoby = new List<Osoba>();

                    using (DbEntities db = new DbEntities())
                    {
                        osoby.AddRange(Politici.Get());
                        var osobyQ = db.Osoba.AsQueryable()
                            //migrace: SponzoringLimitsPredicate hodit do repositories
                            .Where(m => db.Sponzoring.Any(SponzoringRepo.SponzoringLimitsPredicate))
                            .Where(m => m.Status == (int) Osoba.StatusOsobyEnum.VazbyNaPolitiky ||
                                        m.Status == (int) Osoba.StatusOsobyEnum.Sponzor)
                            .AsNoTracking()
                            .ToArray()
                            .OrderBy(o =>
                            {
                                var index = OsobaRepo.Searching.PolitikImportanceOrder.IndexOf(o.Status);
                                return index == -1 ? int.MaxValue : index;
                            });
                        osoby.AddRange(osobyQ);
                        //return osoby;
                        return osoby;
                    }
                }
            );
        }

 
    }
}