using System.Diagnostics;
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
#if DEBUG
                    case "/TEST":
                        //AudioDeviceEnumerator.EnumerateDevices();
                        break;
#endif
                }
            }
#if DEBUG
            Debug.Print("## Debug session start");
            foreach (var ad in AudioDeviceEnumerator.EnumerateDevices(true))
            {
                Debug.Print("Id={0}; Name={1}", ad.Id, ad.Name);
            }
            ServiceControl.Stop();
            Debug.Print("## Debug session end");
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new BTAFService()
            };
            ServiceBase.Run(ServicesToRun);
#endif
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
