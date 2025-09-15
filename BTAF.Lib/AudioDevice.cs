namespace BTAF.Lib
{
    public class AudioDevice
    {
        public string Id { get; }
        public string Name { get; }

        internal AudioDevice(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
