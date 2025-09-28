using System;
using System.IO;

namespace BTAF.Lib
{
    public class AudioDevice
    {
        private static readonly int code = new Random().Next() << 1;

        public string Id { get; private set; }

        public string Name { get; private set; }

        private int hashCode;

        internal AudioDevice() : this(string.Empty, string.Empty)
        {

        }

        internal AudioDevice(string id, string name)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name;
            hashCode = code ^ id.GetHashCode();
        }

        internal void LoadFromConfig(BinaryReader reader)
        {
            Id = reader.ReadString();
            Name = reader.ReadString();
            hashCode = code ^ Id.GetHashCode();
        }

        internal void SaveToConfig(BinaryWriter writer)
        {
            writer.Write(Id);
            writer.Write(Name);
        }

        public override bool Equals(object obj)
        {
            if (obj is AudioDevice dev)
            {
                return dev.Id.Equals(Id, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return hashCode;
        }
    }
}
