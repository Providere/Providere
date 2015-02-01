﻿using System;
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

            Calificacion calificacion = new Calificacion();

            if (idTipoCalificacion == 1)
            {
                calificacion.IdCalificador = contratacion.IdUsuario;
                calificacion.IdCalificado = contratacion.Publicacion.IdUsuario;
            }
            else
            {
                calificacion.IdCalificador = contratacion.Publicacion.IdUsuario;
                calificacion.IdCalificado = contratacion.IdUsuario;
            }

            calificacion.Descripcion = comentario;
            calificacion.Contratacion = contratacion;
            calificacion.TipoEvaluacion = tipoEvaluacion;
            calificacion.TipoCalificacion = tipoCalificacion;

            cr.calificarUsuario(calificacion);
        }

    }
}