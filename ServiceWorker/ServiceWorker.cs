using System.ServiceProcess;

namespace ServiceWorker
{
    public partial class ServiceWorker : ServiceBase
    {
        public ServiceWorker()
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
