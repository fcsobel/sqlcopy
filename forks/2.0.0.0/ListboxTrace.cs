using System.Diagnostics;
using System.Windows.Forms;

namespace Test.SqlCopy
{
    /// <summary>
    /// can be used to display trace output into a listbox
    /// </summary>
    public class ListboxTrace : TraceListener
    {
        private ListBox _lb;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="lb">The listbox that will receive trace data</param>
        public ListboxTrace(ListBox lb)
        {
            _lb = lb;
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
            // ignore this
        }
    }
}
