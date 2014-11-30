using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;
using Providere.Repositorios;

namespace Providere.Servicios
{
    public class PublicacionServicios
    {
        PublicacionRepositorio pr = new PublicacionRepositorio();

        public ListaPublicacionesModel traerPublicacionesMasPopulares(int limite)
        {
           return pr.traerPublicacionesMasPopulares(limite);
        }

        internal void CrearNuevaPublicacion(int idUsuario, int idRubro, int idSubRubro, string titulo, string descripcion,int precioOpcion, string precio)
        {
            pr.CrearNuevaPublicacion(idUsuario, idRubro, idSubRubro, titulo, descripcion, precioOpcion, precio);
        }
    }
}