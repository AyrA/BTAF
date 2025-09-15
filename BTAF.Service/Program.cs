using System.Diagnostics;
using BTAF.Lib;

namespace BTAF.Service
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if DEBUG
            Debug.Print("## Debug session start");
            foreach (var ad in AudioDevices.GetDeviceNames())
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
    }
}
