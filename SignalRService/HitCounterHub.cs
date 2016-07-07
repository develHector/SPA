using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;

namespace SignalRService
{
    [HubName("hitCounter")]
    // [Authorize] // Lamentablemente en modelo self-hosted no podemos pedir que sea autorizado, habrá que resolver nosotros la validación con token por llamada
    public class HitCounterHub : SignalRLibrary.HitCounterLibHub
    { }
}