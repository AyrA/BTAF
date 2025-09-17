using System;

namespace BTAF.Lib.NativeWrapper
{
    [Flags]
    internal enum GenericAccessRights : uint
    {
        Read = 0x80000000,
        Write = 0x40000000,
        Execute = 0x20000000,
        All = 0x10000000
    }

}
