using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Repositorios;
using Providere.Models;

namespace Providere.Servicios
{
    public class ContratacionServicios
    {
        ContratacionRepositorio cor = new ContratacionRepositorio();
        public List<Contratacion> traerContratacionesRealizadas(int idUsuario)
        {
            return cor.traerContratacionesRealizadas(idUsuario);
        }
    }
}