using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;
using Providere.Repositorios;

namespace Providere.Servicios
{
    public class PuntajeServicios
    {
        PuntajeRepositorio pur = new PuntajeRepositorio();

        public object TraerPuntaje(int id)
        {
            return pur.TraerPuntaje(id);
        }
    }
}