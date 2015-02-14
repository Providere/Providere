using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;
using Providere.Repositorios;

namespace Providere.Servicios
{
    public class SancionServicios
    {
        SancionRepositorio sr = new SancionRepositorio();
        UsuarioServicios us = new UsuarioServicios();
        CalificacionServicios cs = new CalificacionServicios();

        internal Sancion ObtenerSancionDeUsuario(Usuario usuario)
        {
            return sr.ObtenerSancionDeUsuario(usuario);
        }

        /*
         * Los parámetros a evaluar son
         * Cantidad de denuncias en relacion a Calificaciones Negativas
         * 
         * Condiciones de bloqueo:
         * + 90% de denuncias 
         * Al tener el listado su estado como sancionados se activara por 15 dias
         * */
        internal void DetectarSancionados()
        {

            foreach (Usuario usuario in us.traerTodosConDenuncias())
            {

                List<Calificacion> calificacionesNegativas = cs.obtenerNegativasDeUsuario(usuario);

                int porcentajeDeDenuncias = (calificacionesNegativas.Count * 100) / usuario.Calificacion.Count;

                if (porcentajeDeDenuncias > 90)
                {
                    establecerSancion(usuario);
                }

            }
        }

        private void establecerSancion(Usuario usuario)
        {
            sr.establecerSancion(usuario);
        }

    }
}