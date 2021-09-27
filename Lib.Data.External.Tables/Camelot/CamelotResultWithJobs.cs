﻿using System;
using System.Collections.Generic;

namespace HlidacStatu.Lib.Data.External.Tables.Camelot
{
    public class CamelotResultWithJobs : CamelotResult
    {
        static HlidacStatu.DetectJobs.InTables it_inTables = new DetectJobs.InTables("IT",
            HlidacStatu.DetectJobs.IT.Keywords, HlidacStatu.DetectJobs.IT.OtherWords, HlidacStatu.DetectJobs.IT.BlacklistedWords
            );

        public CamelotResultWithJobs(Result cr)
        {
            if (cr == null)
                throw new ArgumentNullException("cr");
            if (cr.Tables != null)
            {
                List<TableWithJobs> tbls = new List<TableWithJobs>();
                foreach (var crT in cr.Tables)
                {
                    TableWithJobs tbl = new TableWithJobs();
                    tbl.Content = crT.Content;
                    tbl.Page = crT.Page;
                    tbl.TableInPage = crT.TableInPage;

                    var score = it_inTables.TableWithWordsAndNumbers(
                        tbl.ParsedContent(), out var foundJobs, out var cells);

                    if (foundJobs != null)
                        tbl.FoundJobs = foundJobs.ToArray();
                    else
                        tbl.FoundJobs = new DetectJobs.InTables.Job[] { };
                    tbls.Add(tbl);
                }
                this.TablesWithJobs = tbls.ToArray();
            }
            this.Algorithm = cr.Algorithm;
            this.ElapsedTimeInMs = cr.ElapsedTimeInMs;
            this.Format = cr.Format;
            this.FoundTables = cr.FoundTables;
            this.Status = cr.Status;
            this.Tables = cr.Tables;

        }

        public class TableWithJobs : Table
        {
            public HlidacStatu.DetectJobs.InTables.Job[] FoundJobs { get; set; }
        }

        public TableWithJobs[] TablesWithJobs { get; set; } = new TableWithJobs[] { };


    }

}
