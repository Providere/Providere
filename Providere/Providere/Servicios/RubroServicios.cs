using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Repositorios;
using Providere.Models;

namespace Providere.Servicios
{
    public class RubroServicios
    {
        RubroRepositorio rr = new RubroRepositorio();
        public List<Rubro> obtenerTodos()
        {
            return rr.obtenerTodos();
        }

        public Rubro traerDatosPorId(int id)
        {
            return rr.traerDatosPorId(id);
        }


    }
}