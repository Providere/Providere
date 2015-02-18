using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Repositorios;
using Providere.Models;

namespace Providere.Servicios
{
    public class ReplicaServicios
    {
        ReplicaRepositorio rer = new ReplicaRepositorio();

        public void ReplicarComentario(int id, string replicar)
        {
            rer.ReplicarComentario(id, replicar);
        }

        public List<Replica> TraerTodasLasReplicas()
        {
            return rer.TraerTodasLasReplicas();
        }
    }
}