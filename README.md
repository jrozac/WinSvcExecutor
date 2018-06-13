# WinSvcExecutor
This library makes it easy to create Windows Services for simple command line applications, which can run as a Windows Service or a command line application.

## Usage 
First add the following dependencies to your project (see packages.xml) for details:
* CommandLineParser
* log4Net

Then create two classes: 
* **Options**. It has to extend the **OptionsBaseClass**
* **AppService**. It has to extnd the **AppServiceBase Class**

When done, just run the application through **AppRunner** static class. The example below shows an example of a windows service which does nothing, but it starts. See `https://github.com/jrozac/TcpDemoServerSvc` for a complete example.

```

class Options : OptionsBase
{
    [Option("port", HelpText = "Server port", Required = true)]
    public int Port { get; set; }
}

class AppService : AppServiceBase<Options>
{

    /// <summary>
    /// Initialize 
    /// </summary>
    public override void Init()
    {

    }

    /// <summary>
    /// Service start
    /// </summary>
    /// <returns></returns>
    public override bool Start()
    {

    }

    /// <summary>
    /// Server stop 
    /// </summary>
    public override void Stop()
    {
   
    }

}

class Program
{
    static void Main(string[] args)
    {
        // run application
        AppRunner.Run<AppService, Options>(null, args);

        // run application with custom app service initialization (handy for IOC resolving)
        // AppRunner.Run<AppService, Options>(() => { return new AppService(); }, args);
    }
}

```
