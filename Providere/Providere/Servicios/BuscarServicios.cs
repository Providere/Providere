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

        internal List<Publicacion> buscar(string Rubro, string SubRubro, string Ubicacion)
        {


            Rubro rubro = new Rubro();
            if (Rubro != null)
            {
                rubro = rs.traerDatosPorId(Int32.Parse(Rubro));
            }
            else
            {
                rubro = null;
            }
            SubRubro subrubro = new SubRubro();
            if (SubRubro != null)
            {
                subrubro = ss.traerDatosPorId(Int32.Parse(SubRubro));
            }
            else
            {
                subrubro = null;
            }
            return pr.buscarPorRubroSubRubroUbicacion(rubro, subrubro, Ubicacion);

        }
    }
}