using System.Diagnostics;
using System.IO;
using System.Text;

namespace BTAF.Lib
{
    public class ConfigFile
    {
        private const byte Version = 1;
        private const string Magic = "BTAF";

        private static readonly string filename;

        public string AudioDeviceId { get; set; }

        static ConfigFile()
        {
            using (var p = Process.GetCurrentProcess())
            {
                filename = Path.Combine(Path.GetDirectoryName(Path.GetFullPath(p.MainModule.FileName)), "config.bin");
            }
        }

        public void Save()
        {
            using (var fs = File.Create(filename))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    bw.Write(Encoding.UTF8.GetBytes(Magic));
                    bw.Write(Version);
                    bw.Write(AudioDeviceId);
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
                        if (br.ReadByte() != Version)
                        {
                            throw new InvalidDataException("Invalid config file");
                        }
                        c.AudioDeviceId = br.ReadString();
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
