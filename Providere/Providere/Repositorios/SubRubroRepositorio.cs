﻿using System;
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
                             select im).OrderBy(item => item.Nombre).ToList();
            return subRubros;
        }

        internal IEnumerable<SubRubro> obtenerTodosEnumerables()
        {
            return context.SubRubro.OrderBy(item => item.Nombre);
        }
        internal IEnumerable<SubRubro> obtenerTodos()
        {
            return context.SubRubro.OrderBy(item => item.Nombre).ToList();
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

        internal Publicacion VerificarSubrubroEditar(int idUsuario, int? idSubRubro, int id)
        {
            var publicacion = (from publicaciones in context.Publicacion
                               where (publicaciones.IdUsuario == idUsuario && publicaciones.IdSubRubro == idSubRubro && publicaciones.Id == id)
                               select publicaciones).First();
            return publicacion;
        }
    }
}