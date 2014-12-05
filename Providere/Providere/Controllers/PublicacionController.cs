﻿using System;
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
        //
        // GET: /Publicacion/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        PublicacionServicios ps = new PublicacionServicios();
        ProvidereEntities context = new ProvidereEntities();
        RubroServicios rs = new RubroServicios();


        public ActionResult ListarPublicaciones()
        {
            int idUsuario = Convert.ToInt16(this.Session["IdUsuario"]);
            var publicaciones = ps.ListarMisPublicaciones(idUsuario);

            ViewBag.Rubros = rs.obtenerTodos();
            return View(publicaciones);
        }

        public ActionResult NuevaPublicacion()
        {
            ViewBag.IdRubro = new SelectList(context.Rubro, "Id", "Nombre");
            ViewBag.IdSubRubro = new SelectList(context.SubRubro, "Id", "Nombre");

            ViewBag.Rubros = rs.obtenerTodos();
            return View();
        }

        [HttpPost]
        public ActionResult NuevaPublicacion(int idUsuario, int idRubro, int? idSubRubro, string titulo, string descripcion, int precioOpcion, string precio, IEnumerable<HttpPostedFileBase> files)
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
                return RedirectToAction("Home", "Home");
            }

        }

        public ActionResult VisualizarMiPublicacion(int Id)
        {
            int idUsuario = Convert.ToInt16(this.Session["IdUsuario"]);
            Publicacion miPublicacion = ps.TraerPublicacion(Id, idUsuario);

            ViewBag.Rubros = rs.obtenerTodos();

            return View(miPublicacion);
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

        public ActionResult EditarPublicacion()
        {
            ViewBag.Rubros = rs.obtenerTodos();
            return View();
        }

        //[HttpPost]
        //public ActionResult EditarPublicacion()
        //{
        //    return View();
        //}


        public ActionResult DeshabilitarPublicacion()
        {
            return View();
        }


        //[HttpPost]
        // public ActionResult DeshabilitarPublicacion()
        // {
        //     return View();
        // }
    }
}

