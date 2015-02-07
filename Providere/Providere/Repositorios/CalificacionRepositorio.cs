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

        public List<Calificacion> TraerCalificaciones(int idPublicacion)
        {
            var contratacion = (from contrata in context.Contratacion
                                where contrata.IdPublicacion == idPublicacion
                                select new { contrata.Id });
            List<int> listaDeContrataciones = new List<int>();
            foreach (var item in contratacion)
            {
                listaDeContrataciones.Add(item.Id);
            }
            var resultado = (from calificacion in context.Calificacion
                             where (listaDeContrataciones.Contains(calificacion.IdContratacion) && calificacion.IdTipoCalificacion == 1)
                             orderby calificacion.FechaCalificacion descending
                             select calificacion).ToList();
            return resultado;
        }

        //Trae las primeras 5 calificaciones para mostrar en la publicacion
        public object TraerPrimerasCalificaciones(int limite, int idPublicacion)
        {
            var contratacion = (from contrata in context.Contratacion
                                where contrata.IdPublicacion == idPublicacion
                                select new { contrata.Id });
            List<int> listaDeContrataciones = new List<int>();
            foreach (var item in contratacion)
            {
                listaDeContrataciones.Add(item.Id);
            }
            var resultado = (from calificacion in context.Calificacion
                             where (listaDeContrataciones.Contains(calificacion.IdContratacion) && calificacion.IdTipoCalificacion == 1) //para el prestador
                             orderby calificacion.FechaCalificacion descending
                             select calificacion).Take(limite).ToList();
            return resultado;
        }
    
    }
}