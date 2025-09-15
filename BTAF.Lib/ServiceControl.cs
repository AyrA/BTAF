using System;
using System.Management;
using System.ServiceProcess;

namespace BTAF.Lib
{
    public static class ServiceControl
    {
        private enum StartMode
        {
            Disabled,
            Manual
        }
        private const string ServiceName = "BTAGService";

        public static void Disable()
        {
            Stop();
            SetMode(StartMode.Disabled);
        }

        public static void Enable()
        {
            SetMode(StartMode.Manual);
        }

        public static void Stop()
        {
            using (var s = GetService())
            {
                if (s.Status != ServiceControllerStatus.Stopped)
                {
                    if (s.Status == ServiceControllerStatus.StopPending)
                    {
                        s.WaitForStatus(ServiceControllerStatus.Stopped);
                    }
                    else
                    {
                        try
                        {
                            s.Stop();
                            s.WaitForStatus(ServiceControllerStatus.Stopped);
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        public static void Start()
        {
            Enable();
            using (var s = GetService())
            {
                if (s.StartType == ServiceStartMode.Disabled)
                {
                    throw new InvalidOperationException("Service cannot be started because it is disabled");
                }
                if (s.Status != ServiceControllerStatus.Running)
                {
                    if (s.Status == ServiceControllerStatus.StartPending)
                    {
                        s.WaitForStatus(ServiceControllerStatus.Running);
                    }
                    else
                    {
                        try
                        {
                            s.Start();
                            s.WaitForStatus(ServiceControllerStatus.Running);
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        private static ServiceController GetService()
        {
            return new ServiceController(ServiceName);
        }

        private static void SetMode(StartMode mode)
        {
            using (var m = new ManagementObject($"Win32_Service.Name=\"{ServiceName}\""))
            {
                m.InvokeMethod("ChangeStartMode", new object[] { mode.ToString() });
            }
        }
    }
}
