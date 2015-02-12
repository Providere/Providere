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

            Calificacion calificacion = context.Calificacion.Where(e => e.Id == id).FirstOrDefault();
            calificacion.Denunciado = 1; //comentario de la calificacion fue denunciado

            context.SaveChanges();
        }
    }
}