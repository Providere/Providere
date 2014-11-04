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
        Usuario usuario = new Usuario();
        UsuarioServicios us = new UsuarioServicios();
        public ActionResult RegistrarUsuario()
        {
            ViewBag.RegistracionExitosa = TempData["Exito"];
            return View(usuario);
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "SampleCaptcha", "Codigo incorrecto")]
        public ActionResult RegistrarUsuario(Usuario model)
        {
            bool estado = bool.Parse(Request.Form.GetValues("ckbAcepto")[0]);
            if (ModelState.IsValid && estado == true)
            {

                if (us.EmailExisteActivado(model.Mail))
                {
                    ModelState.AddModelError("", "El mail ingresado ya posee una cuenta asociada");
                    return View(model);
                }
                else
                {
                    if (us.EmailExisteInactivo(model.Mail))
                    {
                        try
                        {
                            us.ActivarUsuarioInactivo(model);
                        }
                        catch (System.Net.Mail.SmtpException)
                        {
                            return RedirectToAction("Error", "Shared");
                        }
                    }
                    else
                    {
                        try
                        {
                            us.AgregarUsuarioNuevo(model);
                        }
                        catch (System.Net.Mail.SmtpException)
                        {
                            return RedirectToAction("Error", "Shared");
                        }
                    }

                    TempData["Exito"] = "La registración fue exitosa. Revise su casilla de mail para activar su cuenta";
                    return RedirectToAction("RegistrarUsuario");

                }
            }

            else
            {
                ModelState.AddModelError("", "Verifique que todos los campos esten completados correctamente");
                return View(model);
            }
        }

        public ActionResult Activar(string codAct)
        {
            string msj;
            if (us.ActivarUsuario(codAct))
            {
                msj = "Muchas gracias por activar su cuenta. El equipo de Providere";
            }
            else
            {
                msj = "Su tiempo para la activacion ha expirado.El equipo de Providere";
            }
            ViewBag.msj = msj;

            return View();
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

    

    }
}
