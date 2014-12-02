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
    }
}