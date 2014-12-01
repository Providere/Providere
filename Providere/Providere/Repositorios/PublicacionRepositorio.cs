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
                                 orderby publicacion.FechaCreacion
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
            if (String.IsNullOrEmpty(Rubro.ToString())
                && String.IsNullOrEmpty(SubRubro.ToString())
                && String.IsNullOrEmpty(Ubicacion))
            {

                var publicaciones = (from publicacion in context.Publicacion
                                     join usuario in context.Usuario on publicacion.IdUsuario equals usuario.Id
                                     where (publicacion.Rubro == Rubro) || (publicacion.SubRubro == SubRubro)
                                     || (usuario.Ubicacion == Ubicacion)
                                     select publicacion).ToList();

                return publicaciones;
            }
            if (String.IsNullOrEmpty(Rubro.ToString())
                && String.IsNullOrEmpty(SubRubro.ToString())
                && String.IsNullOrEmpty(Ubicacion))
            {

                var publicaciones = (from publicacion in context.Publicacion
                                     join usuario in context.Usuario on publicacion.IdUsuario equals usuario.Id
                                     where (publicacion.Rubro == Rubro) || (publicacion.SubRubro == SubRubro)
                                     || (usuario.Ubicacion == Ubicacion)
                                     select publicacion).ToList();

                return publicaciones;
            }

            if (String.IsNullOrEmpty(Rubro.ToString())
                && String.IsNullOrEmpty(SubRubro.ToString())
                && String.IsNullOrEmpty(Ubicacion))
            {

                var publicaciones = (from publicacion in context.Publicacion
                                     join usuario in context.Usuario on publicacion.IdUsuario equals usuario.Id
                                     where (publicacion.Rubro == Rubro) || (publicacion.SubRubro == SubRubro)
                                     || (usuario.Ubicacion == Ubicacion)
                                     select publicacion).ToList();

                return publicaciones;
            }
            return null;
        }

        internal void CrearNuevaPublicacion(int idUsuario, int idRubro, int idSubRubro, string titulo, string descripcion, int precioOpcion, string precio, IEnumerable<HttpPostedFileBase> files)
        {
            Publicacion mipublicacion = new Publicacion();
            mipublicacion.IdUsuario = Convert.ToInt16(idUsuario);
            mipublicacion.Titulo = titulo;
            mipublicacion.Descripcion = descripcion;
            mipublicacion.PrecioOpcion = precioOpcion;
            mipublicacion.Precio = Convert.ToDecimal(precio);
            mipublicacion.IdRubro = Convert.ToInt16(idRubro);
            mipublicacion.IdSubRubro = Convert.ToInt16(idSubRubro);
            mipublicacion.FechaCreacion = DateTime.Now;
            mipublicacion.FechaEdicion = DateTime.Now;
            mipublicacion.Estado = 1; // Habilitada
            context.Publicacion.AddObject(mipublicacion);

            foreach (var file in files)
            {
                string extension = Path.GetExtension(file.FileName);
                string uniqueFileName = Path.ChangeExtension(file.FileName, Convert.ToString(idUsuario));
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Imagenes/Publicacion"),
                              Path.GetFileName(uniqueFileName + extension));
                file.SaveAs(path);
                string pathImagen = uniqueFileName + extension;
                Imagen misImagenes = new Imagen();
                misImagenes.Nombre = pathImagen;
                misImagenes.IdPublicacion = mipublicacion.Id;
                context.Imagen.AddObject(misImagenes);
            }

            context.SaveChanges();
        }

    }
}