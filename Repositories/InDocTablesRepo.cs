﻿using System;
using System.Collections.Generic;
using HlidacStatu.Entities;

using Microsoft.EntityFrameworkCore;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HlidacStatu.Repositories.Exceptions;


namespace HlidacStatu.Repositories
{
    public partial class InDocTablesRepo
    {
        public static void Save(InDocTables tbl)
        {
            using (DbEntities db = new DbEntities())
            {
                db.InDocTables.Attach(tbl);
                if (tbl.Pk == 0)
                    db.Entry(tbl).State = EntityState.Added;
                else
                    db.Entry(tbl).State = EntityState.Modified;

                db.SaveChanges();
            }
        }

        public static async Task<InDocTables> GetNextForCheck(string requestedBy, CancellationToken cancellationToken)
        {
            await using (DbEntities db = new DbEntities())
            {
                //pokud zůstal rozpracovaný úkol, lízne si nejprve ten
                var inProgress = await db.InDocTables
                    .AsQueryable()
                    .Where(m => m.Status == (int)InDocTables.CheckStatuses.InProgress)
                    .Where(m => m.CheckedBy == requestedBy)
                    .FirstOrDefaultAsync(cancellationToken);

                if (inProgress != null)
                    return inProgress;

                var tbl = await db.InDocTables
                    .AsQueryable()
                    .Where(m => m.Status == (int)InDocTables.CheckStatuses.WaitingInQueue)
                    .OrderByDescending(m => m.PrecalculatedScore)
                    .FirstOrDefaultAsync(cancellationToken);

                if (tbl is null)
                    throw new NoDataFoundException("Table InDocTables neobsahuje zadna data ke zpracovani.");
                
                tbl.CheckStatus = InDocTables.CheckStatuses.InProgress;
                tbl.CheckedBy = requestedBy;
                tbl.CheckedDate = DateTime.Now;
                
                await db.SaveChangesAsync(cancellationToken);
                
                return tbl;
            }
        }
        
        public static async Task<InDocTables> GetSpecific(int pk, string requestedBy, CancellationToken cancellationToken)
        {
            await using (DbEntities db = new DbEntities())
            {
                var tbl = await db.InDocTables
                    .AsQueryable()
                    .Where(m => m.Pk == pk)
                    .FirstOrDefaultAsync(cancellationToken);
                
                if (tbl is null)
                    throw new NoDataFoundException("Table InDocTables neobsahuje zadna data ke zpracovani.");
                
                tbl.CheckStatus = InDocTables.CheckStatuses.InProgress;
                tbl.CheckedBy = requestedBy;
                tbl.CheckedDate = DateTime.Now;

                return tbl;
            }
        }
        
        public static async Task<Dictionary<InDocTables.CheckStatuses, int>> GlobalStatistic(CancellationToken cancellationToken)
        {
            await using (DbEntities db = new DbEntities())
            {
                var data = await db.InDocTables
                    .AsNoTracking()
                    .Select(t => new { t.Status, t.CheckedBy, t.CheckElapsedInMs })
                    .ToListAsync(cancellationToken);
                    
                var stat = data.GroupBy(t => t.Status)
                    .ToDictionary(g => (InDocTables.CheckStatuses)g.Key,
                        g => g.Count());

                return stat;
            }
        }
        
        public static async Task<Dictionary<InDocTables.CheckStatuses, int>> UserStatistic(string user, CancellationToken cancellationToken)
        {
            await using (DbEntities db = new DbEntities())
            {
                var data = await db.InDocTables
                    .AsNoTracking()
                    .Where(t => t.CheckedBy == user)
                    .Select(t => new { t.Status, t.CheckedBy, t.CheckElapsedInMs })
                    .ToListAsync(cancellationToken);
                
                var stat = data.GroupBy(t => t.Status)
                    .ToDictionary(g => (InDocTables.CheckStatuses)g.Key,
                        g => g.Count());

                return stat;
            }
        }

        // public static void ChangeStatus(long tblPK, InDocTables.CheckStatuses status, string checkedBy, long checkElapsedTimeInMS)
        // {
        //     using (DbEntities db = new DbEntities())
        //     {
        //         var tbl = db.InDocTables
        //             .AsNoTracking()
        //             .FirstOrDefault(m => m.Pk == tblPK);
        //
        //         if (tbl != null)
        //             ChangeStatus(tbl, status, checkedBy, checkElapsedTimeInMS);
        //     }
        // }
        
        public static async Task ChangeStatus(InDocTables tbl, InDocTables.CheckStatuses status, string checkedBy,
            long checkElapsedTimeInMS)
        {
            await using (DbEntities db = new DbEntities())
            {
                db.InDocTables.Attach(tbl);
                if (tbl.Pk == 0)
                    db.Entry(tbl).State = EntityState.Added;
                else
                    db.Entry(tbl).State = EntityState.Modified;

                tbl.CheckStatus = status;
                tbl.CheckedBy = checkedBy;
                tbl.CheckedDate = System.DateTime.Now;
                tbl.CheckElapsedInMs = (int)checkElapsedTimeInMS;

                await db.SaveChangesAsync();

            }
        }



    }
}
