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

        internal void CrearNuevaPublicacion(int idUsuario, int idRubro, int idSubRubro, string titulo, string descripcion, int precioOpcion,string precio)
        {
            Publicacion mipublicacion = new Publicacion();
            mipublicacion.IdUsuario = Convert.ToInt16(idUsuario);
            mipublicacion.Titulo = titulo;
            mipublicacion.Descripcion = descripcion;
            mipublicacion.PrecioOpcion = precioOpcion;
            mipublicacion.Precio = Convert.ToDecimal(precio);
            mipublicacion.IdRubro = Convert.ToInt16 (idRubro);
            mipublicacion.IdSubRubro = Convert.ToInt16(idSubRubro);
            mipublicacion.FechaCreacion = DateTime.Now;
            mipublicacion.FechaEdicion = DateTime.Now;
            mipublicacion.Estado = 1;
            context.Publicacion.AddObject(mipublicacion);
            context.SaveChanges();

        }
    }
}