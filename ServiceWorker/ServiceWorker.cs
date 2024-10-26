using System;
using System.ServiceProcess;

namespace ServiceWorker
{
    public partial class ServiceWorker : ServiceBase
    {
        // Create a static logger field
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public ServiceWorker()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                Logger.Info("============ Service worker on Start ============");
            }
            catch (Exception ex)
            {
                Logger.Error(GetExceptionInfo(ex, "ServiceWorker.cs"));
            }
        }

        protected override void OnStop()
        {
            try
            {
                Logger.Info("============ Service worker on Stop ============");
            }
            catch (Exception ex)
            {
                Logger.Error(GetExceptionInfo(ex, "ServiceWorker.cs"));
            }
        }

        private string GetExceptionInfo(Exception ex, string className)
        {
            var lineNumber = 0;
            const string lineSearch = ":line ";
            var index = ex.StackTrace.LastIndexOf(lineSearch);
            if (index != -1)
            {
                var lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length);
                if (int.TryParse(lineNumberText, out lineNumber))
                {
                }
            }
            return $"{className}:line {lineNumber} | {ex.GetType()}: {ex.Message}";
        }
    }
}
