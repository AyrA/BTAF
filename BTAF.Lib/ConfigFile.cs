using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace BTAF.Lib
{
    public class ConfigFile
    {
        private const byte CurrentVersion = 1;
        private const string Magic = "BTAF";

        public const int MaxAudioDeviceCount = 100;

        private static readonly string filename;

        /// <summary>
        /// The id of the device being monitored
        /// </summary>
        public AudioDevice[] AudioDevices { get; set; }

        /// <summary>
        /// Audio device monitoring mode.
        /// </summary>
        public MonitorMode MonitorMode { get; set; }

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
            if (AudioDevices == null || AudioDevices.Length == 0)
            {
                throw new InvalidOperationException("At least one audio device must be specified");
            }
            if (AudioDevices.Length > MaxAudioDeviceCount) //Something's fishy if the user tries to save that many devices
            {
                throw new InvalidOperationException($"Excessive number of audio devices. Maximum is {MaxAudioDeviceCount}");
            }
            using (var fs = File.Create(filename))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    bw.Write(Encoding.UTF8.GetBytes(Magic));
                    bw.Write(CurrentVersion);
                    bw.Write((ushort)AudioDevices.Length);
                    foreach (var dev in AudioDevices)
                    {
                        dev.SaveToConfig(bw);
                    }
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
                        var devCount = br.ReadUInt16();
                        if (devCount > MaxAudioDeviceCount)
                        {
                            throw new InvalidDataException("Invalid config file. Too many audio devices");
                        }
                        c.AudioDevices = new AudioDevice[devCount];
                        for (var i = 0; i < devCount; i++)
                        {
                            var dev = new AudioDevice();
                            dev.LoadFromConfig(br);
                            c.AudioDevices[i] = dev;
                        }
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

        /// <summary>
        /// Checks if the audio device is considered ready according to the configured values
        /// </summary>
        /// <returns>
        /// true if the audio device is ready, and the bluetooth audio gateway service can be shut down,
        /// false if the bluetooth audio gateway service should be running
        /// </returns>
        public bool AudioDeviceReady()
        {
            //No device = No action
            if (AudioDevices == null || AudioDevices.Length == 0)
            {
                return false;
            }
            var ids = AudioDevices.Select(m => m.Id).ToArray();
            var devs = AudioDeviceEnumerator.EnumerateDevices(true).Select(m => m.Id).ToArray();

            //Require all devices
            if (MonitorMode == MonitorMode.RequireAll)
            {
                return ids.All(m => devs.Contains(m, StringComparer.InvariantCultureIgnoreCase));
            }

            //Require one device
            foreach (var id in devs)
            {
                if (ids.Contains(id, StringComparer.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets all monitored audio devices that are ready
        /// </summary>
        /// <returns>Audio device list</returns>
        public IEnumerable<AudioDevice> GetReadyAudioDevices()
        {
            if (AudioDevices != null && AudioDevices.Length > 0)
            {
                var ids = AudioDevices.Select(m => m.Id).ToArray();
                return AudioDeviceEnumerator.EnumerateDevices(true).Where(m => ids.Contains(m.Id, StringComparer.InvariantCultureIgnoreCase));
            }
            return Array.Empty<AudioDevice>();
        }
    }
}
