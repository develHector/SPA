using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPAApplication.Models
{
    public class Artist
    {
        // Hay que definir una llave, que siempre será un entero con el nombre de la clase y terminación ID
        public int ArtistID { get; set; }

        public string Name { get; set; }
    }
}
