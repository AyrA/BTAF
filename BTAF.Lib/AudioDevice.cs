using System;

namespace BTAF.Lib
{
    public class AudioDevice
    {
        private static readonly int code = new Random().Next() << 1;

        public string Id { get; }
        public string Name { get; set; }

        private readonly int hashCode;

        internal AudioDevice(string id, string name)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name;
            hashCode = code ^ id.GetHashCode();
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
