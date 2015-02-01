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

        internal List<Calificacion> tieneCalificacionContrataciones(List<Contratacion> listaContratacion)
        {
            /*var calificacion = (from calif in context.Calificacion
                                where calif.IdContratacion == cont.Id
                                and calif.IdUsuario == cont.IdUsuario
                                select calif).FirstOrDefault();*/

            List<int> listaContratacionesPorId = new List<int>();


            foreach (var item in listaContratacion)
            {
                listaContratacionesPorId.Add(item.Id);
            }

            var resultado = from calificaciones in context.Calificacion
                            where listaContratacionesPorId.Contains(calificaciones.IdContratacion)
                            select calificaciones;

            return resultado.ToList();
        }
    }
}