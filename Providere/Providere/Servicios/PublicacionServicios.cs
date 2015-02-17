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
        PuntajeRepositorio pur = new PuntajeRepositorio();
        ImagenRepositorio ir = new ImagenRepositorio();

        public void CrearNuevaPublicacion(int idUsuario, int idRubro, int? idSubRubro, string titulo, string descripcion, int precioOpcion, decimal? precio)
        {
            pr.CrearNuevaPublicacion(idUsuario, idRubro, idSubRubro, titulo, descripcion, precioOpcion, precio);
        }


        public ListaPublicacionesModel traerPublicacionesMejorCalificadas(int limite)
        {
            return obtenerPuntajeImagen(pr.traerPublicacionesMejorCalificadas(limite));
        }

        public ListaPublicacionesModel traerPublicacionesMasRecientes(int limite)
        {
            return obtenerPuntajeImagen(pr.traerPublicacionesMasRecientes(limite));
        }

        public ListaPublicacionesModel traerPublicacionesMasCercanas(String zona, int limite)
        {
            return obtenerPuntajeImagen(pr.traerPublicacionesPorZona(zona, limite));
        }

        // Obtiene el puntaje y la imagen a mostrar de un listado de publicaciones
        // Probablemente no es necesario
        public ListaPublicacionesModel obtenerPuntajeImagen(List<Publicacion> resPublicaciones)
        {
            ListaPublicacionesModel publicaciones = new ListaPublicacionesModel();
            List<PublicacionPuntaje> publiList = new List<PublicacionPuntaje>();

            foreach (var publiMasPopular in resPublicaciones)
            {
                PublicacionPuntaje pj = new PublicacionPuntaje();
                pj.publicacion = publiMasPopular;
                pj.puntaje = pur.traerDatosPorPublicacion(publiMasPopular);
                pj.imagen = ir.traerImagenPrincipal(publiMasPopular);
                if (pj.imagen == null)
                {
                    Imagen im = new Imagen();
                    im.Nombre = "thumbnail-md.png";
                    pj.imagen = im;

                }
                publiList.Add(pj);
            }

            publicaciones.listadoDePublicaciones = publiList;
            return publicaciones;
        }


        public void CargarImagenes(string pathImagen, int idUsuario)
        {
            pr.CargarImagenes(pathImagen, idUsuario);
        }

        public object ListarMisPublicaciones(int idUsuario)
        {
            var misPublicaciones = pr.ListarMisPublicaciones(idUsuario);
            return misPublicaciones;
        }

        internal Publicacion TraerPublicacionPorId(int idPublicacion)
        {
            return pr.TraerPublicacionPorId(idPublicacion);
        }

        public void ModificarPublicacion(int id, int idRubro, int? idSubRubro, string titulo, string descripcion, int precioOpcion, decimal? precio, decimal? oculto)
        {
            pr.ModificarPublicacion(id, idRubro, idSubRubro, titulo, descripcion, precioOpcion, precio, oculto);
        }

        public void EliminarImagen(int id)
        {
            pr.EliminarImagen(id);
        }

        public int VerificarCantidadImagenes(int id)
        {
            return pr.VerificarCantidadImagenes(id);
        }

        public void CargarImagenesEdicion(string pathImagen, int idUsuario, int id)
        {
            pr.CargarImagenesEdicion(pathImagen, idUsuario, id);
        }

        public bool NoExistenImagenes(int id)
        {
            try
            {
                Imagen imagenes = pr.NoExistenImagenes(id);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void CambioEstadoPublicacion(int id)
        {
            pr.CambiarEstadoPublicacion(id);
        }

        public bool VerificarRubro(int idUsuario, int idRubro)
        {
            try
            {
                Publicacion publicacion = pr.VerificarRubro(idUsuario, idRubro);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public bool VerificarSubrubro(int idUsuario, int? idSubRubro)
        {
            try
            {
                Publicacion publicacion = pr.VerificarSubrubro(idUsuario, idSubRubro);
            }
            catch
            {
                return false;
            }
            return true;
        }       
    }
}