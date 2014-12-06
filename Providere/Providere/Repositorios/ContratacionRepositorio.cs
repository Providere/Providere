using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;
using Providere.Servicios;


namespace Providere.Repositorios
{
    public class ContratacionRepositorio
    {
        ProvidereEntities context = new ProvidereEntities();

        internal Contratacion traerDatosPorId(int id)
        {
            var contratacion = (from contrata in context.Contratacion
                                where contrata.Id == id
                                select contrata).FirstOrDefault();
            return contratacion;
        }


        internal List<Contratacion> traerContratacionesRealizadas(int idUsuario)
        {
            return context.Contratacion.Where(x => x.Usuario.Id == idUsuario).ToList();
        }


        internal Contratacion nuevaContratacion(Publicacion publicacion, Usuario usuario)
        {
            Contratacion contratacionNueva = new Contratacion();
            contratacionNueva.IdPublicacion = publicacion.Id;
            contratacionNueva.IdUsuario = usuario.Id;

            context.Contratacion.AddObject(contratacionNueva);
            context.SaveChanges();

            return contratacionNueva;
        }
    }
}