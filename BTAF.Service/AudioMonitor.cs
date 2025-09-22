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
        private AudioRenderer renderer = null;

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
            if (string.IsNullOrEmpty(config.AudioDeviceId))
            {
                return;
            }
            try
            {
                var dev = AudioDeviceEnumerator.EnumerateDevices(true).FirstOrDefault(m => m.Id == config.AudioDeviceId);
                if (dev != null)
                {
                    triggerEvent = ServiceControl.IsRunning;
                    ServiceControl.Stop();
                    ServiceControl.Disable();
                    if (config.KeepDeviceBusy && renderer == null)
                    {
                        renderer = new AudioRenderer(config.AudioDeviceId);
                        renderer.Start();
                    }
                }
                else
                {
                    triggerEvent = !ServiceControl.IsRunning;
                    ServiceControl.Enable();
                    ServiceControl.Start();
                    if (renderer != null)
                    {
                        using (renderer)
                        {
                            renderer.Stop();
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
