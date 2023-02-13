﻿using HlidacStatu.Entities;
using System;
using System.Runtime.CompilerServices;

namespace HlidacStatu.Repositories
{
    public static partial class MonitoredTaskRepo
    {
        public class ForBatch : MonitoredTask, Devmasters.Batch.IMonitor, IDisposable
        {
            private bool disposedValue;

            public ForBatch(
                string application = null,
                string part = null,
                [CallerMemberName] string callerMemberName = "",
                [CallerFilePath] string sourceFilePath = "",
                [CallerLineNumber] int sourceLineNumber = 0
)
                : base(application ?? Devmasters.IO.IOTools.GetExecutingFileName(), part ?? "")
            {
                if (string.IsNullOrEmpty(this.Part))
                {
                    var fn = System.IO.Path.GetFileName(sourceFilePath);
                    this.Part = Devmasters.TextUtil.ShortenText($"{fn}: {callerMemberName} ({sourceLineNumber})", 500, "", "");
                }
            }

            public void Start()
            {
                this.Started = DateTime.Now;
                _ = MonitoredTaskRepo.Create(this);
            }

            public void SetProgress(decimal inPercent)
            {
                _ = MonitoredTaskRepo.SetProgress(this, inPercent);
            }


            public void Finish(bool success, Exception exception)
            {
                _ = MonitoredTaskRepo.Finish(this, success, exception);
            }

            public void Finish(params Exception[] exceptions)
            {
                bool success = exceptions == null || exceptions.Length == 0;
                _ = MonitoredTaskRepo.Finish(this, success , success ? null : new AggregateException(exceptions));
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: dispose managed state (managed objects)
                        this.Finished = this.Finished ?? DateTime.Now;
                        this.Success = this.Progress < 100 ?  false : true;
                        if (this.Success == false)
                            this.Exception = new ApplicationException("Canceled before end").ToString();
                        _= MonitoredTaskRepo.Update(this);
                    }

                    // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                    // TODO: set large fields to null
                    disposedValue = true;
                }
            }

            // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
            // ~ForBatch()
            // {
            //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            //     Dispose(disposing: false);
            // }

            public void Dispose()
            {
                // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }

    }
}