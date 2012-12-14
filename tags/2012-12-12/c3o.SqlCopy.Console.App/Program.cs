using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using c3o.SqlCopy.Data;
using c3o.SqlCopy.Objects;

namespace c3o.SqlCopy.Console.App
{
    class Program
    {
        static void Main(string[] args)
        {
            int exitCode = 0;

            try
            {

                if (args.Length > 0)
                {
                    bool argsOkay = true;
                    bool outputToFile = false;
                    TextWriter streamWriter = null;

                    // get and process arguments
                    string filename = args[0];
                    // add in start folder
                    filename = Path.Combine(Directory.GetParent(filename).FullName, Path.GetFileName(filename));
                    string outputFileName = string.Empty;
                    if (args.Length > 1)
                    {
                        if (args.Length == 3)
                        {
                            if (args[1].ToLower() == "-o")
                            {
                                outputFileName = args[2];
                                // add in start folder
                                outputFileName = System.IO.Path.Combine(Directory.GetParent(outputFileName).FullName, Path.GetFileName(outputFileName));
                                if (Directory.Exists(Directory.GetParent(outputFileName).FullName))
                                {
                                    streamWriter = new StreamWriter(outputFileName, false, Encoding.Unicode);
                                    outputToFile = true;
                                }
                                else
                                {
                                    argsOkay = false;
                                    exitCode = -6;
                                }
                            }
                            else
                            {
                                argsOkay = false;
                                exitCode = -4;
                            }
                        }
                        else
                        {
                            argsOkay = false;
                            exitCode = -4;
                        }
                    }
                    else
                    {
                        if (!File.Exists(filename))
                        {
                            argsOkay = false;
                            exitCode = -5;
                        }
                    }

                    if (argsOkay)
                    {
                        CopyObject obj = CopyObject.Read(filename);

                        if (obj != null)
                        {
                            CopyManager manager = new CopyManager(obj);
                            {
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
                                            manager.Copy(table);
                                            table.Status = "Success";
                                            System.Console.WriteLine("Success " + table.FullName);
                                            if (outputToFile)
                                                streamWriter.WriteLine("Success " + table.FullName);
                                        }
                                    }
                                    catch (Exception er)
                                    {
                                        table.Status = er.Message;
                                        System.Console.WriteLine("Error " + table.FullName);
                                        if (outputToFile)
                                            streamWriter.WriteLine("Error " + table.FullName);
                                        System.Console.WriteLine("Reason \t" + table.Status);
                                        if (outputToFile)
                                            streamWriter.WriteLine("Reason \t" + table.Status);
                                        exitCode = -1;
                                    }
                                }

                                try
                                {
                                    manager.PostCopy();
                                }
                                catch (Exception er)
                                {
                                    obj.PostCopyStatus = er.Message;
                                    System.Console.WriteLine("Error Post copy processing");
                                    if (outputToFile)
                                        streamWriter.WriteLine("Error Post copy processing");
                                    System.Console.WriteLine("Reason \t" + obj.PostCopyStatus);
                                    if (outputToFile)
                                        streamWriter.WriteLine("Reason \t" + obj.PostCopyStatus);
                                    exitCode = -1;
                                }

                            }
                        }
                        else
                        {
                            System.Console.WriteLine(string.Format("Unable to parse XML settings file '{0}'", filename));
                            if (outputToFile)
                                streamWriter.WriteLine(string.Format("Unable to parse XML settings file '{0}'", filename));
                            exitCode = -2;
                        }
                        if (streamWriter != null)
                            streamWriter.Close();
                    }
                    else
                    {
                        switch (exitCode)
                        {
                            case -4:
                                System.Console.WriteLine("Usage is sqlcopy 'Path to XML settings file' [-o 'Path to output file']");
                                break;
                            case -5:
                                System.Console.WriteLine(string.Format("XML settings file '{0}' is not valid", filename));
                                System.Console.WriteLine("Usage is sqlcopy 'Path to XML settings file' [-o 'Path to output file']");
                                break;
                            case -6:
                                System.Console.WriteLine(string.Format("Output file path '{0}' is not valid", Directory.GetParent(outputFileName).FullName));
                                System.Console.WriteLine("Usage is sqlcopy 'Path to XML settings file' [-o 'Path to output file']");
                                break;
                        }
                    }
                }
                else
                {
                    System.Console.WriteLine("Usage is sqlcopy 'Path to XML settings file' [-o 'Path to output file']");
                    exitCode = -3;
                }

            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error during processing.");
                System.Console.WriteLine("Reason \t" + ex.Message);
            }
            Environment.Exit(exitCode);
        }
    }
}