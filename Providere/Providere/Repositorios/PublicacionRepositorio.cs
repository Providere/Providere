using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;
using Providere.Servicios;

namespace Providere.Repositorios
{
    public class PublicacionRepositorio
    {

        ProvidereEntities context = new ProvidereEntities();

        internal List<Publicacion> traerPublicacionesMasPopulares()
        {
            var publicaciones = (from publicacion in context.Publicacion select publicacion).ToList();
            return publicaciones;
        }

        internal List<Publicacion> buscarPorRubroSubRubroUbicacion(short Rubro, short SubRubro, string Ubicacion)
        {
            throw new NotImplementedException();
        }
    }
}