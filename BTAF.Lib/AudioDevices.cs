using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace BTAF.Lib
{
    public static class AudioDevices
    {
        private const int MAXPNAMELEN = 32;

        [DllImport("winmm.dll", EntryPoint = "waveOutGetDevCaps", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint GetAudioDeviceInfo(IntPtr hwo, ref WAVEOUTCAPS pwoc, uint cbwoc);

        [DllImport("winmm.dll", EntryPoint = "waveOutGetNumDevs", SetLastError = true)]
        private static extern uint GetAudioDeviceCount();

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct WAVEOUTCAPS
        {
            public short wMid;
            public short wPid;
            public int vDriverVersion;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAXPNAMELEN)]
            public string szPname;

            public int dwFormats;
            public short wChannels;
            public short wReserved;
            public int dwSupport;
        }

        public static AudioDevice[] GetDeviceNames()
        {
            var ret = new List<AudioDevice>();
            var count = GetAudioDeviceCount();
            for (var i = 0; i < count; i++)
            {
                var buffer = new WAVEOUTCAPS();
                var size = (uint)Marshal.SizeOf(typeof(WAVEOUTCAPS));
                GetAudioDeviceInfo((IntPtr)i, ref buffer, size);
                ret.Add(new AudioDevice(i.ToString(), buffer.szPname));
            }
            return ret.ToArray();
        }
    }
}
