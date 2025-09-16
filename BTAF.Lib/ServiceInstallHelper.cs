using System;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;
using System.ServiceProcess;

namespace BTAF.Lib
{
    public static class ServiceInstallHelper
    {
        [DllImport("Advapi32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr OpenSCManager(IntPtr lpMachineName, IntPtr lpDatabaseName, GenericAccessRights dwDesiredAccess);

        [DllImport("Advapi32.dll")]
        private static extern IntPtr CloseServiceHandle(IntPtr handle);

        [DllImport("Advapi32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr OpenService(IntPtr hSCManager, [MarshalAs(UnmanagedType.LPWStr)] string lpServiceName, GenericAccessRights dwDesiredAccess);

        [DllImport("Advapi32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr ChangeServiceConfig2(IntPtr hService, ServiceInfoLevel dwInfoLevel, IntPtr lpInfo);

        private const string ServiceName = "BTAF";

        [StructLayout(LayoutKind.Sequential)]
        private struct ServiceDescriptionW
        {
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpDescription;
        }

        private enum ServiceInfoLevel : uint
        {
            Description = 1,
            FailureActions = 2,
            DelayedAutoStartInfo = 3,
            FailureActionsFlag = 4,
            ServiceSidInfo = 5,
            RequiredPrivilegesInfo = 6,
            PreshutdownInfo = 7,
            TriggerInfo = 8,
            PreferredNode = 9,
            LaunchProtected = 12
        }

        [Flags]
        private enum GenericAccessRights : uint
        {
            Read = 0x80000000,
            Write = 0x40000000,
            Execute = 0x20000000,
            All = 0x10000000
        }

        public static bool IsInstalled
        {
            get
            {
                try
                {
                    using (new ServiceController(ServiceName))
                    {
                        //NOOP
                    }
                    return true;
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
                    GetExeFileName(),
                    16, //"Own Process" service type
                    0, //No special error handling
                    "Automatic", //Start mode
                    false, //No desktop interaction
                    null, //No username = SYSTEM account
                    "", //System account services do not have a password
                };
                m.InvokeMethod("Create", createArgs);
            }
            SetDescription();
        }

        public static void Uninstall()
        {
            using (var m = new ManagementObject($"Win32_Service.Name=\"{ServiceName}\""))
            {
                m.InvokeMethod("Delete", new object[0]);
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
            var manager = OpenSCManager(IntPtr.Zero, IntPtr.Zero, GenericAccessRights.Write | GenericAccessRights.Read | (GenericAccessRights)0xF003F);
            if (manager == IntPtr.Zero)
            {
                throw new Exception("Failed to open service database");
            }
            try
            {
                var service = OpenService(manager, ServiceName, GenericAccessRights.Read | GenericAccessRights.Write);
                if (service == IntPtr.Zero)
                {
                    throw new Exception("Failed to open service");
                }
                try
                {
                    var desc = new ServiceDescriptionW()
                    {
                        lpDescription = "Takes control of the bluetooth audio gateway service to fix faulty audio implementation in Windows 11"
                    };
                    IntPtr mem = Marshal.AllocHGlobal(Marshal.SizeOf(desc));
                    if (mem == IntPtr.Zero)
                    {
                        throw new Exception("Unable to allocate memory");
                    }
                    try
                    {
                        Marshal.StructureToPtr(desc, mem, false);
                        try
                        {
                            ChangeServiceConfig2(service, ServiceInfoLevel.Description, mem);
                        }
                        finally
                        {
                            Marshal.DestroyStructure(mem, typeof(ServiceDescriptionW));
                            Marshal.FreeHGlobal(mem);
                        }
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(mem);
                    }
                }
                finally
                {
                    CloseServiceHandle(service);
                }
            }
            finally
            {
                CloseServiceHandle(manager);
            }
        }
    }
}
