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

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "SampleCaptcha", "Codigo incorrecto")]
        public ActionResult RegistrarUsuario(Usuario model)
        {
            bool estado = bool.Parse(Request.Form.GetValues("ckbAcepto")[0]);
            if (ModelState.IsValid && estado == true)
            {
                if (us.UsuarioDadoDeBaja(model.Mail))
                {
                    ModelState.AddModelError("", "El usuario registrado con esa direccion de correo electronico fue dado de baja");
                    return View(model);
                }
                else
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

                        TempData["Mensaje"] = "La registración fue exitosa. Revise su correo electronico para activar su cuenta";
                        return RedirectToAction("IniciarSesion");

                    }
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
            ViewBag.Mensaje = TempData["Mensaje"];
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
                    ModelState.AddModelError("", "Verifique su direccion de correo electronico y/o contraseña");
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
        public ActionResult EditarPerfil(string nombre, string apellido, string telefono, string ubicacion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int id = Convert.ToInt16(this.Session["IdUsuario"]);
                    us.ModificarDatosUsuario(id, nombre, apellido, telefono, ubicacion);
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
                    ClientException.LogException(ex, "Error al cargar la imagen");
                    return RedirectToAction("Error", "Shared");
                }
            }
            else
            {
                TempData["Error"] = "No se pudo cargar la imagen, intentelo nuevamente. Debe ser de formato jpg";
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
                    TempData["Mensaje"] = "Su contraseña ha sido modificada con exito";
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
