using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Repositorios;
using Providere.Models;


namespace Providere.Servicios
{
    public class SubRubroServicios
    {
        SubRubroRepositorio sr = new SubRubroRepositorio();
        RubroRepositorio rr = new RubroRepositorio();


        public SubRubro traerDatosPorId(int id)
        {
            return sr.traerDatosPorId(id);
        }

        internal List<SubRubroParaJson> obtenerPorRubro(int rubro)
        {
            Rubro ru = rr.traerDatosPorId(rubro);
            List<SubRubro> listado = sr.obtenerPorRubro(ru);
            List<SubRubroParaJson> listadoParaJson = new List<SubRubroParaJson>();
            foreach (var subrubro in listado)
            {
                SubRubroParaJson subr = new SubRubroParaJson();
                subr.Id = subrubro.Id;
                subr.Nombre = subrubro.Nombre;
                listadoParaJson.Add(subr);
            }
            return listadoParaJson;
        }

        public bool VerificarSubrubro(int idUsuario, int? idSubRubro)
        {
            try
            {
                Publicacion publicacion = sr.VerificarSubrubro(idUsuario, idSubRubro);
            }
            catch
            {
                return false;
            }
            return true;
        }

        internal bool VerificarSubrubroEditar(int idUsuario, int? idSubRubro, int id)
        {
            try
            {
                Publicacion publicacion = sr.VerificarSubrubroEditar(idUsuario, idSubRubro, id);
            }
            catch
            {
                return false;
            }
            return true;
        }

        internal IEnumerable<SubRubro> obtenerTodosEnumerables() { return sr.obtenerTodosEnumerables(); }
    }
}