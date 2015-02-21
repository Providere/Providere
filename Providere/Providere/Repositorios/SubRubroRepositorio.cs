using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;

namespace Providere.Repositorios
{

    public class SubRubroRepositorio
    {
        ProvidereEntities context = new ProvidereEntities();

        internal List<SubRubro> obtenerPorRubro(Rubro rubro)
        {
            var subRubros = (from im in context.SubRubro
                             where im.IdRubro == rubro.Id
                             select im).ToList();
            return subRubros;
        }


        internal SubRubro traerDatosPorId(int id)
        {
            var subrubro = (from sr in context.SubRubro
                            where sr.Id == id
                            select sr).FirstOrDefault();
            return subrubro;

        }
        public Publicacion VerificarSubrubro(int idUsuario, int? idSubRubro)
        {
            var publicacion = (from publicaciones in context.Publicacion
                               where (publicaciones.IdUsuario == idUsuario && publicaciones.IdSubRubro == idSubRubro)
                               select publicaciones).First();
            return publicacion;
        }
    }
}