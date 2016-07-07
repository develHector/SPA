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

        public TimerMessage(DateTime now)
        {
            this.texto = DateTime.Now.ToString("T");
            this.fecha = DateTime.Now;
            this.hora = DateTime.Now.TimeOfDay;
            this.id = DateTime.Now.Ticks;
            this.ok = true;
        }

        public bool Equals(TimerMessage other)
        {
            return other != null &&
                this.texto.Equals(other.texto) &&
                this.fecha.Equals(other.fecha) &&
                this.hora.Equals(other.hora) &&
                this.id.Equals(other.id) &&
                this.ok.Equals(other.ok);
        }
    }
}
