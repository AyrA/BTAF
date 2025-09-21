using System;
using System.Diagnostics;
using System.Management;
using System.ServiceProcess;
using BTAF.Lib.NativeWrapper;

namespace BTAF.Lib
{
    public static class ServiceInstallHelper
    {

        private const string ServiceName = "BTAF";

        public static bool IsInstalled
        {
            get
            {
                try
                {
                    using (var s = new ServiceController(ServiceName))
                    {
                        //Return any property to find out if the service actually exists
                        return
                            s.StartType != ServiceStartMode.Boot ||
                            s.StartType != ServiceStartMode.System;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        public static bool IsEnabled
        {
            get
            {
                try
                {
                    using (var s = new ServiceController(ServiceName))
                    {
                        return s.StartType != ServiceStartMode.Disabled;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        public static bool IsRunning
        {
            get
            {
                try
                {
                    using (var s = new ServiceController(ServiceName))
                    {
                        return s.Status == ServiceControllerStatus.StartPending || s.Status == ServiceControllerStatus.Running;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        public static void Install()
        {
            var scope = new ManagementScope(@"\\.\root\cimv2");
            var path = new ManagementPath("Win32_Service");
            var opt = new ObjectGetOptions(null, TimeSpan.MaxValue, true);

            using (var m = new ManagementClass(scope, path, opt))
            {
                var createArgs = new object[] {
                    ServiceName,
                    "Bluetooth Audio Fix",
                    $"\"{GetExeFileName()}\" /SERVICE", //Exe name should be in quotes in case the path contains spaces
                    16, //"Own Process" service type
                    0, //No special error handling
                    "Automatic", //Start mode
                    false, //No desktop interaction
                    null, //No username = SYSTEM account
                    "", //System account services do not have a password
                    //There are extra arguments here but we don't need them, and they're optional
                };
                m.InvokeMethod("Create", createArgs);
            }
            SetDescription();
        }

        public static void Uninstall()
        {
            //Stop service if still running
            try
            {
                Stop();
            }
            catch
            {
                //NOOP
            }
            using (var m = new ManagementObject($"Win32_Service.Name=\"{ServiceName}\""))
            {
                m.InvokeMethod("Delete", new object[0]);
            }
        }

        public static void Enable()
        {
            using (var manager = new ServiceControlManager())
            {
                using (var service = manager.OpenService(ServiceName))
                {
                    service.SetStartupType(ServiceStartMode.Automatic);
                }
            }
        }

        public static void Disable()
        {
            //Stop service if still running
            try
            {
                Stop();
            }
            catch
            {
                //NOOP
            }
            using (var manager = new ServiceControlManager())
            {
                using (var service = manager.OpenService(ServiceName))
                {
                    service.SetStartupType(ServiceStartMode.Disabled);
                }
            }
        }

        public static void Start()
        {
            using (var s = new ServiceController(ServiceName))
            {
                if (s.Status == ServiceControllerStatus.Running)
                {
                    return;
                }
                s.Start();
                if (s.Status == ServiceControllerStatus.StartPending)
                {
                    s.WaitForStatus(ServiceControllerStatus.Running);
                }
            }
        }

        public static void Stop()
        {
            using (var s = new ServiceController(ServiceName))
            {
                if (s.Status == ServiceControllerStatus.Stopped)
                {
                    return;
                }
                s.Stop();
                if (s.Status == ServiceControllerStatus.StopPending)
                {
                    s.WaitForStatus(ServiceControllerStatus.Stopped);
                }
            }
        }

        private static string GetExeFileName()
        {
            using (var p = Process.GetCurrentProcess())
            {
                return p.MainModule.FileName;
            }
        }

        private static void SetDescription()
        {
            const string desc = "Takes control of the bluetooth audio gateway service to fix faulty audio implementation in Windows 11 | https://github.com/AyrA/BTAF";
            using (var manager = new ServiceControlManager())
            {
                using (var service = manager.OpenService(ServiceName))
                {
                    service.SetDescription(desc);
                }
            }
        }
    }
}
