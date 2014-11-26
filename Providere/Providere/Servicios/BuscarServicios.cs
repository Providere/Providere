using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Repositorios;
using Providere.Models;

namespace Providere.Servicios
{
    public class BuscarServicios
    {
        PublicacionRepositorio pr = new PublicacionRepositorio();

        internal List<Publicacion> buscar(short Rubro, short SubRubro, string Ubicacion)
        {
            return pr.buscarPorRubroSubRubroUbicacion(Rubro,SubRubro,Ubicacion);
            throw new NotImplementedException();
        }
    }
}