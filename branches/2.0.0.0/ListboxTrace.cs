using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace Test.SqlCopy
{
    class ListboxTrace : TraceListener
    {
        private ListBox _lb;

        public ListboxTrace(ListBox lb)
        {
            _lb = lb;
        }

        delegate void Invoker(string msg);

        private static void SyncBeginInvoke(Control control, MethodInvoker del)
        {
            if ((control != null) && control.InvokeRequired)
                control.BeginInvoke(del, null);
            else
                del();
        }
    

        public override void WriteLine(string message)
        {
            if (!_lb.InvokeRequired)
            {
                while (_lb.Items.Count > 50)
                {
                    _lb.Items.RemoveAt(49);
                }
                _lb.Items.Insert(0, message);
            }
            else
            {
                _lb.BeginInvoke(new MethodInvoker(delegate() { WriteLine(message); }));
            }
        }

        public override void Write(string message)
        {
            //this.WriteLine(message);
        }
    }
}
