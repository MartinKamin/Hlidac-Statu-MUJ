﻿using HlidacStatu.Entities.Entities;
using HlidacStatu.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZiggyCreatures.Caching.Fusion;

namespace PlatyUredniku
{
    public static class StaticCache
    {
        private static IFusionCache _cache;

        public static void Init(IFusionCache fusionCache)
        {
            _cache = fusionCache;
        }

        public static async Task<int> GetPlatyCountPerYearAsync(int year)
        {
            return await _cache.GetOrSetAsync<int>(
                $"platyCount_{year}",
                async _ => (await PuRepo.GetPlatyAsync(PuRepo.DefaultYear)).Count
            );
        }

        public static async Task<List<PuPlat>> GetPoziceDlePlatu(int min, int max, int year)
        {
            return await _cache.GetOrSetAsync<List<PuPlat>>(
                $"{nameof(PuRepo.GetPoziceDlePlatuAsync)}_{min}_{max}_{year}",
                    async _ => await PuRepo.GetPoziceDlePlatuAsync(min, max, year)
        );
        }
    }


}
