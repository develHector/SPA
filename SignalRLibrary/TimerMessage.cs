using System;

namespace SignalRLibrary
{
    public class TimerMessage : IEquatable<TimerMessage>
    {        
        public string texto { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan hora { get; set; }
        public long id { get; set; }
        public bool ok { get; set; }

        public TimerMessage(DateTime d)
        {
            this.texto = d.ToString("T");
            this.fecha = d;
            this.hora = d.TimeOfDay;
            this.id = d.Ticks;
            this.ok = true;
        }

        public bool Equals(TimerMessage o)
        {
            return o != null &&
                this.texto.Equals(o.texto) &&
                this.fecha.Equals(o.fecha) &&
                this.hora.Equals(o.hora) &&
                this.id.Equals(o.id) &&
                this.ok.Equals(o.ok);
        }
    }
}
