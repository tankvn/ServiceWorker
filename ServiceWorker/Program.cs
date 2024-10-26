using System.ServiceProcess;

namespace ServiceWorker
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ServiceWorker()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
