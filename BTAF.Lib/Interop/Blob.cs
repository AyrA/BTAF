using System;

namespace BTAF.Lib.Interop
{
    /// <summary>
    /// Blob data structure.
    /// </summary>
    internal struct Blob
    {
        /// <summary>
        /// Blob data size.
        /// </summary>
        internal int Size;

        /// <summary>
        /// Blob data.
        /// </summary>
        internal IntPtr Data;
    }
}
