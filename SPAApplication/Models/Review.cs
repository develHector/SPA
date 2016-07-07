using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPAApplication.Models
{
    public class Review
    {

        public int ReviewID { get; set; }

        public int AlbumID { get; set; }

        // Las relaciones tienen que ser virtuales, un album puede tener reviews multiples, esto apunta hacia atrás
        public virtual Album Album { get; set; }

        public string Contents { get; set; }

        [Required()]
        [Display(Name ="Email Address")]
        [DataType(DataType.EmailAddress)]
        public string ReviewerEmail { get; set; }
    }
}

// Code-first EF - automatically figures things out
// EF - one to one relationship requiere lo de virtuales y listas