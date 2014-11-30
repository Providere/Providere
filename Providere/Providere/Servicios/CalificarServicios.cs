using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Repositorios;
using Providere.Models;

namespace Providere.Servicios
{
    public class CalificarServicios
    {
        CalificacionRepositorio cr = new CalificacionRepositorio();
        ContratacionRepositorio cor = new ContratacionRepositorio();
        TipoEvaluacionRepositorio tir = new TipoEvaluacionRepositorio();
        TipoCalificacionRepositorio tcr = new TipoCalificacionRepositorio();

        internal void calificarUsuario(short idCalificador, short idCalificado, short contratacionId, short tipoEvaluacionId, short tipoCalificacionId)
        {
            Contratacion contratacion = cor.traerDatosPorId(contratacionId);
            TipoEvaluacion tipoEvaluacion = tir.traerDatosPorId(tipoEvaluacionId);
            TipoCalificacion tipoCalificacion = tcr.traerDatosPorId(tipoCalificacionId);

            Calificacion calificacion  = new Calificacion();
            calificacion.IdCalificador = idCalificador;
            calificacion.IdCalificado = idCalificado;
            calificacion.Contratacion = contratacion;
            calificacion.TipoEvaluacion = tipoEvaluacion;
            calificacion.TipoCalificacion = tipoCalificacion;

            cr.calificarUsuario(calificacion);
        }
    }
}