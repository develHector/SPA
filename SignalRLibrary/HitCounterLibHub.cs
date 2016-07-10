using System;

namespace SignalRLibrary
{
    using MyBag = System.Collections.Generic.List<TimerMessage>;

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

#if DEBUG
            // Prueba y comparación de la serialización JSON texto y binaria
            // Desconozco si comprimida una será mejor que la otra, 
            // Desconozco si la serlalización binaria nativa de .Net es mejor, más rápida o de menor tamaño
            // NOTA: No pude de-serializar la lista, solo el objeto binario
            var t0 = new TimerMessage(DateTime.Now);
            var bag = new MyBag() { t0, new TimerMessage(DateTime.MinValue), new TimerMessage(DateTime.MaxValue) };

            var tobj = JsonSerializationTest(bag);
            var tbag = JsonDeserializationTest<MyBag>(tobj);
            System.Diagnostics.Debug.WriteLine(tbag.ToString());

            var bobj0 = BinaryJsonSerializationTest(t0);
            var bt0 = BinaryJsonDeserializationTest<TimerMessage>(bobj0);
            System.Diagnostics.Debug.WriteLine(bt0.ToString());

            var tobj1 = JsonSerializationTest(t0);
            var bt1 = JsonDeserializationTest<MyBag>(tobj);
            System.Diagnostics.Debug.WriteLine(tbag.ToString());
#endif    
        }


        private void onTimer(object state)
        {
            // Automáticamente SignalR serializa a JSON
            var p1 = new TimerMessage(DateTime.Now);

            // Pero para fines didácticos, mandamos una lista serializada de cosas básicas
            var bag = new MyBag() { p1 };
            var SerializedBag = Newtonsoft.Json.JsonConvert.SerializeObject(bag);

            // Mandamos a toda la banda
            Clients.All.onTimer(SerializedBag);
        }

        private string JsonSerializationTest<T>(T bag)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(bag); ;
        }

        private T JsonDeserializationTest<T>(string buffer)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(buffer); ;
        }

        private byte[] BinaryJsonSerializationTest<T>(T bag)
        {
            var jsonSerializer = new Newtonsoft.Json.JsonSerializer();

            using (var ms = new System.IO.MemoryStream())
            using (var bson = new Newtonsoft.Json.Bson.BsonWriter(ms))
            {
                jsonSerializer.Serialize(bson, bag, typeof(T));
                return ms.ToArray();
            }

        }

        private T BinaryJsonDeserializationTest<T>(byte[] bytes)
        {
            var jsonSerializer = new Newtonsoft.Json.JsonSerializer();

            // Deserialization
            using (var ms = new System.IO.MemoryStream(bytes))
            {
                using (var bson = new Newtonsoft.Json.Bson.BsonReader(ms))
                {
                    // Exception here
                    return jsonSerializer.Deserialize<T>(bson);
                }
            }
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