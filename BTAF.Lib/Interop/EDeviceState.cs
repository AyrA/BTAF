using System;

namespace BTAF.Lib.Interop
{
    /// <summary>
    /// EDeviceState enumeration
    /// </summary>
    [Flags]
    internal enum EDeviceState
    {
        /// <summary>
        /// The audio endpoint device is active.
        /// That is, the audio adapter that connects to the endpoint device is present and enabled.
        /// In addition, if the endpoint device plugs into a jack on the adapter,
        /// then the endpoint device is plugged in.
        /// </summary>
        Active = 0x1,
        /// <summary>
        /// The audio endpoint device is disabled.
        /// The user has disabled the device in the Windows multimedia control panel, Mmsys.cpl.
        /// </summary>
        Disabled = 0x2,
        /// <summary>
        /// The audio endpoint device is not present because the audio adapter
        /// that connects to the endpoint device has been removed from the system,
        /// or the user has disabled the adapter device in Device Manager.
        /// </summary>
        NotPresent = 0x4,
        /// <summary>
        /// The audio endpoint device is unplugged.
        /// The audio adapter that contains the jack for the endpoint device is present and enabled,
        /// but the endpoint device is not plugged into the jack.
        /// Only a device with jack-presence detection can be in this state.
        /// For more information about jack-presence detection,
        /// see https://learn.microsoft.com/en-us/windows/win32/coreaudio/audio-endpoint-devices
        /// </summary>
        Unplugged = 0x8,
        /// <summary>
        /// Includes audio endpoint devices in all states active, disabled, not present, and unplugged.
        /// </summary>
        All = 0x0F
    }
}
