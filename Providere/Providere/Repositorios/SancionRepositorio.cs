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
            Sancion sancion = new Sancion();
            try
            {
                sancion = context.Sancion.Where(s => s.Usuario.Id == usuario.Id).Where(s => s.FechaFin > DateTime.Now).FirstOrDefault();
            } catch(NullReferenceException nre){
                return null;
            }
                return sancion;
        }


        internal void establecerSancion(Usuario usuario)
        {
            Sancion sancion = new Sancion();
            sancion.Usuario = usuario;
            sancion.FechaInicio = DateTime.Now;
            sancion.FechaFin = DateTime.Now.AddDays(15);

            Estado estado = new Estado();
            estado.Id = 2;
            usuario.Estado = estado;

            context.SaveChanges();
        }

        internal void retirarSancion(Usuario usuario)
        {
            Estado estado = new Estado();
            estado.Id = 1;
            usuario.Estado = estado;

            context.SaveChanges();
        }
    }
}