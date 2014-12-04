using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;
using Providere.Servicios;

namespace Providere.Repositorios
{
    public class RubroRepositorio
    {
        ProvidereEntities context = new ProvidereEntities();

        internal Rubro traerDatosPorId(int id)
        {
            var rubro = (from rr in context.Rubro
                                where rr.Id == id
                                select rr).FirstOrDefault();
            return rubro;
        }
    }
}