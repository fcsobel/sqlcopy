using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Test.SqlCopy
{
    /// <summary>
    /// worker class
    /// </summary>
    internal sealed class WorkerThread
    {
        public string TableName { get; private set; }
        public CopyData CopyDataInfo { get; private set; }
        private ManualResetEvent _quitEvent;
        private ManualResetEvent _processingEvent = new ManualResetEvent(false);
        public ManualResetEvent IdleEvent { get; private set; }

        public WorkerThread(ManualResetEvent quitEvent, CopyData copyData)
        {
            this.CopyDataInfo = copyData;
            _quitEvent = quitEvent;

            IdleEvent = new ManualResetEvent(true);
        }

        public void CopyTable(string tableName)
        {
            this.TableName = tableName;
            this.IdleEvent.Reset();
            _processingEvent.Set();
        }

        public void ThreadProc()
        {
            while (true)
            {
                int result = WaitHandle.WaitAny(new WaitHandle[] { _quitEvent, _processingEvent });
                switch (result)
                {
                    case 0: // quit
                        IdleEvent.Set();
                        return;
                    case 1: // go
                        break;
                }

                try
                {
                    //do work
                    CopyDataInfo.CopyTable(TableName);
                }
                catch (Exception ex)
                {
                    Trace.TraceError("Error occurred while processing table {0}: {1}", TableName, ex.Message);
                    // break down
                    _quitEvent.Set();
                }

                _processingEvent.Reset();
                IdleEvent.Set();
            }
        }
    }
}
