using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Providere.Models;
using Providere.Servicios;
using BotDetect.Web.UI.Mvc;
using System.Web.Security;

namespace Providere.Controllers
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/
       
        public ActionResult RegistrarUsuario()
        {
            Usuario usuario = new Usuario();
            return View(usuario);
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "SampleCaptcha", "Codigo incorrecto")]
        public ActionResult RegistrarUsuario(Usuario model)
        {
            bool estado = bool.Parse(Request.Form.GetValues("ckbAcepto")[0]);
            if (ModelState.IsValid && estado == true) 
            {
                return View(model);
            }
            else
            {
                ModelState.AddModelError("", "Verifique que todos los campos esten completados correctamente");
                return View(model);
            }
        }

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
