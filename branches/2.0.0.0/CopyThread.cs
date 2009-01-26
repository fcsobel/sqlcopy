using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace Test.SqlCopy
{
    class CopyDoneEventArgs : EventArgs
    {
        public Exception Ex { get; private set; }

        public CopyDoneEventArgs()
        {

        }
        public CopyDoneEventArgs(Exception ex)
        {
            Ex = ex;
        }
    }
    class CopyThread
    {
        private bool _deleteRows;
        private string _destination;
        private string _source;
        private ManualResetEvent _quitEvent;
        private int _threadCount;
        private SqlBulkCopyOptions _options;
        private int _bulkCopyTimeout;
        private int _batchSize;
        private string[] _tables;

        public event EventHandler<CopyProgressEventArgs> CopyProgress;
        public event EventHandler<CopyDoneEventArgs> CopyDone;

        public CopyThread(bool deleteRows
            , string destination
            , string source
            , ManualResetEvent quitEvent
            , int threadCount
            , SqlBulkCopyOptions options
            , int bulkCopyTimeout
            , int batchSize
            , string[] tables)
        {
            _deleteRows = deleteRows;
            _destination = destination;
            _source = source;
            _quitEvent = quitEvent;
            _threadCount = threadCount;
            _options = options;
            _bulkCopyTimeout = bulkCopyTimeout;
            _batchSize = batchSize;
            _tables = tables;
        }

        // Background Version
        public void CopyTables()
        {
            try
            {
                try
                {
                    if (_deleteRows)
                        this.PreCopySql();
                }
                catch
                {
                    Trace.TraceError("Pre SQL Error");
                    throw;
                }

                int threadCount = _threadCount;

                // the worker thread pool
                WorkerThread[] threadPool = new WorkerThread[threadCount];
                // the idle event
                ManualResetEvent[] idleEvents = new ManualResetEvent[threadCount];
                // Threads
                Thread[] workerThreads = new Thread[threadCount];

                // create the copy object
                CopyData copy = new CopyData(_source, _destination, _options, _bulkCopyTimeout, _batchSize, _deleteRows);
                // attach the progress handler
                copy.CopyProgress += new EventHandler<CopyProgressEventArgs>(copy_CopyProgress);

                // construct the thread pool
                for (int i = 0; i < threadCount; i++)
                {
                    WorkerThread workerThread = new WorkerThread(_quitEvent, copy);
                    workerThread.ProcessResult += new EventHandler<ProcessResultArgs>(workerThread_ProcessResult);
                    threadPool[i] = workerThread;
                    idleEvents[i] = threadPool[i].IdleEvent;
                    workerThreads[i] = new Thread(threadPool[i].ThreadProc);
                    workerThreads[i].Start();
                }
                foreach (string tableName in _tables)
                {

                    while (true)
                    {
                        if (!_quitEvent.WaitOne(100, false))
                        {
                            // wait for the first thread to be idle
                            int result = WaitHandle.WaitAny(idleEvents, 1000, false);

                            if (result >= 0 && result != WaitHandle.WaitTimeout) // quit event
                            {
                                //string tableName = (string)row.Cells[1].Value;
                                Trace.TraceInformation("Staring to process table {0} with thread {1}", tableName, result);
                                //row.Cells[3].Value = "Started";
                                idleEvents[result].Reset();
                                threadPool[result].CopyTable(tableName);
                                break;
                            }
                        }
                        else // quit received
                        {
                            Trace.TraceInformation("Cancelling process");
                            foreach (Thread t in workerThreads)
                                t.Abort();
                            foreach (ManualResetEvent evt in idleEvents)
                                evt.Set();
                            return;

                        }
                    }
                }
                // wait for all threads to finish
                WaitHandle.WaitAll(idleEvents);

                if (CopyDone != null)
                {
                    CopyDone.Invoke(this, null);
                }
               

                try
                {
                    if (_deleteRows) this.PostCopySql();
                    Trace.TraceInformation("Done");

                }
                catch
                {
                    Trace.TraceError("Post SQL Error");
                    throw;
                }
            }
            catch (Exception ex)
            {
                if (CopyDone != null)
                {
                    CopyDone.Invoke(this, new CopyDoneEventArgs(ex));
                }
            }
        }

        void workerThread_ProcessResult(object sender, ProcessResultArgs e)
        {
            if (CopyProgress != null)
            {
                CopyProgress.Invoke(this, new CopyProgressEventArgs(e.TableName, e.Exception));
            }
        }

        /// <summary>
        /// handler for the copy progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void copy_CopyProgress(object sender, CopyProgressEventArgs e)
        {
            if (CopyProgress != null)
            {
                CopyProgress.Invoke(sender, e);
            }

        }

        //Turn constraints and triggers back on
        //exec sp_msforeachtable @command1="print '?'", @command2="ALTER TABLE ? CHECK CONSTRAINT all"
        //exec sp_msforeachtable @command1="print '?'", @command2="ALTER TABLE ? ENABLE TRIGGER all"
        public void PostCopySql()
        {
            using (SqlConnection destination = new SqlConnection(_destination))
            {
                //string sql = "exec sp_msforeachtable 'ALTER TABLE ? CHECK CONSTRAINT all'; exec sp_msforeachtable 'ALTER TABLE ? ENABLE TRIGGER all'; ";
                string sql = Properties.Settings.Default.PostCopySql;

                using (SqlCommand command = new SqlCommand(sql, destination))
                {
                    command.CommandTimeout = _bulkCopyTimeout;
                    Trace.TraceInformation("Processing post copy sql");
                    destination.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        //Disable Constraints for all tables
        //exec sp_msforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all"
        //exec sp_msforeachtable "ALTER TABLE ? DISABLE TRIGGER all"
        public void PreCopySql()
        {
            using (SqlConnection destination = new SqlConnection(_destination))
            {
                //string sql = "exec sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'; exec sp_msforeachtable 'ALTER TABLE ? DISABLE TRIGGER all'; ";
                string sql = Properties.Settings.Default.PreCopySql;

                using (SqlCommand command = new SqlCommand(sql, destination))
                {
                    Trace.TraceInformation("Processing pre copy sql");
                    command.CommandTimeout = _bulkCopyTimeout;
                    destination.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
