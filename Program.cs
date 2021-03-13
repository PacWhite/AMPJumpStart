using System;
using System.Diagnostics;
using System.IO;

namespace JumpStart
{
    class Program
    {
        static string _template = "config.jump";
        static string _templateContent;
        static Process proc = new Process();
        static string ProcessName;
        static void Main(string[] args)
        {
            Console.WriteLine("Unoffical CubeCoders AMP JumpStart - an Process for the processes!");
            
            if (!File.Exists(_template))
            {
                Console.WriteLine("No Jumpstart Config found - Copy the main config of your app and rename it to config.jump or select via --template=filename.ext");
                Environment.Exit(0);
            }
            else
            {
                _templateContent = File.ReadAllText(_template);
            }
            CreateConfig(args);
            StartAndReadProcess();
        }

        static void CreateConfig(string[] arguments)
        {
            Console.WriteLine("Create JumpStart Config....");
            string configFileName = "";
            string[] seperators = new string[] { "[", "]" };
            foreach(string arg in arguments)
            {
                string[] splittetArg = arg.Split("=", StringSplitOptions.RemoveEmptyEntries);
                if(splittetArg[0] == "--config")
                {
                    configFileName = splittetArg[1];
                    Console.WriteLine("Configfile: {0}", splittetArg[1]);
                }
                else if(splittetArg[0] == "--serverExe")
                {
                    ProcessName = splittetArg[1];
                    Console.WriteLine("ProcessName: {0}", splittetArg[1]);
                }
                else if(splittetArg[0] == "--template")
                {
                    _template = splittetArg[1];
                    _templateContent = File.ReadAllText(_template);
                }
                else if(splittetArg[0] == "--seperator")
                {
                    seperators = splittetArg[1].Split("|");
                    Console.WriteLine("Seperators: {0}|{1}", seperators[0], seperators[1]);
                }
                else
                {
                    splittetArg[0] = seperators[0] + splittetArg[0].Replace("--", "") + seperators[1];
                    _templateContent = _templateContent.Replace(splittetArg[0], splittetArg[1]);
                }
            }
            File.WriteAllText(configFileName, _templateContent);
        }

        static void StartAndReadProcess()
        {
            
            proc.EnableRaisingEvents = true;
            proc.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(process_OutputDataReceived);
            proc.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(process_ErrorDataReceived);
            proc.Exited += new System.EventHandler(process_Exited);
            proc.StartInfo.FileName = ProcessName;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            if(File.Exists("arguments.jump"))
            {
                Console.WriteLine("Arguments: {0}", File.ReadAllText("arguments.jump"));
                proc.StartInfo.Arguments = File.ReadAllText("arguments.jump");
            }
            else
            {
                Console.WriteLine("Arguments: No argument.jump found.");
            }
            proc.Start();
            proc.BeginOutputReadLine();
            proc.WaitForExit();
        }

        private static void process_Exited(object sender, EventArgs e)
        {
            proc.Close();
        }
        private static void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }
        private static void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }
    }
}
