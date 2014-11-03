using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Providere.Controllers
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/

        public ActionResult RegistrarUsuario()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult RegistrarUsuario()
        //{
        //    return View();
        //}

        public ActionResult IniciarSesion()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult IniciarSesion()
        //{
        //    return View();
        //}

        public ActionResult EditarPerfil()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult EditarPerfil()
        //{
        //    return View();
        //}

        public ActionResult Activar()
        {
            return View();
        }

    }
}
