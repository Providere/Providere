using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;
using Providere.Servicios;
using System.IO;

namespace Providere.Repositorios
{
    public class PublicacionRepositorio
    {

        ProvidereEntities context = new ProvidereEntities();

        internal List<Publicacion> traerPublicacionesPorZona(String zona, int limite)
        {
            var publicaciones = (from publicacion in context.Publicacion select publicacion).Take(limite).ToList();
            return publicaciones;
        }

        internal List<Publicacion> traerPublicacionesMasRecientes(int limite)
        {
            var publicaciones = (from publicacion in context.Publicacion
                                 orderby publicacion.FechaCreacion descending 
                                 select publicacion).Take(limite).ToList();
            return publicaciones;
        }

        internal List<Publicacion> traerPublicacionesMejorCalificadas(int limite)
        {

            var publicacionesMasPopulares = (from publicacion in context.Publicacion
                                             join puntaje in context.Puntaje on publicacion.Id equals puntaje.IdPublicacion
                                             orderby puntaje.Total descending
                                             select publicacion).Take(limite).ToList();

            return publicacionesMasPopulares;
        }

        internal List<Publicacion> buscarPorRubroSubRubroUbicacion(Rubro Rubro, SubRubro SubRubro, string Ubicacion)
        {
            var publicaciones = (from publicacion in context.Publicacion select publicacion);
            if (Rubro != null)
            {
                publicaciones = publicaciones.Where(b => Rubro.Id.Equals(21));
            }
            if (SubRubro != null)
            {
                publicaciones = publicaciones.Where(b => SubRubro.Id.Equals(b.Rubro.Id));
            }

            if (Ubicacion != null)
            {
                publicaciones = publicaciones.Where(b => b.Usuario.Ubicacion.Equals(Ubicacion));
            }
            return publicaciones.ToList();

        }

        internal void CrearNuevaPublicacion(int idUsuario, int idRubro, int? idSubRubro, string titulo, string descripcion, int precioOpcion, decimal? precio)
        {
            Publicacion mipublicacion = new Publicacion();
            mipublicacion.IdUsuario = Convert.ToInt16(idUsuario);
            mipublicacion.Titulo = titulo;
            mipublicacion.Descripcion = descripcion;

            if (precioOpcion == '1')
            {
                mipublicacion.PrecioOpcion = precioOpcion;
                mipublicacion.Precio = null;
            }
            else
            {
                mipublicacion.PrecioOpcion = precioOpcion;
                mipublicacion.Precio = Convert.ToDecimal(precio);
            }
            mipublicacion.IdRubro = Convert.ToInt16(idRubro);
            if (idSubRubro != null)
            {
                mipublicacion.IdSubRubro = Convert.ToInt16(idSubRubro);
            }
            else
            {
                mipublicacion.IdSubRubro = null;
            }
            mipublicacion.FechaCreacion = DateTime.Now;
            mipublicacion.FechaEdicion = DateTime.Now;
            mipublicacion.Estado = 1; // Habilitada
            context.Publicacion.AddObject(mipublicacion);
            context.SaveChanges();
        }



        internal void CargarImagenes(string pathImagen, int idUsuario)
        {
            var IdPublicacion = (from publicacion in context.Publicacion
                                 where (publicacion.IdUsuario == idUsuario)
                                 select publicacion).Max(p => p.Id);
            Imagen misImagenes = new Imagen();
            misImagenes.Nombre = pathImagen;
            misImagenes.IdPublicacion = IdPublicacion;
            context.Imagen.AddObject(misImagenes);
            context.SaveChanges();
        }

        internal object ListarMisPublicaciones(int idUsuario)
        {
            var resultado = (from publicaciones in context.Publicacion
                             where publicaciones.IdUsuario == idUsuario
                             select publicaciones);
            return resultado;
        }

        internal Publicacion TraerPublicacion(int Id, int idUsuario)
        {
            var publicacion = (from publicaciones in context.Publicacion
                               where publicaciones.IdUsuario == idUsuario && publicaciones.Id == Id
                               select publicaciones).FirstOrDefault();
            return publicacion;
        }


        internal Publicacion TraerPublicacionPorId(int idPublicacion)
        {
            var publicacion = (from publicaciones in context.Publicacion
                               where publicaciones.Id == idPublicacion
                               select publicaciones).FirstOrDefault();
            return publicacion;
        }
    }
}
