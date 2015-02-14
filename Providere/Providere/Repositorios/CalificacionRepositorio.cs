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


        internal void calificarUsuario(string comentario, Contratacion contratacion, TipoEvaluacion tipoEvaluacion, TipoCalificacion tipoCalificacion, int idTipoCalificacion)
        {
            Calificacion calificacion = new Calificacion();

            if (idTipoCalificacion == 1)
            {
                calificacion.IdCalificador = contratacion.IdUsuario;
                calificacion.IdCalificado = contratacion.Publicacion.IdUsuario;
            }
            else
            {
                calificacion.IdCalificador = contratacion.Publicacion.IdUsuario;
                calificacion.IdCalificado = contratacion.IdUsuario;
            }

            calificacion.Descripcion = comentario;
            calificacion.IdContratacion = contratacion.Id;
            calificacion.IdTipoCalificacion = tipoCalificacion.Id;
            calificacion.IdTipoEvaluacion = tipoEvaluacion.Id;
            calificacion.FechaCalificacion = DateTime.Now;
            calificacion.FlagDenunciado = 0; //no fue denunciada esa calificacion todavia
            calificacion.FlagReplicado = 0; //no fue replicado esa calificacion todavia

            context.Calificacion.AddObject(calificacion);

            Contratacion cambioEstado = context.Contratacion.Where(e => e.Id == contratacion.Id).FirstOrDefault();
            cambioEstado.FlagCalificoCliente = 1; //en este caso se marca que el cliente califico al prestador

            context.SaveChanges();
        }

      /*  internal List<Calificacion> tieneCalificacionContrataciones(List<Contratacion> listaContratacion)
        {

            List<int> listaContratacionesPorId = new List<int>();


            foreach (var item in listaContratacion)
            {
                listaContratacionesPorId.Add(item.Id);
            }

            var resultado = from calificaciones in context.Calificacion
                            where listaContratacionesPorId.Contains(calificaciones.IdContratacion)
                            select calificaciones;

            return resultado.ToList();

        } */

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
                             where (listaDeContrataciones.Contains(calificacion.IdContratacion) && calificacion.IdTipoCalificacion == 1 && calificacion.IdTipoEvaluacion == 1)
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
                             where (listaDeContrataciones.Contains(calificacion.IdContratacion) && calificacion.IdTipoCalificacion == 1 && calificacion.IdTipoEvaluacion == 1) //para el prestador
                             orderby calificacion.FechaCalificacion descending
                             select calificacion).Take(limite).ToList();
            return resultado;
        }


        public object TraerCalificacionesObtenidas(int idUsuario, int limite)
        {
            var resultados = (from califica in context.Calificacion
                              where califica.IdCalificado == idUsuario
                              select califica).Take(limite).ToList();
            return resultados;
        }

        public object TraerCalificacionesOtorgadas(int idUsuario, int limite)
        {
            var resultados = (from califica in context.Calificacion
                              where califica.IdCalificador == idUsuario
                              select califica).Take(limite).ToList();
            return resultados;
        }

        public object TraerCalificacionObtenidasTodas(int idUsuario)
        {
            var resultados = (from califica in context.Calificacion
                              where califica.IdCalificado == idUsuario
                              select califica).ToList();
            return resultados;
        }

        public object TraerCalificacionesOtorgadasTodas(int idUsuario)
        {
            var resultados = (from califica in context.Calificacion
                              where califica.IdCalificador == idUsuario
                              select califica).ToList();
            return resultados;
        }
    }
}