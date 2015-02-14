using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Repositorios;
using Providere.Models;

namespace Providere.Servicios
{
    public class CalificacionServicios
    {
        CalificacionRepositorio cr = new CalificacionRepositorio();
        ContratacionRepositorio cor = new ContratacionRepositorio();
        TipoEvaluacionRepositorio tir = new TipoEvaluacionRepositorio();
        TipoCalificacionRepositorio tcr = new TipoCalificacionRepositorio();

        internal void calificarUsuario(int idContratacion, int idTipoEvaluacion, int idTipoCalificacion, string comentario)
        {
            Contratacion contratacion = cor.traerDatosPorId(idContratacion);
            TipoEvaluacion tipoEvaluacion = tir.traerDatosPorId(idTipoEvaluacion);
            TipoCalificacion tipoCalificacion = tcr.traerDatosPorId(idTipoCalificacion);

            cr.calificarUsuario(comentario, contratacion, tipoEvaluacion, tipoCalificacion, idTipoCalificacion);

        }

        public List<Calificacion> tieneCalificacionContrataciones(List<Contratacion> listaContratacion)
        {
            var tieneCalificacion = cr.tieneCalificacionContrataciones(listaContratacion);

            return tieneCalificacion;
        }

        internal List<Calificacion> obtenerNegativasDeUsuario(Usuario usuario)
        {
            return cr.obtenerNegativasDeUsuario(usuario);
        }

        public object TraerCalificaciones(int idPublicacion)
        {
            return cr.TraerCalificaciones(idPublicacion);
        }

        public object TraerPrimerasCalificaciones(int limite, int idPublicacion)
        {
            return cr.TraerPrimerasCalificaciones(limite, idPublicacion);
        }

        public object TraerCalificacionesObtenidas(int idUsuario,int limite)
        {
            return cr.TraerCalificacionesObtenidas(idUsuario, limite);
        }

        public object TraerCalificacionesOtorgadas(int idUsuario, int limite)
        {
            return cr.TraerCalificacionesOtorgadas(idUsuario, limite);
        }

        public object TraerCalificacionesObtenidasTodas(int idUsuario)
        {
            return cr.TraerCalificacionObtenidasTodas(idUsuario);
        }

        public object TraerCalificacionesOtorgadasTodas(int idUsuario)
        {
            return cr.TraerCalificacionesOtorgadasTodas(idUsuario);
        }
    }
}