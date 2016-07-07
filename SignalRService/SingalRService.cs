using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

[assembly: Microsoft.Owin.OwinStartup(typeof(SignalRService.Startup))]

namespace SignalRService
{
    public partial class SingalRService : ServiceBase
    {
        private static IDisposable webapp = null;

        public SingalRService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            this.StartServices();
        }

        protected override void OnStop()
        {
            this.StopServices();
        }

        internal void StopServices()
        {
            if (SingalRService.webapp != null)
            {
                SingalRService.webapp.Dispose();
                System.Threading.Thread.Sleep(1500);
            }
            SingalRService.webapp = null;
        }

        internal void StartServices()
        {
            if (SingalRService.webapp == null)
            {
                var uri = "https://localhost:8082";
                SingalRService.webapp = Microsoft.Owin.Hosting.WebApp.Start<SignalRService.Startup>(uri);
            }
        }
    }
}
