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
            var publicaciones = (from publicacion in context.Publicacion where publicacion.Estado == 1  select publicacion).Take(limite).ToList();
            return publicaciones;
        }

        internal List<Publicacion> traerPublicacionesMasRecientes(int limite)
        {
            var publicaciones = (from publicacion in context.Publicacion
                                 where publicacion.Estado == 1 //Habilitada
                                 orderby publicacion.FechaCreacion descending
                                 select publicacion).Take(limite).ToList();
            return publicaciones;
        }

        internal List<Publicacion> traerPublicacionesMejorCalificadas(int limite)
        {

            var publicacionesMasPopulares = (from publicacion in context.Publicacion
                                             join puntaje in context.Puntaje on publicacion.Id equals puntaje.IdPublicacion
                                             where publicacion.Estado == 1 //Habilitada
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

        public void CrearNuevaPublicacion(int idUsuario, int idRubro, int? idSubRubro, string titulo, string descripcion, int precioOpcion, decimal? precio)
        {
            Publicacion mipublicacion = new Publicacion();
            mipublicacion.IdUsuario = Convert.ToInt16(idUsuario);
            mipublicacion.Titulo = titulo;
            mipublicacion.Descripcion = descripcion;

            if (precioOpcion == 1)
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



        public void CargarImagenes(string pathImagen, int idUsuario)
        {
            var IdPublicacion = (from publicacion in context.Publicacion
                                 where (publicacion.IdUsuario == idUsuario)
                                 select publicacion).Max(p => p.Id); //La publicacion que se esta creando es la ultima,se guarda con Id autoincremental 
            Imagen misImagenes = new Imagen();
            misImagenes.Nombre = pathImagen;
            misImagenes.IdPublicacion = IdPublicacion;
            context.Imagen.AddObject(misImagenes);
            context.SaveChanges();
        }

        public object ListarMisPublicaciones(int idUsuario)
        {
            var resultado = (from publicaciones in context.Publicacion
                             where (publicaciones.IdUsuario == idUsuario)  //Listar todas mis publicaciones
                             select publicaciones).ToList();
            return resultado;
        }

        public Publicacion TraerPublicacion(int Id, int idUsuario)
        {
            var publicacion = (from publicaciones in context.Publicacion
                               where (publicaciones.IdUsuario == idUsuario) && (publicaciones.Id == Id) 
                               select publicaciones).FirstOrDefault(); 
            return publicacion;
        }


        internal Publicacion TraerPublicacionPorId(int idPublicacion)
        {
            var publicacion = (from publicaciones in context.Publicacion
                               where (publicaciones.Id == idPublicacion) 
                               select publicaciones).FirstOrDefault();
            return publicacion;
        }

        public void ModificarPublicacion(int id, int idRubro, int? idSubRubro, string titulo, string descripcion, int precioOpcion, decimal? precio, decimal? oculto)
        {
            Publicacion publicacion = context.Publicacion.Where(e => e.Id == id).FirstOrDefault();
            publicacion.Titulo = titulo;
            publicacion.Descripcion = descripcion;

            if (precioOpcion == 1)
            {
                publicacion.PrecioOpcion = precioOpcion;
                publicacion.Precio = null;
            }
            else
            {
                if (precioOpcion == 2 && precio == null)
                {
                    publicacion.PrecioOpcion = precioOpcion;
                    publicacion.Precio = oculto;
                }
                else
                {
                    publicacion.PrecioOpcion = precioOpcion;
                    publicacion.Precio = Convert.ToDecimal(precio);
                }

            }
            publicacion.IdRubro = Convert.ToInt16(idRubro);
            if (idSubRubro != null)
            {
                publicacion.IdSubRubro = Convert.ToInt16(idSubRubro);
            }
            else
            {
                publicacion.IdSubRubro = null;
            }

            publicacion.FechaEdicion = DateTime.Now;
            context.SaveChanges();
        }

        public void EliminarImagen(int id)
        {
            var baja = (from e in context.Imagen
                        where e.Id == id
                        select e).Single();

            context.Imagen.DeleteObject(baja);
            context.SaveChanges();
        }

        public int VerificarCantidadImagenes(int id)
        {
            var cant = (from e in context.Imagen
                        where e.IdPublicacion == id
                        select e).Count();
            return cant;
        }

        public void CargarImagenesEdicion(string pathImagen, int idUsuario, int id)
        {
            Imagen misImagenes = new Imagen();
            misImagenes.Nombre = pathImagen;
            misImagenes.IdPublicacion = Convert.ToInt16(id);
            context.Imagen.AddObject(misImagenes);
            context.SaveChanges();
        }

        public Imagen NoExistenImagenes(int id)
        {
            var imagen = (from img in context.Imagen
                          where (img.IdPublicacion == id)
                          select img).First();
            return imagen;
        }

        public void CambiarEstadoPublicacion(int id)
        {
            Publicacion publicacion = context.Publicacion.Where(e => e.Id == id).FirstOrDefault();
            if (publicacion.Estado == 1)
            {
                publicacion.Estado = 0; //Deshabilita
            }
            else
            {
                publicacion.Estado = 1; //Habilita nuevamente
            }

            context.SaveChanges();
        }

        public Publicacion VerificarRubro(int idUsuario, int idRubro)
        {
            var publicacion = (from publicaciones in context.Publicacion
                               
                               where (publicaciones.IdUsuario == idUsuario && publicaciones.IdRubro == idRubro
                                &&  publicaciones.IdRubro<20)
                               select publicaciones).First();
            return publicacion;
        }

        public Publicacion VerificarSubrubro(int idUsuario, int? idSubRubro)
        {
            var publicacion = (from publicaciones in context.Publicacion

                               where (publicaciones.IdUsuario == idUsuario && publicaciones.IdSubRubro == idSubRubro)
                               select publicaciones).First();
            return publicacion;
        }
    }
}
