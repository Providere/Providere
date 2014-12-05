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

        RubroServicios rs = new RubroServicios();
        SubRubroServicios ss = new SubRubroServicios();

        internal List<Publicacion> buscar(int? Rubro, int? SubRubro, string Ubicacion)
        {
            if (Rubro != null)
            {
                Rubro rubro = new Rubro();
                rubro = rs.traerDatosPorId(Rubro.GetValueOrDefault());
            }

            if (SubRubro != null)
            {
                SubRubro subrubro = new SubRubro();
                subrubro = ss.traerDatosPorId(SubRubro.GetValueOrDefault());
            }

           /* if (Rubro != null && SubRubro != null && Ubicacion != null)
            {
                return pr.buscarPorRubroSubRubroUbicacion(rubro, subrubro, Ubicacion);
            }
            if (Rubro == null && SubRubro != null && Ubicacion != null)
            {
                return pr.buscarPorRubroSubRubroUbicacion(rubro, subrubro, Ubicacion);
            }

            if (Rubro != null && SubRubro == null && Ubicacion != null)
            {
                return pr.buscarPorRubroSubRubroUbicacion(rubro, subrubro, Ubicacion);
            }
            if (Rubro != null && SubRubro != null && Ubicacion == null)
            {
                return pr.buscarPorRubroSubRubroUbicacion(rubro, subrubro, Ubicacion);
            } */
            return null;
        }
    }
}