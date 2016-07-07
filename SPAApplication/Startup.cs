using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SPAApplication.Startup))]

namespace SPAApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);

            // Con lo siguiente cargas los hubs, que son objetos de mayor nivel de abstracción que las conexiones
            app.MapSignalR();
        }
    }
}
