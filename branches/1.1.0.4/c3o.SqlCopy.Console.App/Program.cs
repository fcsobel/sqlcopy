using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using c3o.SqlCopy.Data;
using c3o.SqlCopy.Objects;

namespace c3o.SqlCopy.Console.App
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string filename = args[0];

                if (!string.IsNullOrEmpty(filename))
                {
                    CopyObject obj = SerializationHelper.Deserialize<CopyObject>(filename);

                    if (obj != null)
                    {
                        CopyManager manager = new CopyManager(obj);
                        {
                            //manager.Copy();


                            try
                            {
                                manager.PreCopy();
                            }
                            catch (Exception er)
                            {
                                obj.PreCopyStatus = er.Message;
                                return;
                            }

                            foreach (TableObject table in obj.Tables)
                            {
                                try
                                {
                                    if (table.Selected)
                                    {
                                        manager.Copy(table.Name);
                                        table.Status = "Success";
                                        System.Console.WriteLine("Success " + table.Name);
                                    }
                                }
                                catch (Exception er)
                                {
                                    table.Status = er.Message;
                                    System.Console.WriteLine("Error " + table.Name);
                                }

                                
                            }

                            try
                            {
                                manager.PostCopy();
                            }
                            catch (Exception er)
                            {
                                obj.PostCopyStatus = er.Message;
                            }
                        }
                    }
                }
            }
        }
    }
}
