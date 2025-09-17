using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.ServiceProcess;

namespace BTAF.Lib.NativeWrapper
{
    internal class Service : IDisposable
    {
        [DllImport("Advapi32.dll")]
        private static extern IntPtr CloseServiceHandle(IntPtr handle);

        [DllImport("Advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr OpenService(IntPtr hSCManager, [MarshalAs(UnmanagedType.LPWStr)] string lpServiceName, GenericAccessRights dwDesiredAccess);

        [DllImport("Advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr ChangeServiceConfig2(IntPtr hService, ServiceInfoLevel dwInfoLevel, IntPtr lpInfo);

        [DllImport("Advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int ChangeServiceConfig(IntPtr hService, uint dwServiceType, ServiceStartMode dwStartType, uint dwErrorControl,
            [MarshalAs(UnmanagedType.LPWStr)]
            string lpBinaryPathName,
            [MarshalAs(UnmanagedType.LPWStr)]
            string lpLoadOrderGroup,
            IntPtr lpdwTagId, //Should be "out int" but we're not using this
            [MarshalAs(UnmanagedType.LPWStr)]
            string lpDependencies,
            [MarshalAs(UnmanagedType.LPWStr)]
            string lpServiceStartName,
            [MarshalAs(UnmanagedType.LPWStr)]
            string lpPassword,
            [MarshalAs(UnmanagedType.LPWStr)]
            string lpDisplayName);

        private IntPtr handle;

        [StructLayout(LayoutKind.Sequential)]
        private struct ServiceDescriptionW
        {
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpDescription;
        }

        internal Service(IntPtr manager, string serviceName)
        {
            handle = OpenService(manager, serviceName, GenericAccessRights.Read | GenericAccessRights.Write);
            if (handle == IntPtr.Zero)
            {
                throw new Win32Exception();
            }
        }

        public void SetDescription(string description)
        {
            var desc = new ServiceDescriptionW()
            {
                lpDescription = description
            };
            IntPtr mem = Marshal.AllocHGlobal(Marshal.SizeOf(desc));
            if (mem == IntPtr.Zero)
            {
                throw new OutOfMemoryException("Unable to allocate memory for service description");
            }
            try
            {
                Marshal.StructureToPtr(desc, mem, false);
                try
                {
                    ChangeServiceConfig2(handle, ServiceInfoLevel.Description, mem);
                }
                finally
                {
                    Marshal.DestroyStructure(mem, typeof(ServiceDescriptionW));
                }
            }
            finally
            {
                Marshal.FreeHGlobal(mem);
            }
        }

        public void SetStartupType(ServiceStartMode startType)
        {
            var res = ChangeServiceConfig(
                handle, 0x10 /*SERVICE_OWN_PROCESS*/, startType, 0x1/*SERVICE_ERROR_NORMAL*/,
                null, null, IntPtr.Zero, null, null, null, null);
            if (res == 0)
            {
                throw new Win32Exception();
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (handle != IntPtr.Zero)
            {
                CloseServiceHandle(handle);
                handle = IntPtr.Zero;
            }
        }
    }
}
