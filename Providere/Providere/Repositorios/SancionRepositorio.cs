using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;


namespace Providere.Repositorios
{
    public class SancionRepositorio
    {
        ProvidereEntities context = new ProvidereEntities();

        internal Sancion ObtenerSancionDeUsuario(Usuario usuario)
        {
            var sancion = context.Sancion.Where(s => s.Usuario.Id == usuario.Id).Where(s => s.FechaFin > DateTime.Now ).FirstOrDefault();
            return sancion;
        }

    }
}