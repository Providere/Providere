using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Providere.Models;
using Providere.Servicios;

namespace Providere.Controllers
{
    public class PreguntaController : Controller
    {
        PreguntaServicios prs = new PreguntaServicios();

        public ActionResult Index()
        {
            return View();
        }

       
        public ActionResult Preguntar(int idUsuario, int id, string preguntar)
        {
            int idUser = Convert.ToInt16(this.Session["IdUsuario"]);
            if (idUsuario == idUser)
            {
                TempData["Error"] = "No podes preguntar en tu publicación";
                return RedirectToAction("Home", "Home");
            }
            else
            {
            if (!string.IsNullOrWhiteSpace(preguntar))
            {
                prs.PreguntarEnPublicacion(idUsuario,id, preguntar);
                TempData["Exito"] = "Pregunta publicada correctamente";
                return RedirectToAction("Home", "Home"); 
            }
            TempData["Error"] = "La pregunta no puede ser vacia";
            return RedirectToAction("VisualizarPublicacion", "Publicacion", new { idPublicacion = id });
            }
        }
    }
}
