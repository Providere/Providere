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
                    ModelState.AddModelError("", "La direccion de correo electronico ingresado ya posee una cuenta asociada");
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
                        catch (System.Net.Mail.SmtpException ex)
                        {
                            ClientException.LogException(ex, "Error al enviar el mail de activacion");
                            return RedirectToAction("Error", "Shared");
                        }
                    }
                    else
                    {
                        try
                        {
                            us.AgregarUsuarioNuevo(model);
                        }
                        catch (System.Net.Mail.SmtpException ex)
                        {
                            ClientException.LogException(ex, "Error al enviar el mail de activacion");
                            return RedirectToAction("Error", "Shared");
                        }
                    }

                    TempData["Exito"] = "La registración fue exitosa. Revise su correo electronico para activar su cuenta";
                    return RedirectToAction("Home,Home");

                }
            }

            else
            {
                ModelState.AddModelError("", "Verifique que todos los campos esten completados correctamente. Debe aceptar los terminos y condiciones");
                return View(model);
            }
        }

        public ActionResult Activar(string codAct)
        {
            string msj;
            if (us.ActivarUsuario(codAct))
            {
                msj = "Su cuenta ha sido activada satisfactoriamente. Ya puede comenzar a utilizar Providere";
            }
            else
            {
                msj = "El tiempo para la activacion de su cuenta ha expirado, vuelva a registrarse para recibir un nuevo mail de activacion";
            }
            ViewBag.msj = msj;

            return View();
        }

        public ActionResult IniciarSesion()
        {

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult IniciarSesion(Usuario model, string returnUrl)
        {
            int cantidadDeErrores = 0;

            if (model.Mail.Length > 50)
            {
                ModelState.AddModelError("", "Direccion de correo electronico demasiado largo");
                cantidadDeErrores++;
            }

            if (model.Contrasenia.Length > 10)
            {
                ModelState.AddModelError("", "Contraseña demasiado larga");
                cantidadDeErrores++;
            }

            if (cantidadDeErrores == 0)
            {
                model.Contrasenia = Encryptor.MD5Hash(model.Contrasenia);
                if (us.UsuarioInexistente(model))
                {
                    ModelState.AddModelError("", "Usuario inexistente");
                }
                else
                {
                    if (us.UsuarioExistente(model))
                    {
                        if (us.UsuarioActivo(model))
                        {
                            us.CrearCookie(model);
                            Session["IdUsuario"] = us.traerIdUsuario(model);
                            if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                            {
                                return Redirect(returnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Home", "Home");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Usuario inactivo");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Verifique su direccion de correo electronico y/o contraseña");
                    }
                }
            }
            return View(model);
        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index", "Index");
        }

        public ActionResult EditarPerfil()
        {
            int idUsuario = Convert.ToInt16(this.Session["IdUsuario"]);
            Usuario usuario = us.ObtenerUsuarioEditar(idUsuario);
            return View(usuario);
        }

        [HttpPost]
        public ActionResult EditarPerfil(int id, string nombre, string apellido, string telefono, string ubicacion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    us.ModificarDatosUsuario(id, nombre, apellido, telefono, ubicacion);
                    TempData["Exito"] = "Sus datos personales se actualizaron correctamente";
                    return RedirectToAction("Home", "Home");
                }
                catch (Exception ex)
                {
                    ClientException.LogException(ex, "Error al modificar sus datos");
                    return RedirectToAction("Error", "Shared");
                }
            }
            else
            {
                ModelState.AddModelError("", "No se puede modificar sus datos, intentelo nuevamente");
                return View();
            }
        }

        [HttpPost]
        public ActionResult CambiarContrasenia(int id, string contrasenia)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    us.GuardarContraseniaNueva(id, contrasenia);
                    TempData["Exito"] = "Su contraseña ha sido modificada con exito";
                    return RedirectToAction("Home", "Home");
                }
                catch (Exception ex)
                {
                    ClientException.LogException(ex, "Error al modificar su contraseña");
                    return RedirectToAction("Error", "Shared");
                }
            }
            else
            {
                ModelState.AddModelError("", "No se puede modificar su contraseña, intentelo nuevamente");
                return View();
            }
        }

        [HttpPost]
        public ActionResult EliminarCuenta()
        {

            return View(); //Hacer logout
        }
    }
}
