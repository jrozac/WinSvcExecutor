using CommandLine;
using System;

namespace WinSvcExecutor
{
    /// <summary>
    /// Options base 
    /// </summary>
    public abstract class OptionsBase
    {

        /// <summary>
        /// Command line value with default
        /// </summary>
        private bool _commandLine = Environment.UserInteractive;

        /// <summary>
        /// Set command line execution
        /// </summary>
        [Option("cmd", HelpText = "Run as command line application.", Required = false)]
        public bool CommandLine { set { _commandLine = value; } get { return _commandLine; } }

    }
}
