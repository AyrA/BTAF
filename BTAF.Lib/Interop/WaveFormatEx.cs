using System.Runtime.InteropServices;

namespace BTAF.Lib.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct WaveFormatEx
    {
        readonly ushort wFormatTag;
        readonly ushort nChannels;
        readonly uint nSamplesPerSec;
        readonly uint nAvgBytesPerSec;
        readonly ushort nBlockAlign;
        readonly ushort wBitsPerSample;
        readonly ushort cbSize;
    }
}
