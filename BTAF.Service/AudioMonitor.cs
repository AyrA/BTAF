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
        private readonly string deviceId;

        public bool IsRunning { get; private set; }

        public AudioMonitor()
        {
            t = new Timer(TimerAction);
            deviceId = ConfigFile.Load().AudioDeviceId;
            IsRunning = false;
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
            if (string.IsNullOrEmpty(deviceId))
            {
                return;
            }
            try
            {
                var dev = AudioDeviceEnumerator.EnumerateDevices(true).FirstOrDefault(m => m.Id == deviceId);
                if (dev != null)
                {
                    triggerEvent = ServiceControl.IsRunning;
                    ServiceControl.Stop();
                    ServiceControl.Disable();
                }
                else
                {
                    triggerEvent = !ServiceControl.IsRunning;
                    ServiceControl.Enable();
                    ServiceControl.Start();
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
