using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Providere.Models
{
    public class PublicacionPuntaje
    {

        public Publicacion publicacion { get; set; }

        public Puntaje puntaje { get; set; }

        public Imagen imagen { get; set; }
    }
}