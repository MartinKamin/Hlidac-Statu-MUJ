﻿using System;
using System.Linq;

namespace HlidacStatu.Entities
{
    public partial class UptimeServer
    {

        public string[] GroupArray()
        {
            if (string.IsNullOrEmpty(this.Groups))
                return new string[] { this.Priorita.ToString() };
            else
                return this.Groups.Split('|', StringSplitOptions.RemoveEmptyEntries)
                    .Append(this.Priorita.ToString())
                    .ToArray();
        }


        public string Hash()
        {
            return Devmasters.Crypto.Hash.ComputeHashToHex(Id + "xxttxx" + Id);

        }
        public bool ValidHash(string h)
        {
            return h == Hash();
        }

        Uri _uri = null;
        public string Host()
        {
            InitUri();
            return _uri?.Host;
        }

        private void InitUri()
        {
            if (_uri == null)
            {
                Uri.TryCreate(this.PublicUrl, UriKind.Absolute, out _uri);
            }
        }
    }
}
