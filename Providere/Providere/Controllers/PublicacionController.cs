using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Providere.Models;
using Providere.Servicios;
using System.IO;

namespace Providere.Controllers
{
    public class PublicacionController : Controller
    {
        PublicacionServicios ps = new PublicacionServicios();
        ProvidereEntities context = new ProvidereEntities();


        public ActionResult ListarPublicaciones()
        {
            int idUsuario = Convert.ToInt16(this.Session["IdUsuario"]);
            var publicaciones = ps.ListarMisPublicaciones(idUsuario);

            ViewBag.Error = TempData["Error"];
            ViewBag.Mensaje = TempData["Mensaje"];

            return View(publicaciones);
        }

        // Listado publico
        public ActionResult Listar()
        {
            int idUsuario = Convert.ToInt16(this.Session["IdUsuario"]);
            var publicaciones = ps.ListarMisPublicaciones(idUsuario);

            return View();
        }

        public ActionResult NuevaPublicacion()
        {
            ViewBag.IdRubro = new SelectList(context.Rubro, "Id", "Nombre");
            ViewBag.IdSubRubro = new SelectList(context.SubRubro, "Id", "Nombre");
            ViewBag.Error = TempData["Error"];

            return View();
        }

        [HttpPost]
        public ActionResult NuevaPublicacion(int idUsuario, int idRubro, int? idSubRubro, string titulo, string descripcion, int precioOpcion, decimal? precio, IEnumerable<HttpPostedFileBase> files)
        {         
            if (ps.VerificarRubro(idUsuario, idRubro) || ps.VerificarSubrubro(idUsuario,idSubRubro))
            {
                TempData["Error"] = "Ya tiene una publicacion creada en ese rubro o subrubro";
                return RedirectToAction("NuevaPublicacion");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        ps.CrearNuevaPublicacion(idUsuario, idRubro, idSubRubro, titulo, descripcion, precioOpcion, precio);

                        if (files.First() != null && files.Count() < 5)
                        {
                            foreach (var file in files)
                            {

                                string extension = Path.GetExtension(file.FileName);
                                if (file.ContentLength > 0 && extension == ".jpg")
                                {
                                    string uniqueFileName = Path.ChangeExtension(file.FileName, Convert.ToString(idUsuario));
                                    string path = Path.Combine(Server.MapPath("~/Imagenes/Publicacion"),
                                                Path.GetFileName(uniqueFileName + extension));
                                    file.SaveAs(path);
                                    string pathImagen = uniqueFileName + extension;

                                    ps.CargarImagenes(pathImagen, idUsuario);
                                }
                            }
                            return RedirectToAction("ListarPublicaciones");
                        }
                        else
                        {
                            return RedirectToAction("ListarPublicaciones");
                        }
                    }
                    catch (Exception ex)
                    {
                        ClientException.LogException(ex, "Error al crear la publicación");
                        return RedirectToAction("Error", "Shared");
                    }
                }
                else
                {
                    TempData["Error"] = "No se pudo crear la publicación, intentelo nuevamente";
                    return RedirectToAction("ListarPublicaciones");
                }
            }

        }

        public ActionResult VisualizarMiPublicacion(int Id)
        {
            int idUsuario = Convert.ToInt16(this.Session["IdUsuario"]);
            Publicacion miPublicacion = ps.TraerPublicacion(Id, idUsuario);

            if (miPublicacion.Estado == 0) //Deshabilitado
            {
                TempData["Error"] = "No puede visualizar su publicación porque esta deshabilitada";
                return RedirectToAction("ListarPublicaciones");
            }
            else
            {
                if (ps.NoExistenImagenes(Id) == false)
                {
                    ViewBag.NoExistenImagenes = "No existen imagenes para mostrar";
                }

                ViewBag.accionPadre = "VisualizarMiPublicacion";
                return View(miPublicacion);
            }
        }

        // Publicacion/VisualizarPublicacion/12
        public ActionResult VisualizarPublicacion(int idPublicacion)
        {
            Publicacion miPublicacion = ps.TraerPublicacionPorId(idPublicacion);

            ViewBag.accionPadre = "VisualizarPublicacion";
            return View("VisualizarMiPublicacion", miPublicacion);
        }

        public ActionResult MostrarFotoPerfil(int id)
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

        public ActionResult EditarPublicacion(int id)
        {
            int idUsuario = Convert.ToInt16(this.Session["IdUsuario"]);
            Publicacion publicacion = ps.TraerPublicacion(id, idUsuario);

            ViewBag.Mensaje = TempData["Mensaje"];
            ViewBag.Error = TempData["Error"];

            if (publicacion.Estado == 0) //Deshabilitado
            {
                TempData["Error"] = "No se puede editar la publicación porque esta deshabilitada";
                return RedirectToAction("ListarPublicaciones");
            }

            else
            {
                if (ps.NoExistenImagenes(id) == false) //Si devuelve false es porque no existen imagenes para esa publicacion
                {
                    ViewBag.NoExistenImagenes = "No existen imagenes para mostrar";
                }

                ViewBag.IdRubro = new SelectList(context.Rubro, "Id", "Nombre", publicacion.IdRubro);
                ViewBag.IdSubRubro = new SelectList(context.SubRubro, "Id", "Nombre", publicacion.IdSubRubro);

                return View(publicacion);
            }
        }

        [HttpPost]
        public ActionResult EditarPublicacion(int idUsuario,int id, int idRubro, int? idSubRubro, string titulo, string descripcion, int precioOpcion, decimal? precio,decimal? oculto, IEnumerable<HttpPostedFileBase> files)
        {
            if (ps.VerificarRubro(idUsuario, idRubro) || ps.VerificarSubrubro(idUsuario, idSubRubro))
            {
                TempData["Error"] = "Ya tiene una publicacion creada en ese rubro o subrubro";
                return RedirectToAction("EditarPublicacion", new { id = id });
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        ps.ModificarPublicacion(id, idRubro, idSubRubro, titulo, descripcion, precioOpcion, precio, oculto);
                        int verificar = ps.VerificarCantidadImagenes(id); // Verifico para no pasar la cant de 4 imagenes.Devuelve la cantidad real o sino 0
                        int cant = files.Count();
                        int total = (verificar + cant);
                        if (files.First() != null && total < 5)
                        {
                            foreach (var file in files)
                            {

                                string extension = Path.GetExtension(file.FileName);
                                if (file.ContentLength > 0 && extension == ".jpg")
                                {
                                    string uniqueFileName = Path.ChangeExtension(file.FileName, Convert.ToString(idUsuario));
                                    string path = Path.Combine(Server.MapPath("~/Imagenes/Publicacion"),
                                                Path.GetFileName(uniqueFileName + extension));
                                    file.SaveAs(path);
                                    string pathImagen = uniqueFileName + extension;

                                    ps.CargarImagenesEdicion(pathImagen, idUsuario, id);
                                }
                            }
                            return RedirectToAction("ListarPublicaciones");
                        }
                        else
                        {
                            return RedirectToAction("ListarPublicaciones");
                        }
                    }
                    catch (Exception ex)
                    {
                        ClientException.LogException(ex, "Error al editar la publicación");
                        return RedirectToAction("Error", "Shared");
                    }
                }
                else
                {
                    TempData["Error"] = "No se pudo editar la publicación, intentelo nuevamente";
                    return RedirectToAction("EditarPublicacion", new { id = id });
                }
            }
        }

        [HttpPost]
        public ActionResult EliminarImagen(int id, int idPublicacion)
        {
            try
            {
                ps.EliminarImagen(id);
                TempData["Mensaje"] = "Imagen eliminada correctamente";
                return RedirectToAction("EditarPublicacion",new { id = idPublicacion });
            }
            catch (Exception ex)
            {
                ClientException.LogException(ex, "Error al eliminar la imagen");
                return RedirectToAction("Error", "Shared");
            }
        }

        [HttpPost]
        public ActionResult DeshabilitarPublicacion(int id)
        {
            try
            {
                bool estado = bool.Parse(Request.Form.GetValues("ckbDeshabilitar")[0]);
                if (estado == true)
                {
                    ps.CambioEstadoPublicacion(id);
                    TempData["Mensaje"] = "Publicación deshabilitada correctamente";
                    return RedirectToAction("ListarPublicaciones");
                }
                else
                {
                    TempData["Error"] = "No se pudo deshabilitar la publicación, intentelo nuevamente";
                    return RedirectToAction("ListarPublicaciones");
                }
            }
            catch (Exception ex)
            {
                ClientException.LogException(ex, "Error al deshabilitar la publicación");
                return RedirectToAction("Error", "Shared");
            }
           
        }

        [HttpPost]
        public ActionResult HabilitarPublicacion(int id)
        {
            try
            {
                bool estado = bool.Parse(Request.Form.GetValues("ckbHabilitar")[0]);
                if (estado == true)
                {
                    ps.CambioEstadoPublicacion(id);
                    TempData["Mensaje"] = "Publicación habilitada correctamente";
                    return RedirectToAction("ListarPublicaciones");
                }
                else
                {
                    TempData["Error"] = "No se pudo habilitar la publicación, intentelo nuevamente";
                    return RedirectToAction("ListarPublicaciones");
                }
            }
            catch (Exception ex)
            {
                ClientException.LogException(ex, "Error al habilitar la publicación");
                return RedirectToAction("Error", "Shared");
            }
        }

        public ActionResult ContratarPublicacion(Publicacion publicacion)
        {
            return RedirectToAction("Contratar", "Contratacion", publicacion);
        }
    }
}

