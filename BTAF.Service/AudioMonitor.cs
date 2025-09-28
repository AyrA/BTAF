using System;
using System.Linq;
using System.Threading;
using BTAF.Lib;

namespace BTAF.Service
{
    internal class AudioMonitor : IDisposable
    {
        public delegate void AudioGatewayServiceChangeEventHandler(object sender, bool isRunning);

        public event AudioGatewayServiceChangeEventHandler AudioGatewayServiceChange = delegate { };

        private readonly Timer t;
        private ConfigFile config;
        private AudioRenderer[] renderer = null;

        public bool IsRunning { get; private set; }

        public AudioMonitor()
        {
            t = new Timer(TimerAction);
            config = ConfigFile.Load();
            IsRunning = false;
        }

        public void ReloadConfig()
        {
            config = ConfigFile.Load();
        }

        public void Start()
        {
            IsRunning = true;
            t.Change(0, 5000);
        }

        public void Stop()
        {
            t.Change(Timeout.Infinite, Timeout.Infinite);
            IsRunning = false;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            t.Dispose();
        }

        private void TimerAction(object state)
        {
            bool triggerEvent = false;
            if (config.AudioDevices == null || config.AudioDevices.Length == 0)
            {
                return;
            }
            try
            {
                if (config.AudioDeviceReady())
                {
                    triggerEvent = ServiceControl.IsRunning;
                    ServiceControl.Stop();
                    ServiceControl.Disable();
                    if (config.KeepDeviceBusy && renderer == null)
                    {
                        renderer = config.GetReadyAudioDevices().Select(m => new AudioRenderer(m.Id)).ToArray();
                        foreach (var r in renderer)
                        {
                            r.Start();
                        }
                    }
                }
                else
                {
                    triggerEvent = !ServiceControl.IsRunning;
                    ServiceControl.Enable();
                    ServiceControl.Start();
                    if (renderer != null)
                    {
                        foreach (var r in renderer)
                        {
                            using (r)
                            {
                                r.Stop();
                            }
                        }
                        renderer = null;
                    }
                }
            }
            catch
            {
                //NOOP
            }
            if (triggerEvent)
            {
                AudioGatewayServiceChange(this, ServiceControl.IsRunning);
            }
        }
    }
}
