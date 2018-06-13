using CommandLine;
using System;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace WinSvcExecutor
{

    /// <summary>
    /// Application runner. Main class that runs the application 
    /// </summary>
    public static class AppRunner
    {

        /// <summary>
        /// Read arguments options
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public static TOptions ReadOptions<TOptions>(string[] args) where TOptions : OptionsBase
        {
            TOptions options = null;
            Parser.Default.ParseArguments<TOptions>(args).WithParsed(opts => options = opts);
            return options;
        }


        public static void Run<TAppService, TOptions>(string[] args)
        {

        

        }

        /// <summary>
        /// Runs application service
        /// </summary>
        /// <typeparam name="TAppService"></typeparam>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="appServiceCreate">Function for custom application service creation. 
        /// It is handy in case when IOC provider is used. 
        /// If set to null, a default constructor without parameters will be used to create a new instance.</param>
        /// <param name="args">Command line arguments</param>
        public static void Run<TAppService, TOptions>(Func<TAppService> appServiceCreate, string[] args)
            where TAppService : AppServiceBase<TOptions> where TOptions : OptionsBase
        {

            // read options 
            var options = ReadOptions<TOptions>(args);
            if (options == null)
            {
                return;
            }

            // create app service 
            TAppService app = appServiceCreate != null ? appServiceCreate() : Activator.CreateInstance<TAppService>();

            // init app
            app.PreInit(options);
            app.Init();

            // run as command line 
            if (Environment.UserInteractive || options.CommandLine)
            {
                // start
                Console.WriteLine("Starting app service {0}.", typeof(TAppService).Name);
                bool status = app.Start();

                // service started
                if (status)
                {
                    Console.WriteLine("App service started.");

                    // wait for stop line
                    Task.Run(() =>
                    {
                        Console.WriteLine("Press Enter to stop the service.");
                        Console.ReadLine();
                    }).GetAwaiter().GetResult();

                    // stop service
                    Console.WriteLine("Stopping app service");
                    app.Stop();
                    Console.WriteLine("App service stopped.");

                }
                else // failed to start 
                {
                    Console.WriteLine("App service failed to start.");
                }

            } // run as windows service 
            else
            {
                var service = new WindowsService<TAppService, TOptions>(app);
                ServiceBase.Run(service);
            }
        }

    }

}
