using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;

namespace c3o.SqlCopy.Win.Service
{

    //    The new service must be installed before it can be run. Open a .NET command prompt and do this:


    //C:\> InstallUtil /LogToConsole=true cron.exe   # flag is optional but handy
    //C:\> net start cron
    //C:\> net stop cron              # to stop the service when finished
    //C:\> InstallUtil /u cron.exe    # to uninstall


    [RunInstaller(true)]
    public partial class SqlCopyInstaller : Installer
    {
        private ServiceProcessInstaller processInstaller;
        private ServiceInstaller serviceInstaller;

        public SqlCopyInstaller()
        {
            InitializeComponent();

            processInstaller = new ServiceProcessInstaller();
            serviceInstaller = new ServiceInstaller();
            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "SqlCopyService";
            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);
        }
    }
}
