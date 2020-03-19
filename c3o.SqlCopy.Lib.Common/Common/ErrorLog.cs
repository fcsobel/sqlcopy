using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c3o.SqlCopy.Common
{

    public sealed class ErrorLog
    {
        public List<string> Errors { get; set; }

        private ErrorLog()
        {
            this.Errors = new List<string>();
        }

        public static ErrorLog Instance { get { return Nested.instance; } }
        private class Nested
        {
            // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly ErrorLog instance = new ErrorLog();
        }
    }
}