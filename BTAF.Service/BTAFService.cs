using System.ServiceProcess;

namespace BTAF.Service
{
    public partial class BTAFService : ServiceBase
    {
        public BTAFService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
