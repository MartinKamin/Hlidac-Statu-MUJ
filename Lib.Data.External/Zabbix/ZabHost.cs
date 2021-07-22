﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Devmasters.Enums;

namespace HlidacStatu.Lib.Data.External.Zabbix
{

    public enum Statuses
    {
        OK = 0,
        Pomalé = 1,
        Nedostupné = 2,
        TimeOuted = 98,
        BadHttpCode = 99,
        Unknown = 1000,
    }

    [ShowNiceDisplayName()]
    public enum SSLLabsGrades
    {
        [NiceDisplayName("A+")]
        Aplus = 1,
        A = 2,
        [NiceDisplayName("A-")]
        Aminus = 3,
        B = 5,
        C = 6,
        D = 7,
        E = 8,
        F = 9,
        [NiceDisplayName("T")]
        T = 20,
        [NiceDisplayName("M")]
        M = 50,
        [NiceDisplayName("X")]
        X = 100

    }

    public class ZabHost
    {
        private string _hash = null;
        public ZabHost(string hostId, string host, string url, string description, string[] mainGroup = null)
        {
            hostid = hostId;
            this.host = host;
            this.url = url;
            urad = Devmasters.TextUtil.NormalizeToBlockText(Devmasters.RegexUtil.GetRegexGroupValue(description, @"Urad:\s?(?<txt>[^\x0a\x0d]*)", "txt"));
            popis = Devmasters.TextUtil.ShortenHTML(Devmasters.RegexUtil.GetRegexGroupValue(description, @"Popis:\s?(?<txt>[^\x0a\x0d]*)", "txt"), 10000, new string[] {"a","b"} );
            publicname = Devmasters.TextUtil.NormalizeToBlockText(Devmasters.RegexUtil.GetRegexGroupValue(description, @"Nazev:\s?(?<txt>[^\x0a\x0d]*)", "txt"));            
            string sgroup = Devmasters.TextUtil.NormalizeToBlockText(Devmasters.RegexUtil.GetRegexGroupValue(description, @"Poznamka:\s?(?<txt>[^\x0a\x0d]*)", "txt"));

            customUrl = Devmasters.TextUtil.NormalizeToBlockText(Devmasters.RegexUtil.GetRegexGroupValue(description, @"URL:\s?(?<txt>[^\x0a\x0d]*)", "txt"));

            groups.Clear();
            if (!string.IsNullOrEmpty(sgroup))
            {
                var agroups = sgroup.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                groups.AddRange(agroups);
            }
            if (mainGroup != null && mainGroup.Length > 0)
                groups.AddRange(mainGroup);

            if (string.IsNullOrEmpty(publicname))
                publicname = this.host;
            _hash = Devmasters.Crypto.Hash.ComputeHashToHex(hostid + "xxttxx" + hostid);
        }
        public string hostid { get; set; }
        public string host { get; set; }
        public string url { get; set; }

        public string opendataUrl { get { return "https://www.hlidacstatu.cz/api/v2/Weby/" + hostid ; } }
        public string pageUrl { get { return "https://www.hlidacstatu.cz/StatniWeby/Info/" + hostid + "?h=" + hash; } }

        [JsonIgnore()]
        public string customUrl { get; set; }
        public string urad { get; set; }
        public string publicname { get; set; }
        public string popis { get; set; }
        [JsonIgnore()]
        public string itemIdResponseTime { get; set; }
        [JsonIgnore()]
        public string itemIdResponseCode { get; set; }

        [JsonIgnore()]
        public string itemIdSsl { get; set; }
        [JsonIgnore()]
        public List<string> groups { get; set; } = new List<string>();

        public string UriHost()
        {
            string s = "";
            if (!string.IsNullOrEmpty(customUrl))
                s = customUrl;
            else
                s = url;
            Uri uri = null;
            Uri.TryCreate(s, UriKind.Absolute, out uri);
            return uri?.Host;

        }

        public string hash { get { return _hash; } }
        public bool ValidHash(string h)
        {
            return h == _hash;
        }
    }
}