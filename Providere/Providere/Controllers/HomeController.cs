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

        public ActionResult Home(Usuario user)
        {
            ViewBag.Mensaje = TempData["Mensaje"];
            ViewBag.Error = TempData["Error"];

            ViewBag.PublicacionesMasRecientes = ps.traerPublicacionesMasRecientes(4);

            ViewBag.PublicacionesMejorCalificadas = ps.traerPublicacionesMejorCalificadas(4);

            ViewBag.PublicacionesMasCercanas = ps.traerPublicacionesMasCercanas(user.Ubicacion, 4);

           // ViewBag.PublicacionesMasCercanas = ps.traerPublicacionesMasCercanas(user.Ubicacion, 4);


            return View();
        }

    }
}
