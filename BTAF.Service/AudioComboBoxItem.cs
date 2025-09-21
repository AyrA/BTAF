using BTAF.Lib;

namespace BTAF.Service
{
    internal class AudioComboBoxItem
    {
        public AudioDevice Device { get; }

        public AudioComboBoxItem(AudioDevice dev)
        {
            Device = dev;
        }

        public override string ToString()
        {
            return Device.Name;
        }
    }
}
