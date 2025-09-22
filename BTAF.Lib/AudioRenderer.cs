using System;
using System.Linq;
using BTAF.Lib.Interop;

namespace BTAF.Lib
{
    public class AudioRenderer : IDisposable
    {
        private const long ReftimesPerSec = 10000000;

        private static readonly Guid audioClientId = new Guid(Guids.IAudioClientIIDString);
        private static readonly Guid audioRendererId = new Guid(Guids.IAudioRenderClientIIDString);
        private readonly IMMDevice dev;
        private readonly IAudioClient audioClient;
        private readonly IAudioRenderClient audioRenderClient;
        private bool isRunning;

        public AudioRenderer(string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentException("Device id cannot be empty or null", nameof(deviceId));
            }
            dev = AudioDeviceEnumerator.EnumerateDevicesInternal(true).FirstOrDefault(m => m.GetId() == deviceId)
                ?? throw new ArgumentException("Device not found", nameof(deviceId));

            //Step 1: Get audio client
            Guid g = audioClientId;
            audioClient = (IAudioClient)dev.Activate(ref g, ClsCtx.ALL, IntPtr.Zero);
            if (audioClient == null)
            {
                throw new ArgumentException("Specified audio device cannot be activated", nameof(deviceId));
            }

            //Step 2: Begin playback session with default format
            var format = audioClient.GetMixFormat();
            Guid session = Guid.Empty;
            var initResult = audioClient.Initialize(AudioClientShareMode.Shared, AudioClientStreamFlags.None, ReftimesPerSec, 0, format, ref session);
            if (initResult != 0)
            {
                throw new ArgumentException("Unable to initialize specified audio device", nameof(deviceId));
            }

            //Step 3: Prepare audio renderer
            g = audioRendererId;
            audioRenderClient = (IAudioRenderClient)audioClient.GetService(ref g);
            if (audioRenderClient == null)
            {
                throw new ArgumentException("Unable to create audio renderer for the specified audio device", nameof(deviceId));
            }

            //Step 4: Initialize buffer
            int bufferSize = audioClient.GetBufferSize();
            var mem = audioRenderClient.GetBuffer(bufferSize);
            audioRenderClient.ReleaseBuffer(bufferSize, 0);
        }

        public void Start()
        {
            lock (audioClient)
            {
                if (!isRunning)
                {
                    isRunning = true;
                    audioClient.Start();
                }
            }
        }

        public void Stop()
        {
            lock (audioClient)
            {
                if (isRunning)
                {
                    isRunning = false;
                    audioClient.Stop();
                }
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Stop();
        }
    }
}
