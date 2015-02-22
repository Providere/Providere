using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Providere.Models;
using Providere.Servicios;

namespace Providere.Controllers
{
    public class IndexController : Controller
    {
        //
        // GET: /Index/

        PublicacionServicios ps = new PublicacionServicios();
        ProvidereEntities context = new ProvidereEntities();

        public ActionResult Index()
        {
            ViewBag.Publicaciones = ps.traerPublicacionesMasRecientes(4);

            if (this.Session["IdUsuario"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Home", "Home");
            }
        }

    }
}
