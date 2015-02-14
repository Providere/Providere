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

        public List<Contratacion> traerQuienesMeContrataron(int idUsuario)
        {
            return cor.traerQuienesMeContrataron(idUsuario);
        }

        internal Contratacion nuevaContratacion(Publicacion publicacion, Usuario usuario)
        {
            return cor.nuevaContratacion(publicacion, usuario);
        }

        internal object traerPorId(int idContratacion)
        {
            return cor.traerDatosPorId(idContratacion);
        }

        public object TraerContratada(int idPublicacion, int idUsuario)
        {
            return cor.TraerContratada(idPublicacion, idUsuario);
        }
    }
}