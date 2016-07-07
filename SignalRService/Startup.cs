﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

namespace SignalRService
{
    public class Startup
    {           
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.MapSignalR();
            // app.UseWelcomePage();
        }
    }
}
