using System;

namespace SignalRLibrary
{
    // La hacemos abstracta para que deba ser herdada
    public abstract class HitCounterLibHub : Microsoft.AspNet.SignalR.Hub
    {
        // Do not use static variables on web code, but on this case is didactic
        static int hitCount = 0;
        public static System.Threading.Timer mytimer;

        public HitCounterLibHub(bool CreateTimer = true)
        {
            if (CreateTimer && HitCounterLibHub.mytimer == null)
            {
                HitCounterLibHub.mytimer = new System.Threading.Timer(this.onTimer, null, 1000, 1000);
            }            
        }
                        

        private void onTimer(object state)
        {
            // Automáticamente SignalR serializa a JSON
            var p1 = new TimerMessage(DateTime.Now);
            
            // Pero para fines didácticos, mandamos una lista serializada de cosas básicas
            var Bag = new System.Collections.Generic.List<TimerMessage>() { p1 };
            var SerializedBag = Newtonsoft.Json.JsonConvert.SerializeObject(Bag);

            // Mandamos a toda la banda
            Clients.All.onTimer(SerializedBag);
        }

        public void RecordHit()
        {
            // Clients.All.hello(); // Este lo pone por default, pero no le hagamos caso
            // Notar que el All es un objeto dinámico, cosa que hasta SignalR hace sentido usar
            hitCount++;
            Clients.All.onRecordHit(hitCount);
        }

        /// <summary>
        /// Qué? El server se da cuenta cuando cierro mi browser? SIIIIIIIIIIIIII!!!!!!!!!!!!!!!!!!!
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns> 
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
        }

        /// <summary>
        /// Al entrar, para guardar alguna evidencia en log o algo
        /// </summary>
        /// <returns></returns>
        public override System.Threading.Tasks.Task OnConnected()
        {
            return base.OnConnected();
        }

        /// <summary>
        /// Con esto el server se da cuenta cuando el cliente cierra el browser
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            hitCount--;
            Clients.All.onRecordHit(hitCount);

            return base.OnDisconnected(stopCalled);

        }

        /// <summary>
        /// Reconexión, quizá para retomar foto o algo
        /// </summary>
        /// <returns></returns>
        public override System.Threading.Tasks.Task OnReconnected()
        {
            hitCount++;
            Clients.All.onRecordHit(hitCount);

            return base.OnReconnected();
        }
    }
}