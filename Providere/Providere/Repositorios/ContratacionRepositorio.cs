using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;
using Providere.Servicios;


namespace Providere.Repositorios
{
    public class ContratacionRepositorio
    {
        ProvidereEntities context = new ProvidereEntities();

        internal Contratacion traerDatosPorId(int id)
        {
            var contratacion = (from contrata in context.Contratacion
                                where contrata.Id == id
                                select contrata).FirstOrDefault();
            return contratacion;
        }

    }
}