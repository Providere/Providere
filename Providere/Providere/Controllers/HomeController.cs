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

        public ActionResult Home()
        {
            
            ViewBag.Exito = TempData["Exito"];
            ViewBag.Error = TempData["Error"];

            ViewBag.PublicacionesMasRecientes = ps.traerPublicacionesMasRecientes(4);

            ViewBag.PublicacionesMejorCalificadas = ps.traerPublicacionesMejorCalificadas(4);

            ViewBag.UsuariosMasCercanos = us.traerPorZona(us.traerUsuario(Convert.ToInt16(this.Session["IdUsuario"])), 4);

            ViewBag.Usuario = us.ObtenerUsuarioEditar(Convert.ToInt16(this.Session["IdUsuario"]));

            return View();
        }

    }
}
