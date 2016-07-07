using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPAApplication.Models
{
    public class Album
    {

        public int AlbumID { get; set; }

        public string Title { get; set; }

        public Artist Artist { get; set; }

        // Hicimos esto una lista para que la pata de gallo esté para acá, y cuando creemos el Controller de forma automática, no nos truene
        // un album puede tener reviews multiples
        public virtual List<Review> Review { get; set; }

    }
}
