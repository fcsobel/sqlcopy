using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using c3o.SqlCopy.Data;
using c3o.SqlCopy.Objects;
using CommandLine;

namespace c3o.SqlCopy.Console1.App
{
	class Program
	{


		class Options
		{
			//[Option('r', "read", Required = true,
			//  HelpText = "Input files to be processed.")]
			//public IEnumerable<string> InputFiles { get; set; }

			//// omitting long name, default --verbose
			//[Option(DefaultValue = true,
			//  HelpText = "Prints all messages to standard output.")]
			//public bool Verbose { get; set; }

			[Value(0)]
			public string FileName { get; set; }
		}


		//// Consume them
		//static void Main(string[] args) {
		//  var options = new Options();
		//  if (CommandLine.Parser.Default.ParseArguments(args, options)) {
		//	// Values are available here
		//	if (options.Verbose) Console.WriteLine("Filename: {0}", options.InputFile);
		//  }
		//}

		//https://commandline.codeplex.com/

		static void Main(string[] args)
		{
			int exitCode = 0;

			// pare the command line arguments
			var result = CommandLine.Parser.Default.ParseArguments<Options>(args);

			// if there are no errors
			if (!result.Errors.Any())
			{

				// Values are available here
				string filename = result.Value.FileName;

				if (!string.IsNullOrEmpty(filename))
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
										//table.Status = "Success";
										table.CopyStatus = CopyStatusEnum.Success;
										System.Console.WriteLine("Success " + table.Name);
									}
								}
								catch (Exception er)
								{
									//table.Status = er.Message;
									table.Message = er.Message;
									table.CopyStatus = CopyStatusEnum.Error;
									System.Console.WriteLine("Error " + table.Name);
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
