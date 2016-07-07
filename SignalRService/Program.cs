using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SignalRService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (System.Environment.UserInteractive)
            {
                var Form = new RunForm();
                Form.ShowDialog();
            }
            else
            {
                var ServicesToRun = new ServiceBase[] { new SingalRService() };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
