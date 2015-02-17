using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;
using Providere.Repositorios;


namespace Providere.Servicios
{
    public class PuntajeClienteServicio
    {

        PuntajeClienteRepositorio pcr = new PuntajeClienteRepositorio();

        public object TraerPuntajeCliente(int id)
        {
            return pcr.TraerPuntajeCliente(id);
        }



    }
}