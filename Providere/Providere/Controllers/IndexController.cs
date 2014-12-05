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
        RubroServicios rs = new RubroServicios();
        ProvidereEntities context = new ProvidereEntities();

        public ActionResult Index()
        {
            ViewBag.Publicaciones = ps.traerPublicacionesMejorCalificadas(4);
            ViewBag.Rubros = rs.obtenerTodos();

            return View();
        }

    }
}
