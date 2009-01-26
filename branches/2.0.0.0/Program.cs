using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Test.SqlCopy
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Control.CheckForIllegalCrossThreadCalls = false;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new   SqlCopyForm());
        }
    }
}