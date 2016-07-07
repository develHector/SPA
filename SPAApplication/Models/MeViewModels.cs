using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SPAApplication.Models
{
    // Models returned by MeController actions.
    public class GetViewModel
    {
        public string Hometown { get; set; }

        // Creo ya por cuarta o quinta ocasión tengo que meterle estos datos al modelo de usuario
        public string ColorFavorito { get; set; }
        public string PrimeraNovia { get; set; }
    }
}