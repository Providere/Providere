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


        public void CrearNuevaPublicacion(int idUsuario, int idRubro, int idSubRubro, string titulo, string descripcion, int precioOpcion, string precio)
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

        internal object ListarMisPublicaciones(int idUsuario)
        {
            var misPublicaciones = pr.ListarMisPublicaciones(idUsuario);
            return misPublicaciones;
        }
    }
}