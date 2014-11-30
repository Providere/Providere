using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Providere.Models;
using System.Web.Security;
using Providere.Servicios;

namespace Providere.Repositorios
{
    class TipoCalificacionRepositorio
    {
        ProvidereEntities context = new ProvidereEntities();

        internal TipoCalificacion traerDatosPorId(int id)
        {
            var tipo = (from tipoCalificacion in context.TipoCalificacion
                        where tipoCalificacion.Id == id
                        select tipoCalificacion).First();
            return tipo;
        }
    }
}
