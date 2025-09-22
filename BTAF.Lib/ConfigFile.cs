using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace BTAF.Lib
{
    public class ConfigFile
    {
        private const byte CurrentVersion = 1;
        private const string Magic = "BTAF";

        private static readonly string filename;

        /// <summary>
        /// The id of the device being monitored
        /// </summary>
        public string AudioDeviceId { get; set; }

        /// <summary>
        /// True to keep the device busy at all times
        /// </summary>
        /// <remarks>
        /// This might be necessary when the beginning of sound is cut off,
        /// or a few milliseconds of garbage (from the previous audio session) plays
        /// </remarks>
        public bool KeepDeviceBusy { get; set; }

        static ConfigFile()
        {
            using (var p = Process.GetCurrentProcess())
            {
                filename = Path.Combine(Path.GetDirectoryName(Path.GetFullPath(p.MainModule.FileName)), "config.bin");
            }
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(AudioDeviceId))
            {
                throw new InvalidOperationException("Audio device not specified");
            }
            using (var fs = File.Create(filename))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    bw.Write(Encoding.UTF8.GetBytes(Magic));
                    bw.Write(CurrentVersion);
                    bw.Write(AudioDeviceId);
                    bw.Write(KeepDeviceBusy);
                }
            }
        }

        public static ConfigFile Load()
        {
            var c = new ConfigFile();
            try
            {
                using (var fs = File.OpenRead(filename))
                {
                    using (var br = new BinaryReader(fs))
                    {
                        if (Encoding.UTF8.GetString(br.ReadBytes(4)) != Magic)
                        {
                            throw new InvalidDataException("Invalid config file");
                        }
                        var version = br.ReadByte();
                        if (version == 0 || version > CurrentVersion)
                        {
                            throw new InvalidDataException("Invalid config file. Version not supported");
                        }
                        c.AudioDeviceId = br.ReadString();
                        c.KeepDeviceBusy = br.ReadBoolean();
                    }
                }
            }
            catch
            {
                return new ConfigFile();
            }
            return c;
        }
    }
}
