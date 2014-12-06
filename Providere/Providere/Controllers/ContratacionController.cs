using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Providere.Servicios;
using Providere.Models;

namespace Providere.Controllers
{
    public class ContratacionController : Controller
    {
        //
        // GET: /Contratacion/
        ContratacionServicios cs = new ContratacionServicios();
        UsuarioServicios us = new UsuarioServicios();
        public ActionResult Index()
        {

            int idUsuario = Convert.ToInt16(this.Session["IdUsuario"]);
            var contrataciones = cs.traerContratacionesRealizadas(idUsuario);

            return View(contrataciones);
        }

        public ActionResult Contratar(Publicacion publicacion)
        {

            int idUsuario = Convert.ToInt16(this.Session["IdUsuario"]);
            var usuario = us.ObtenerUsuarioEditar(idUsuario);
            var contratacion = cs.nuevaContratacion(publicacion, usuario);

            return RedirectToAction("Index");
        }

    }
}
