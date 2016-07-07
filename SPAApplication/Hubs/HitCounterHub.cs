using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SPAApplication
{
    [HubName("hitCounter")]
    [Authorize] // Aquí sí se puede (y se debe) manejar que sólo con Authorize se hagan las llamadas de SignalR
    public class HitCounterHub : SignalRLibrary.HitCounterLibHub
    { }
}