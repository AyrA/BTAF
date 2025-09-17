using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace BTAF.Lib.NativeWrapper
{
    internal class ServiceControlManager : IDisposable
    {
        [DllImport("Advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr OpenSCManager(IntPtr lpMachineName, IntPtr lpDatabaseName, GenericAccessRights dwDesiredAccess);

        [DllImport("Advapi32.dll")]
        private static extern IntPtr CloseServiceHandle(IntPtr handle);

        private IntPtr handle;

        public ServiceControlManager()
        {
            handle = OpenSCManager(IntPtr.Zero, IntPtr.Zero, GenericAccessRights.Read | GenericAccessRights.Write);
            if (handle == IntPtr.Zero)
            {
                throw new Win32Exception();
            }
        }

        public Service OpenService(string serviceName)
        {
            return new Service(handle, serviceName);
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
