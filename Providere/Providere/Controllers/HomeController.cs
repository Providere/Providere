using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Providere.Servicios;
using Providere.Models;

namespace Providere.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        PublicacionServicios ps = new PublicacionServicios();
        UsuarioServicios us = new UsuarioServicios();
        SancionServicios ss = new SancionServicios();

        public ActionResult Home()
        {
            
            ViewBag.Exito = TempData["Exito"];
            ViewBag.Error = TempData["Error"];

            int limite = 4;

            ViewBag.PublicacionesMasRecientes = ps.traerPublicacionesMasRecientes(limite);

            ViewBag.PublicacionesMejorCalificadas = ps.traerPublicacionesMejorCalificadas(limite);

            ViewBag.UsuariosMasCercanos = us.traerPorZona(us.traerUsuario(Convert.ToInt16(this.Session["IdUsuario"])), limite);

            ViewBag.Sancion = ss.ObtenerSancionDeUsuario(us.ObtenerUsuarioEditar(Convert.ToInt16(this.Session["IdUsuario"])));
            
            return View();
        }

    }
}
