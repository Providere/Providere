using System;
using System.Collections.Generic;
using System.Linq;
using Providere.Models;
using Providere.Servicios;
using System.Web;

namespace Providere.Repositorios
{
    public class DenunciaRepositorio
    {
        ProvidereEntities context = new ProvidereEntities();        

        /**
         * Trae las denuncias efectuadas entre el dia del analisis de denuncias
         * y 30 dias antes
         * */
        internal List<Denuncia> traerDenunciasDelMes()
        {
            return context.Denuncia.Where(x => x.Fecha < DateTime.Now)
                .Where(x => x.Fecha > (
                    DateTime.Now.AddDays(-DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month)
                    )
                    )
                    ).ToList();
        }

        public void DenunciarComentario(int id)
        {
            Denuncia denuncia = new Denuncia();
            denuncia.IdCalificacion = Convert.ToInt16(id);
            denuncia.Fecha = DateTime.Now;
            context.Denuncia.AddObject(denuncia);
            context.SaveChanges();
        }

        public Denuncia VerificarComentarioDenunciado(int id)
        {
            var denuncia = (from de in context.Denuncia
                            where (de.IdCalificacion == id)
                            select de).FirstOrDefault();
            return denuncia;
        }

        public List<Denuncia> TraerDenunciados(int idUsuario)
        {
            var calificacionesOtorgadas = (from califica in context.Calificacion
                                           where califica.IdCalificado == idUsuario
                                           select new { califica.Id});


            List<int> listaCalificacionesOtorgadas = new List<int>();

            foreach (var item in calificacionesOtorgadas)
            {
                listaCalificacionesOtorgadas.Add(item.Id);
            }

            var resultado = from denun in context.Denuncia
                            where  listaCalificacionesOtorgadas.Contains(denun.IdCalificacion)              
                            select denun;

            return resultado.ToList();
        }
    }
}