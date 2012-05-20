using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using c3o.SqlCopy.Data;
using System.Threading;

namespace c3o.SqlCopy.Win.Service
{
    public partial class SqlCopyService : ServiceBase
    {
        private CronJob job;
        private Timer stateTimer;
        private TimerCallback timerDelegate;


        public SqlCopyService()
        {
            InitializeComponent();

            this.ServiceName = "SqlCopyService";
            this.CanStop = true;
            this.CanPauseAndContinue = false;
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            string path = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).FullName;

            File.WriteAllText(path + @"\start1.txt", DateTime.Now.ToString());

            job = new CronJob();
            timerDelegate = new TimerCallback(job.DoSomething);
            stateTimer = new Timer(timerDelegate, null, 10000, 10000);
        }

        protected override void OnStop()
        {
            string path = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).FullName;
            File.WriteAllText(path + @"\stop1.txt", DateTime.Now.ToString());

            stateTimer.Dispose();
        }

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    File.WriteAllText(@"c:\temp\copy1.txt", DateTime.Now.ToString());

        //    CopyManager.RunCopyJobs();
        //}
    }

    public class CronJob
    {
        public void DoSomething(object state)
        {
            string path = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).FullName;
            File.WriteAllText(path + @"\copy1.txt", DateTime.Now.ToString());

            CopyManager.RunCopyJobs();
        }
    }
}
