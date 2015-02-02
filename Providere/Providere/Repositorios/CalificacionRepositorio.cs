using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;
using Providere.Servicios;

namespace Providere.Repositorios
{
    public class CalificacionRepositorio
    {
        ProvidereEntities context = new ProvidereEntities();


        internal void calificarUsuario(Calificacion model)
        {
            context.Calificacion.AddObject(model);
            context.SaveChanges();
        }


        internal List<Calificacion> obtenerNegativasDeUsuario(Usuario usuario)
        {
            return context.Calificacion.Where(x => x.Usuario.Id == usuario.Id).Where(x => x.Denuncia.Count > 1).ToList();
        }

    
    }
}