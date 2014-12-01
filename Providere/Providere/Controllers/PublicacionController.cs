using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Providere.Models;
using Providere.Servicios;

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
        

        public ActionResult ListarPublicaciones()
        {
            int idUsuario = Convert.ToInt16(this.Session["IdUsuario"]);
            var publicaciones = context.Publicacion.Include("Rubro"); //Eager loading => Carga temprana
            return View(publicaciones.ToList());
        }

        public ActionResult NuevaPublicacion()
        {
               ViewBag.IdRubro = new SelectList(context.Rubro, "Id", "Nombre");
               ViewBag.IdSubRubro = new SelectList(context.SubRubro, "Id", "Nombre");

            return View();
        }

        [HttpPost]
        public ActionResult NuevaPublicacion(int idUsuario, int idRubro, int idSubRubro, string titulo, string descripcion, int precioOpcion, string precio, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ps.CrearNuevaPublicacion(idUsuario,idRubro,idSubRubro,titulo,descripcion,precioOpcion,precio,files);
                
                      return RedirectToAction("ListarPublicaciones");
                }
                catch(Exception ex)
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
      }
 }

