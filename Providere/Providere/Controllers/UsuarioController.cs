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
        RubroServicios rs = new RubroServicios();
        PuntajeClienteServicio pcs = new PuntajeClienteServicio();
        PublicacionServicios ps = new PublicacionServicios();
        PuntajeServicios pus = new PuntajeServicios();

        public ActionResult RegistrarUsuario()
        {
            ViewBag.Error = TempData["Error"];
            ViewBag.Accion = "Registracion";
            return View();
        }

        [HttpPost,CaptchaValidation("CaptchaCode", "SampleCaptcha", "Codigo incorrecto")]
        public ActionResult RegistrarUsuario(string nombre, string apellido, string dni, string mail, string telefono, string contrasenia, string CaptchaCode, string geocomplete)
        {
            bool estado = bool.Parse(Request.Form.GetValues("ckbAcepto")[0]);
            if (ModelState.IsValid && estado == true && !string.IsNullOrWhiteSpace(geocomplete))
            {
                if (us.UsuarioDadoDeBaja(mail))
                {
                    ModelState.AddModelError("", "El usuario registrado con esa direccion de correo electronico fue dado de baja.");
                    return View();
                }
                else
                {

                    if (us.UsuarioExisteActivado(mail,dni))
                    {
                        ModelState.AddModelError("", "La direccion de correo electrónico o el DNI ingresado ya posee una cuenta asociada.");
                        return View();
                    }
                    else
                    {
                        if (us.UsuarioExisteInactivo(mail,dni))
                        {
                            try
                            {
                                us.ActivarUsuarioInactivo(nombre,apellido,dni,mail,telefono,contrasenia,geocomplete);
                            }
                            catch (System.Net.Mail.SmtpException ex)
                            {
                                ClientException.LogException(ex, "Error al enviar el mail de activación.");
                                return RedirectToAction("Error", "Shared");
                            }
                        }
                        else
                        {
                            try
                            {
                                us.AgregarUsuarioNuevo(nombre, apellido,dni, mail, telefono, contrasenia, geocomplete);
                            }
                            catch (System.Net.Mail.SmtpException ex)
                            {
                                ClientException.LogException(ex, "Error al enviar el mail de activación.");
                                return RedirectToAction("Error", "Shared");
                            }
                        }

                        TempData["Exito"] = "La registración fue exitosa. Revisa tu correo electrónico para activar la cuenta.";
                        return RedirectToAction("IniciarSesion");

                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "No te olvides de ingresar ubicación, y aceptar los términos y condiciones para continuar con la registración.");
                return View();
            }

        }

        public ActionResult Activar(string codAct)
        {
            string msj;
            if (us.ActivarUsuario(codAct))
            {
                msj = "Tu cuenta ha sido activada satisfactoriamente. Ya podes comenzar a utilizar Providere.";
            }
            else
            {
                msj = "El tiempo para la activación de su cuenta ha expirado, volvé a registrarte para recibir un nuevo mail de activación.";
            }
            ViewBag.msj = msj;

            return View();
        }

        public ActionResult IniciarSesion()
        {
            ViewBag.Exito = TempData["Exito"];
            ViewBag.Error = TempData["Error"];

            ViewBag.Accion = "Identificacion";

            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult IniciarSesion(Usuario model, string returnUrl)
        {
            int cantidadDeErrores = 0;

            if (model.Mail.Length > 50)
            {
                ModelState.AddModelError("", "Dirección de correo electrónico demasiado larga.");
                cantidadDeErrores++;
            }

            if (model.Contrasenia.Length > 10)
            {
                ModelState.AddModelError("", "Contraseña demasiada larga.");
                cantidadDeErrores++;
            }

            if (cantidadDeErrores == 0)
            {
                model.Contrasenia = Encryptor.MD5Hash(model.Contrasenia);

                if (us.UsuarioExistente(model))
                {
                    if (us.UsuarioActivo(model) || us.UsuarioBloqueado(model))
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
                        ModelState.AddModelError("", "Usuario inactivo o dado de baja.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Verifica tu dirección de correo electrónico y/o contraseña.");
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

        public ActionResult EditarPerfil(int? ActivePanel)
        {
            ImageHelper ih = new ImageHelper();
            int idUsuario = Convert.ToInt16(this.Session["IdUsuario"]);
            Usuario usuario = us.ObtenerUsuarioEditar(idUsuario);
            ViewBag.geocomplete2 = usuario.Ubicacion;
            ViewBag.Rubros = rs.obtenerTodos();

            ViewBag.Exito = TempData["Exito"];
            ViewBag.Error = TempData["Error"];

            if (ActivePanel == null)
            {
                ViewBag.ActivePanel = 1;
            }
            else
            {
                ViewBag.ActivePanel = ActivePanel;
            }

            ViewBag.ProfileImageExists = ih.ProfileImageExists(usuario);
            return View(usuario);
        }

        [HttpPost]
        public ActionResult EditarPerfil(string nombre, string apellido, string dni, string telefono, string geocomplete2)
        {
            int id = Convert.ToInt16(this.Session["IdUsuario"]);
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(geocomplete2))
            {
                try
                {
                 
                    us.ModificarDatosUsuario(id, nombre, apellido,dni, telefono, geocomplete2);
                    TempData["Exito"] = "Tus datos personales se actualizaron correctamente.";
                    return RedirectToAction("Home", "Home");
                }
                catch (Exception ex)
                {
                    ClientException.LogException(ex, "Error al modificar tus datos.");
                    return RedirectToAction("Error", "Shared");
                }
            }
            else
            {
                TempData["Error"] = "No se pudo modificar tus datos, intentalo nuevamente.";
                return RedirectToAction("EditarPerfil", new { id = id });
            }

        }

        [HttpPost]
        public ActionResult CambiarFotoPerfil(HttpPostedFileBase file)
        {
            string extension = Path.GetExtension(file.FileName);
            int id = Convert.ToInt16(this.Session["IdUsuario"]);
            if (file != null && file.ContentLength > 0 && extension == ".jpg")
            {
                try
                {
                    string uniqueFileName = Path.ChangeExtension("imagen", Convert.ToString(id));
                    string path = Path.Combine(Server.MapPath("~/Imagenes/FotoPerfil"),
                                       Path.GetFileName(uniqueFileName + extension));
                    file.SaveAs(path);

                    TempData["Exito"] = "Tu foto de perfil se ha cargado con éxito.";

                    return RedirectToAction("Home", "Home");
                }
                catch (Exception ex)
                {
                    ClientException.LogException(ex, "Error al cargar la imágen.");
                    return RedirectToAction("Error", "Shared");
                }
            }
            else
            {
                TempData["Error"] = "No se pudo cargar la imagen, intentalo nuevamente.";

                return RedirectToAction("EditarPerfil", new { id = id });
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
                    TempData["Exito"] = "Tu contraseña ha sido modificada con éxito.";
                    return RedirectToAction("Home", "Home");
                }
                catch (Exception ex)
                {
                    ClientException.LogException(ex, "Error al modificar tu contraseña.");
                    return RedirectToAction("Error", "Shared");
                }
            }
            else
            {
                TempData["Error"] = "Tu contraseña no pudo ser modificada, intentalo nuevamente.";
                return RedirectToAction("EditarPerfil", new { id = id });
            }
        }

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
                TempData["Error"] = "No se pudo eliminar tu cuenta, intentalo nuevamente.";
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

        //Recuperar contrasenia:
        public ActionResult OlvideContrasenia()
        {
            ViewBag.Error = TempData["Error"];
            return View();
        }


        [HttpPost]
        public ActionResult OlvideContrasenia(string mail)
        {
            //verificar si ese usuario existe en la BD y esta activo, asi puede recuperar su contraseña:
            if (ModelState.IsValid && us.VerificarIdentidad(mail))
            {
                try
                {
                    us.MailRecuperarContrasenia(mail);

                }
                catch (System.Net.Mail.SmtpException ex)
                {
                    ClientException.LogException(ex, "Error al enviar el mail.");
                    return RedirectToAction("Error", "Shared");
                }
            }
            else
            {
                TempData["Error"] = "Correo electrónico inexistente.";
                return RedirectToAction("OlvideContrasenia");
            }
            TempData["Exito"] = "Correo electrónico enviado exitosamente.";
            return RedirectToAction("IniciarSesion");
        }


        public ActionResult NuevaContrasenia(string id)
        {
            return View();
        }


        [HttpPost]
        public ActionResult NuevaContrasenia(string id, string contrasenia)
        {
            if (ModelState.IsValid)
            {
                us.RestablecerContrasenia(id, contrasenia);

                TempData["Exito"] = "Contraseña modificada exitosamente!";

                return RedirectToAction("IniciarSesion");
            }
            else
            {
                return View("Index", "Index");
            }
        }

        public ActionResult VerPerfil(int idUsuario)
        {
            Usuario usuario = us.traerUsuario(idUsuario);

            var puntaje = pcs.TraerPuntajeCliente(idUsuario);

            ViewBag.PuntajeCliente = puntaje;

            // Busco las publicaciones
            var publicaciones = ps.ListarMisPublicaciones(idUsuario);

            ViewBag.ListaPublicaciones = publicaciones;

            // Busco los puntajes
            var puntajes = pus.traerPuntajesDePublicacionesDeUnUsuario(publicaciones);

            ViewBag.ListaPuntajes = puntajes;

            return View(usuario);
        }
    }
}
