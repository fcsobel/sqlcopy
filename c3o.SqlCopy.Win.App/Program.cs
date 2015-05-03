using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace c3o.SqlCopy
{
    static class Program
    {
		//[DllImport( "kernel32.dll" )]
		//static extern bool AttachConsole( int dwProcessId );
		//private const int ATTACH_PARENT_PROCESS = -1;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			// redirect console output to parent process;
            // must be before any calls to Console.WriteLine()
            //AttachConsole( ATTACH_PARENT_PROCESS );

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new   SqlCopyForm());

            //Application.Run(new CopyManagerForm());
			//test
        }
    }
}