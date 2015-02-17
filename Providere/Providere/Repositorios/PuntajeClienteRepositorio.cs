using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;
using System.Web.Security;
using Providere.Servicios;

namespace Providere.Repositorios
{
    public class PuntajeClienteRepositorio
    {
        ProvidereEntities context = new ProvidereEntities();

        public object TraerPuntajeCliente(int p)
        {
            var puntaje = (from punt in context.PuntajeCliente
                           where punt.IdUsuario == p
                           select punt).FirstOrDefault();

            if (puntaje == null)
            {
                Puntaje puntajeArmado = new Puntaje();
                puntajeArmado.Total = 0;
                return puntajeArmado;
            
            }
            else
            {
                return puntaje;
            }
            
        }

    }
}