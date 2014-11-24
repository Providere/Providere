using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Providere.Models;
using Providere.Servicios;
using BotDetect.Web.UI.Mvc;
using System.Web.Security;
using System.IO;

namespace Providere.Controllers
{
    public class UsuarioController : Controller
    {

        UsuarioServicios us = new UsuarioServicios();

        public ActionResult RegistrarUsuario()
        {

            return View();
        }

        [HttpPost,CaptchaValidation("CaptchaCode", "SampleCaptcha", "Codigo incorrecto")]
        public ActionResult RegistrarUsuario(string nombre, string apellido, string mail, string telefono, string contrasenia, string geocomplete)
        {
            bool estado = bool.Parse(Request.Form.GetValues("ckbAcepto")[0]);
            if (ModelState.IsValid && estado == true && !string.IsNullOrWhiteSpace(geocomplete))
            {
                if (us.UsuarioDadoDeBaja(mail))
                {
                    ModelState.AddModelError("", "El usuario registrado con esa direccion de correo electronico fue dado de baja");
                    return View();
                }
                else
                {

                    if (us.EmailExisteActivado(mail))
                    {
                        ModelState.AddModelError("", "La direccion de correo electrónico ingresado ya posee una cuenta asociada");
                        return View();
                    }
                    else
                    {
                        if (us.EmailExisteInactivo(mail))
                        {
                            try
                            {
                                us.ActivarUsuarioInactivo(nombre,apellido,mail,telefono,contrasenia,geocomplete);
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
                                us.AgregarUsuarioNuevo(nombre, apellido, mail, telefono, contrasenia, geocomplete);
                            }
                            catch (System.Net.Mail.SmtpException ex)
                            {
                                ClientException.LogException(ex, "Error al enviar el mail de activacion");
                                return RedirectToAction("Error", "Shared");
                            }
                        }

                        TempData["Mensaje"] = "La registración fue exitosa. Revise su correo electrónico para activar su cuenta";
                        return RedirectToAction("IniciarSesion");

                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "No se olvide de ingresar ubicación, y aceptar los términos y condiciones para continuar con la registración");
                return View();
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
                msj = "El tiempo para la activación de su cuenta ha expirado, vuelva a registrarse para recibir un nuevo mail de activación";
            }
            ViewBag.msj = msj;

            return View();
        }

        public ActionResult IniciarSesion()
        {
            ViewBag.Mensaje = TempData["Mensaje"];
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult IniciarSesion(Usuario model, string returnUrl)
        {
            int cantidadDeErrores = 0;

            if (model.Mail.Length > 50)
            {
                ModelState.AddModelError("", "Direccion de correo electrónico demasiado largo");
                cantidadDeErrores++;
            }

            if (model.Contrasenia.Length > 10)
            {
                ModelState.AddModelError("", "Contraseña demasiada larga");
                cantidadDeErrores++;
            }

            if (cantidadDeErrores == 0)
            {
                model.Contrasenia = Encryptor.MD5Hash(model.Contrasenia);

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
                        ModelState.AddModelError("", "Usuario inactivo o dado de baja");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Verifique su dirección de correo electrónico y/o contraseña");
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
            ViewBag.geocomplete2 = usuario.Ubicacion;
            return View(usuario);
        }

        [HttpPost]
        public ActionResult EditarPerfil(string nombre, string apellido, string telefono, string geocomplete2)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(geocomplete2))
            {
                try
                {
                    int id = Convert.ToInt16(this.Session["IdUsuario"]);
                    us.ModificarDatosUsuario(id, nombre, apellido, telefono, geocomplete2);
                    TempData["Mensaje"] = "Sus datos personales se actualizaron correctamente";
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
                TempData["Error"] = "No se pudo modificar sus datos, intentelo nuevamente";
                return RedirectToAction("Home", "Home");
            }

        }

        [HttpPost]
        public ActionResult CambiarFotoPerfil(HttpPostedFileBase file)
        {
            string verificarExt = Path.GetExtension(file.FileName);
            if (file != null && file.ContentLength > 0 && verificarExt == ".jpg")
            {
                try
                {
                    int id = Convert.ToInt16(this.Session["IdUsuario"]);
                    string extension = Path.GetExtension(file.FileName);
                    string uniqueFileName = Path.ChangeExtension("imagen", Convert.ToString(id));
                    string path = Path.Combine(Server.MapPath("~/Imagenes/FotoPerfil"),
                                       Path.GetFileName(uniqueFileName + extension));
                    file.SaveAs(path);

                    TempData["Mensaje"] = "Su foto de perfil se ha cargado con exito";
                    return RedirectToAction("Home", "Home");
                }
                catch (Exception ex)
                {
                    ClientException.LogException(ex, "Error al cargar la imágen");
                    return RedirectToAction("Error", "Shared");
                }
            }
            else
            {
                TempData["Error"] = "No se pudo cargar la imágen, intentelo nuevamente. Debe ser de formato jpg";
                return RedirectToAction("Home", "Home");
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
                    TempData["Mensaje"] = "Su contraseña ha sido modificada con éxito";
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
                TempData["Error"] = "Su contraseña no pudo ser modificada, intentelo nuevamente";
                return RedirectToAction("Home", "Home");
            }
        }

        [HttpPost]
        public ActionResult EliminarCuenta()
        {
            bool estado = bool.Parse(Request.Form.GetValues("ckbEliminar")[0]);

            if (estado == true)
            {
                int idUsuario = Convert.ToInt16(this.Session["IdUsuario"]);
                //Actualizar BD nuevo estado:
                us.DarDeBajaUsuario(idUsuario);
                return RedirectToAction("CerrarSesion");
            }
            else
            {
                TempData["Error"] = "No se pudo eliminar su cuenta, intentelo nuevamente";
                return RedirectToAction("Home", "Home");
            }
        }

        public ActionResult MostrarImagen(int id)
        {
            var path = Server.MapPath("~/Imagenes/FotoPerfil");
            var file = string.Format("imagen.{0}.jpg", id);
            var fullPath = Path.Combine(path, file);
            if (!System.IO.File.Exists(fullPath))
            {
                var path2 = Server.MapPath("~/Content/img/user.jpg");
                return File(path2, "Content/img");
            }
            else
            {
                return File(fullPath, "Imagenes/FotoPerfil", file);
            }
        }
    }
}
