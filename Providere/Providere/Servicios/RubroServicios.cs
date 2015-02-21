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

        public bool VerificarRubro(int idUsuario, int idRubro)
        {
            try
            {
                Publicacion publicacion = rr.VerificarRubro(idUsuario, idRubro);
            }
            catch
            {
                return false;
            }
            return true;
        }



        internal bool VerificarRubroEditar(int idUsuario, int idRubro, int id)
        {
            try
            {
                Publicacion publicacion = rr.VerificarRubroEditar(idUsuario, idRubro,id);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}