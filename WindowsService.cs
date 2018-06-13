using log4net;
using System;
using System.ServiceProcess;

namespace WinSvcExecutor
{

    /// <summary>
    /// Windows service. Redefines default service behaviour.
    /// </summary>
    public partial class WindowsService<TAppService, TOptions> : ServiceBase
        where TAppService : AppServiceBase<TOptions>
        where TOptions : OptionsBase
    {

        /// <summary>
        /// Logger
        /// </summary>
        private ILog Logger = LogManager.GetLogger(string.Format("WindowsService-{0}", typeof(TAppService).Name));

        /// <summary>
        /// App service 
        /// </summary>
        private TAppService _appService;

        /// <summary>
        /// Constructor with appService
        /// </summary>
        /// <param name="appService"></param>
        public WindowsService(TAppService appService) : base()
        {
            _appService = appService;
        }

        /// <summary>
        /// Service start 
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            // try to start service 
            bool status = false;
            try
            {
                status = _appService.Start();
            }
            catch (Exception e)
            {
                Logger.ErrorFormat("Failed to start with Exception {0}: {1}.", e.GetType().Name, e.Message);
            }

            // stop service 
            if (!status)
            {
                Logger.ErrorFormat("Failed to start. Will clean up and stop.");
                _appService.CleanUp();
                Stop();
            }
        }

        /// <summary>
        /// Service stop 
        /// </summary>
        protected override void OnStop()
        {
            _appService.Stop();
        }

    }
}
