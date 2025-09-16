using System.Runtime.InteropServices;

namespace BTAF.Lib.Interop
{
    /// <summary>
    /// Native methods.
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// Frees all elements that can be freed in a given PropVariant structure.
        /// </summary>
        /// <param name="pvar">
        /// A reference to an initialized PropVariant structure for which any deallocatable elements
        /// are to be freed. On return, all zeroes are written to the PROPVARIANT structure.
        /// </param>
        /// <returns>
        /// S_OK (0) if the VT types are recognized and all items that can be freed have been freed.
        /// STG_E_INVALIDPARAMETER (0x80030057) if the variant has an unknown VT type.
        /// </returns>
        [DllImport("ole32.dll")]
        internal static extern int PropVariantClear(ref PropVariant pvar);
    }
}
