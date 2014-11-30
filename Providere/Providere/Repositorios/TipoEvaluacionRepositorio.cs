using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Providere.Models;
using System.Web.Security;
using Providere.Servicios;


namespace Providere.Repositorios
{
    class TipoEvaluacionRepositorio
    {
        ProvidereEntities context = new ProvidereEntities();

        internal TipoEvaluacion traerDatosPorId(int id)
        {
            var tipo = (from tipoEvaluacion in context.TipoEvaluacion
                        where tipoEvaluacion.Id == id
                        select tipoEvaluacion).First();
            return tipo;
        }

    }
}
