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

        internal List<Calificacion> obtenerNegativasDeUsuario(Usuario usuario)
        {
            return cr.obtenerNegativasDeUsuario(usuario);
        }

        public List<Calificacion> TraerCalificaciones(int idPublicacion)
        {
            return cr.TraerCalificaciones(idPublicacion);
        }

        public List<Calificacion> TraerPrimerasCalificaciones(int limite, int idPublicacion)
        {
            return cr.TraerPrimerasCalificaciones(limite, idPublicacion);
        }

        public List<Calificacion> TraerCalificacionesObtenidas(int idUsuario,int limite)
        {
            return cr.TraerCalificacionesObtenidas(idUsuario, limite);
        }

        public List<Calificacion> TraerCalificacionesOtorgadas(int idUsuario, int limite)
        {
            return cr.TraerCalificacionesOtorgadas(idUsuario, limite);
        }

        public List<Calificacion> TraerCalificacionesObtenidasTodas(int idUsuario)
        {
            return cr.TraerCalificacionObtenidasTodas(idUsuario);
        }

        public List<Calificacion> TraerCalificacionesOtorgadasTodas(int idUsuario)
        {
            return cr.TraerCalificacionesOtorgadasTodas(idUsuario);
        }

        public List<Calificacion> TraerCalificacionesPositivas(int idPublicacion)
        {
            return cr.TraerCalificacionesPositivas(idPublicacion);
        }

        public List<Calificacion> TraerCalificacionesNeutras(int idPublicacion)
        {
            return cr.TraerCalificacionesNeutras(idPublicacion);
        }

        public List<Calificacion> TraerCalificacionesNegativas(int idPublicacion)
        {
            return cr.TraerCalificacionesNegativas(idPublicacion);
        }

        public Calificacion TraerCalificacionReplicar(int id)
        {
            return cr.TraerCalificacionReplicar(id);
        }
    }
}