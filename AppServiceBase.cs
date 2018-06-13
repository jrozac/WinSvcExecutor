namespace WinSvcExecutor
{

    /// <summary>
    /// Application service base definition
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    public abstract class AppServiceBase<TOptions> where TOptions : OptionsBase
    {

        /// <summary>
        /// Options 
        /// </summary>
        public TOptions Options { get; protected set; }

        /// <summary>
        /// Pre-initialization. It setups the service.
        /// </summary>
        /// <param name="options"></param>
        public void PreInit(TOptions options)
        {
            Options = options;
        }

        /// <summary>
        /// It is called to init a service. 
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// It is called to stop a service.
        /// </summary>
        public abstract void Stop();

        /// <summary>
        /// It is called to start a service.
        /// </summary>
        /// <returns></returns>
        public abstract bool Start();

        /// <summary>
        /// CleanUp procedure. Put your cleanup code here.
        /// Function is automatically called when service start fails.
        /// </summary>
        public virtual void CleanUp() { }

    }

}
