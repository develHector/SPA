using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRService
{
    [HubName("myHub")]
    public class MyHub : Hub
    {
        private static int hitCount = 0;

        private static System.Threading.Timer mytimer;

        public MyHub()
        {
            MyHub.mytimer = MyHub.mytimer ?? new System.Threading.Timer(this.onTimer, null, 1000, 1000);
        }
         
        private void onTimer(object state)
        {
            Clients.All.onTimer(DateTime.Now.ToString("T"));
        }

        public void RecordHit()
        {
            // Clients.All.hello(); // Este lo pone por default, pero no le hagamos caso
            // Notar que el All es un objeto dinámico, cosa que hasta SignalR hace sentido usar
            MyHub.hitCount++;
            Clients.All.onRecordHit(MyHub.hitCount);
        }

        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            MyHub.hitCount--;
            Clients.All.onRecordHit(MyHub.hitCount);

            return base.OnDisconnected(stopCalled);

        }

    }
}
