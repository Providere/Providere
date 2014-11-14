using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Providere.Models;
using Providere.Servicios;

namespace Providere.Controllers
{
    public class PublicacionController : Controller
    {
        //
        // GET: /Publicacion/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        PublicacionServicios ps = new PublicacionServicios();
        ProvidereEntities entities = new ProvidereEntities();
        public ActionResult ListarPublicaciones()
        {
            int idUsuario = Convert.ToInt16(this.Session["IdUsuario"]);
            var publicaciones = entities.Publicacion.Include("Rubro"); //Eager loading => Carga temprana
            return View(publicaciones.ToList());
        }

        public ActionResult NuevaPublicacion()
        {
            return View();
        }
    }
}
