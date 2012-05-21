using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using c3o.SqlCopy.Data;
using c3o.SqlCopy.Objects;

namespace c3o.SqlCopy.Console.App
{
	/// <summary>
	/// Simple console app to run a saved xml copy job created in the ui
	/// </summary>
	class Program
	{
		static void Main(string[] args)
		{
			int exitCode = 0;

			if (args.Length > 0)
			{
				string filename = args[0];

				if (!string.IsNullOrEmpty(filename))
				{
					// load the xml copy job into memory
					CopyObject obj = CopyObject.Read(filename);
					
					if (obj != null)
					{
						CopyManager manager = new CopyManager(obj);
						{
							// do any pre copy work
							try
							{
								manager.PreCopy();
							}
							catch (Exception er)
							{
								obj.PreCopyStatus = er.Message;
								return;
							}

							// copy each table
							foreach (TableObject table in obj.Tables)
							{
								try
								{
									if (table.Selected)
									{
										manager.Copy(table);
										table.Status = "Success";
										System.Console.WriteLine("Success " + table.FullName);
									}
								}
								catch (Exception er)
								{
									table.Status = er.Message;
									System.Console.WriteLine("Error " + table.FullName);
									exitCode = -1;
								}
							}

							// kick off post copy work
							try
							{
								manager.PostCopy();
							}

							catch (Exception er)
							{
								obj.PostCopyStatus = er.Message;
								exitCode = -1;
							}

						}
					}
				}
				else
				{
					exitCode = -4;
				}
			}
			else
			{
				exitCode = -3;
			}

			Environment.Exit(exitCode);
		}		
	}
}