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

        internal List<Contratacion> traerQuienesMeContrataron(int idUsuario)
        {

            var misPublicaciones = (from publi in context.Publicacion
                                    where publi.IdUsuario == idUsuario
                                    select new { publi.Id });


            List<int> listaPublicaciones = new List<int>();

            foreach (var item in misPublicaciones)
            {
                listaPublicaciones.Add(item.Id);
            }

            var resultado = from cont in context.Contratacion
                            where listaPublicaciones.Contains(cont.IdPublicacion)
                            select cont;

            return resultado.ToList();
        }
         

        internal Contratacion nuevaContratacion(Publicacion publicacion, Usuario usuario)
        {
            Contratacion contratacionNueva = new Contratacion();

            contratacionNueva.IdPublicacion = publicacion.Id;
            contratacionNueva.IdUsuario = usuario.Id;
            contratacionNueva.FlagCalificoProveedor = 0;
            contratacionNueva.FlagCalificoCliente = 0;

            context.Contratacion.AddObject(contratacionNueva);
            context.SaveChanges();

            return contratacionNueva;
        }

        public object TraerContratada(int idPublicacion, int idUsuario)
        {
            var contratacion = (from contrata in context.Contratacion
                                where (contrata.IdUsuario == idUsuario && contrata.IdPublicacion == idPublicacion && contrata.FlagCalificoCliente == 0 && contrata.FlagCalificoProveedor == 0)
                                || (contrata.IdUsuario == idUsuario && contrata.IdPublicacion == idPublicacion &&  contrata.FlagCalificoProveedor == 1 && contrata.FlagCalificoCliente == 0) 
                                || (contrata.IdUsuario == idUsuario && contrata.IdPublicacion == idPublicacion && contrata.FlagCalificoProveedor == 0 && contrata.FlagCalificoCliente == 1)
                                select contrata).FirstOrDefault(); //Traer la que ya fue contratada y no calificada por ambas partes(esa sera la ultima)
            return contratacion;
        }
    }
}