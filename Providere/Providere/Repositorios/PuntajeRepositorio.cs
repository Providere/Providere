using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;
using System.Web.Security;
using Providere.Servicios;

namespace Providere.Repositorios
{
    public class PuntajeRepositorio
    {
        ProvidereEntities context = new ProvidereEntities();

        internal Puntaje traerDatosPorPublicacion(Publicacion publicacion)
        {
            var puntuacion = (from puntuacionSeleccionada in context.Puntaje
                              where puntuacionSeleccionada.IdPublicacion == publicacion.Id
                              select puntuacionSeleccionada).FirstOrDefault();

            if (puntuacion == null)
            {
                Puntaje puntajeArmado = new Puntaje();
                puntajeArmado.Total = 0;
                return puntajeArmado;
            }
            else
            {
                return puntuacion;
            }

        }

        public object TraerPuntaje(int p)
        {
            var puntaje = (from punt in context.Puntaje
                           where punt.IdPublicacion == p
                           select punt.Total).FirstOrDefault();
            return puntaje;
        }

        //Es para traer los puntajes de las publicaciones de un usuario, lo utilizo en Ver Perfil
        internal List<Puntaje> traerPuntajesDePublicacionesDeUnUsuario(List<Publicacion> publicaciones)
        { 
            List<int> listaPublicaciones = new List<int>();

            foreach (var item in publicaciones)
            {
                listaPublicaciones.Add(item.Id);
            }

            var resultado = from punt in context.Puntaje
                            where listaPublicaciones.Contains(punt.IdPublicacion)
                            select punt;

            return resultado.ToList();
        }

    }
}