using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Test.SqlCopy
{
    /// <summary>
    /// reports on process result
    /// </summary>
    public class ProcessResultArgs : EventArgs
    {
        public Exception Exception { get; private set; }
        public string TableName { get; private set; }
        public bool Success
        {
            get
            {
                return Exception == null;
            }
        }

        public ProcessResultArgs(string tableName, Exception ex)
        {
            this.Exception = ex;
            this.TableName = tableName;
        }
        public ProcessResultArgs(string tableName) : this(tableName, null)
        {
        }
    }
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
        public event EventHandler<ProcessResultArgs> ProcessResult;

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
                Exception e = null;
                try
                {
                    //do work
                    CopyDataInfo.CopyTable(TableName);
                }
                catch (Exception ex)
                {
                    Trace.TraceError("Error occurred while processing table {0}: {1}", TableName, ex.Message);
                    e = ex;
                }

                _processingEvent.Reset();
                IdleEvent.Set();
                if (ProcessResult != null)
                {
                    ProcessResult.Invoke(this, new ProcessResultArgs(TableName, e));
                }
            }
        }
    }
}
