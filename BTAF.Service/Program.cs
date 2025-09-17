using System;
using System.ServiceProcess;
using BTAF.Lib;

namespace BTAF.Service
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                RunService();
            }
            else
            {
                switch (args[0].ToUpperInvariant())
                {
                    case "/INSTALL":
                        ServiceInstallHelper.Install();
                        break;
                    case "/UNINSTALL":
                        ServiceInstallHelper.Uninstall();
                        break;
                    case "/CONFIG":
                        Configurator.Run();
                        break;
                    default:
                        Console.WriteLine("BTAF.Service /INSTALL|/UNINSTALL|/CONFIG");
                        break;
                }
                return;
            }
        }

        private static void RunService()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new BTAFService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
