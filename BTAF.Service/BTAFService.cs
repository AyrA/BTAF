using System.ServiceProcess;

namespace BTAF.Service
{
    public partial class BTAFService : ServiceBase
    {
        private readonly AudioMonitor monitor;

        public BTAFService()
        {
            InitializeComponent();
            monitor = new AudioMonitor();
        }

        protected override void OnStart(string[] args)
        {
            monitor.Start();
        }

        protected override void OnStop()
        {
            monitor.Stop();
        }
    }
}
