using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;
using Providere.Repositorios;

namespace Providere.Servicios
{
    public class PublicacionServicios
    {
        PublicacionRepositorio pr = new PublicacionRepositorio();

        public List<Publicacion> traerPublicacionesMasPopulares()
        {
           return pr.traerPublicacionesMasPopulares();
        }
    }
}