using System;
using System.Collections.Generic;
using System.Linq;
using BTAF.Lib.Interop;

namespace BTAF.Lib
{
    public static class AudioDeviceEnumerator
    {
        private const string DeviceNameGuid = "B3F8FA53-0004-438E-9003-51A46E139BFC";

        private static readonly PropertyKey nameKey = new PropertyKey()
        {
            FormatId = new Guid(DeviceNameGuid),
            PropertyId = 6
        };

        public static AudioDevice GetDefaultDevice()
        {
            var instance = CreateInstance();
            var dev = instance.GetDefaultAudioEndpoint(EDataFlow.Render, ERole.Multimedia);
            if (dev == null)
            {
                return null;
            }
            var store = dev.OpenPropertyStore(2); //2=Read
            var name = store.GetValue(nameKey);
            return new AudioDevice(dev.GetId(), name.Value.ToString());
        }

        public static IEnumerable<AudioDevice> EnumerateDevices(bool activeOnly)
        {
            return EnumerateDevicesInternal(activeOnly).Select(item =>
            {
                var store = item.OpenPropertyStore(2); //2=Read
                var name = store.GetValue(nameKey);
                return new AudioDevice(item.GetId(), name.Value.ToString());
            });
        }

        internal static IEnumerable<IMMDevice> EnumerateDevicesInternal(bool activeOnly)
        {
            var instance = CreateInstance();
            var enumerator = instance.EnumAudioEndpoints(EDataFlow.Render, activeOnly ? EDeviceState.Active : EDeviceState.All);

            for (var i = 0; i < enumerator.GetCount(); i++)
            {
                yield return enumerator.Item(i);
            }
        }

        private static IMMDeviceEnumerator CreateInstance()
        {
            var type = Type.GetTypeFromCLSID(new Guid(Guids.MMDeviceEnumeratorCLSIDString));
            return (IMMDeviceEnumerator)Activator.CreateInstance(type);
        }
    }
}
