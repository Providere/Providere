using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;
using Providere.Repositorios;

namespace Providere.Servicios
{
    public class SancionServicios
    {
        SancionRepositorio sr = new SancionRepositorio();
        internal Sancion ObtenerSancionDeUsuario(Usuario usuario)
        {
            return sr.ObtenerSancionDeUsuario(usuario);
        }
    }
}