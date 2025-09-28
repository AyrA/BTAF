namespace BTAF.Lib
{
    /// <summary>
    /// Audio device monitoring mode
    /// </summary>
    public enum MonitorMode
    {
        /// <summary>
        /// Consider the audio device connected when any of the configured devices is connected
        /// </summary>
        RequireAny = 1,
        /// <summary>
        /// Consider the audio device connected when all of the configured devices are connected
        /// </summary>
        RequireAll = 2
    }
}
