using System;
using System.ServiceProcess;

namespace MCD.RLPlanning.WinService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun = new ServiceBase[] 
            { 
                new EmailService() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}