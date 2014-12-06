using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Providere.Controllers
{
    public class SharedController : Controller
    {
        //
        // GET: /Shared/

        public ActionResult _Layout()
        {
            if (this.Session["IdUsuario"] == null)
            {
                return RedirectToAction("CerrarSesion", "Usuario");
            }
            else
            {
                return View();
            }
        }

        public ActionResult _Registracion()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

    }
}
