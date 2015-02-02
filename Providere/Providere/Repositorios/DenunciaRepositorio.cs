using System;
using System.Collections.Generic;
using System.Linq;
using Providere.Models;
using Providere.Servicios;
using System.Web;

namespace Providere.Repositorios
{
    public class DenunciaRepositorio
    {
        ProvidereEntities context = new ProvidereEntities();        

        /**
         * Trae las denuncias efectuadas entre el dia del analisis de denuncias
         * y 30 dias antes
         * */
        internal List<Denuncia> traerDenunciasDelMes()
        {
            return context.Denuncia.Where(x => x.Fecha < DateTime.Now)
                .Where(x => x.Fecha > (
                    DateTime.Now.AddDays(-DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)
                    )
                    )
                    ).ToList();
        }
    }
}