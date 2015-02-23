using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;
using Providere.Servicios;

namespace Providere.Repositorios
{
    public class RubroRepositorio
    {
        ProvidereEntities context = new ProvidereEntities();

        internal Rubro traerDatosPorId(int id)
        {
            var rubro = (from rr in context.Rubro
                         where rr.Id == id
                         select rr).FirstOrDefault();
            return rubro;
        }

        internal List<Rubro> obtenerTodos()
        {
            return context.Rubro.OrderBy(item => item.Nombre).ToList();
        }

        public Publicacion VerificarRubro(int idUsuario, int idRubro)
        {
            var publicacion = (from publicaciones in context.Publicacion
                               where (publicaciones.IdUsuario == idUsuario && publicaciones.IdRubro == idRubro
                                && publicaciones.IdRubro < 20)
                               select publicaciones).First();
            return publicacion;
        }



        internal Publicacion VerificarRubroEditar(int idUsuario, int idRubro, int id)
        {
            var publicacion = (from publicaciones in context.Publicacion
                               where (publicaciones.IdUsuario == idUsuario && publicaciones.IdRubro == idRubro
                                && publicaciones.IdRubro < 20 && publicaciones.Id == id)
                               select publicaciones).First();
            return publicacion;
        }
    }
}