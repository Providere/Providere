using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;

namespace Providere.Repositorios
{
    public class ReplicaRepositorio
    {
        ProvidereEntities context = new ProvidereEntities();

        public void ReplicarComentario(int id, string replicar)
        {
            Replica replica = new Replica();
            replica.IdCalificacion = Convert.ToInt16(id);
            replica.Comentario = replicar;
            context.Replica.AddObject(replica);

            Calificacion calificacion = context.Calificacion.Where(e => e.Id == id).FirstOrDefault();
            calificacion.Replicado = 1; //comentario de la calificacion fue replicado
            context.SaveChanges();
            
        }

        public object TraerTodasLasReplicas()
        {
            var resultado = (from replica in context.Replica
                             select replica).ToList();
            return resultado;
        }
    }
}