﻿using System;
using System.Collections.Generic;

namespace HlidacStatu.Repositories.Searching
{
    public class Query
    {
        public static string Formatted(string property, DateTime exactDate)
        {
            return $"{property}:{exactDate:yyyy-MM-dd}";
        }
        public static string Formatted(string property, DateTime fromDate, DateTime toDate, bool includefromDate = true, bool includeToDate = true)
        {
            return $"{property}:{(includefromDate ? "[" : "{")}{fromDate:yyyy-MM-dd} TO {toDate:yyyy-MM-dd}{(includeToDate ? "]" : "}")}";
        }
        public static string ModifyQueryAND(params string[] queryParts)
        {
            return ModifyQuery("AND", queryParts);
        }
        public static string ModifyQueryOR(params string[] queryParts)
        {
            return ModifyQuery("OR", queryParts);
        }
        private static string ModifyQuery(string logicOperator, params string[] queryParts)
        {
            List<string> q = new List<string>();
            if (queryParts == null)
                return string.Empty;
            if (queryParts.Length == 0)
                return string.Empty;

            foreach (string part in queryParts)
            {
                var nq = part.Trim();
                if (string.IsNullOrEmpty(nq))
                    continue;

                if (!nq.StartsWith("(") && !nq.EndsWith(")"))
                    nq = $"( {nq} )";

                q.Add(nq);
            }
            if (q.Count == 0)
                return string.Empty;
            var res = $"( " + string.Join($" {logicOperator} ", q) + " )";

            return res;
        }
    }

}
