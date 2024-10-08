﻿using System.Linq.Expressions;
using HlidacStatu.Entities;
using HlidacStatu.Extensions;

namespace HlidacStatuApi.Models
{
    public class OsobaSocialDTO
    {
        public OsobaSocialDTO(Osoba o, Expression<Func<OsobaEvent, bool>> socialPredicate)
        {
            this.TitulPred = o.TitulPred;
            this.Jmeno = o.Jmeno;
            this.Prijmeni = o.Prijmeni;
            this.TitulPo = o.TitulPo;
            this.NameId = o.NameId;
            this.Profile = o.GetUrl();
            this.SocialniSite = o.NoFilteredEvents(socialPredicate)
                .Select(e => new SocialNetworkDTO(e))
                .ToList();
        }
        public string TitulPred { get; set; }
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public string TitulPo { get; set; }
        public string NameId { get; set; }
        public string Profile { get; set; }
        public List<SocialNetworkDTO> SocialniSite { get; set; }
    }
}