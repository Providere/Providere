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
        PublicacionServicios ps = new PublicacionServicios();
        MailServicios mailing = new MailServicios();

        public ActionResult Index()
        {
            ViewBag.Error = TempData["Error"];
            ViewBag.Error = TempData["Exito"];

            int idUsuario = Convert.ToInt16(this.Session["IdUsuario"]);

            var preguntasSinResponder = prs.TraerPreguntasSinResponder(idUsuario);
            ViewBag.PreguntasSinResponder = preguntasSinResponder;

            var preguntasQueHice = prs.TraerPreguntasQueHice(idUsuario);
            ViewBag.PreguntasQueHice = preguntasQueHice;

            return View();
        }

        [HttpPost]
        public ActionResult Preguntar(int idUsuario, int id, string preguntar)
        {
            int idUser = Convert.ToInt16(this.Session["IdUsuario"]);
            if (idUsuario == idUser)
            {
                TempData["Error"] = "No podes preguntar en tu publicación.";
                return RedirectToAction("VisualizarPublicacion", "Publicacion", new { idPublicacion = id });
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(preguntar))
                {
                    try
                    {
                        prs.PreguntarEnPublicacion(idUser, id, preguntar);

                        TempData["Exito"] = "Pregunta publicada correctamente.";
                        return RedirectToAction("VisualizarPublicacion", "Publicacion", new { idPublicacion = id });
                    }
                    catch (Exception ex)
                    {
                        ClientException.LogException(ex, "Error al guardar la pregunta.");
                        return RedirectToAction("Error", "Shared");
                    }
                }

                TempData["Error"] = "La pregunta no puede ser vacía.";
                return RedirectToAction("VisualizarPublicacion", "Publicacion", new { idPublicacion = id });
            }
        }

        [HttpPost]
        public ActionResult Responder(int id, string responder)
        {
            if (!string.IsNullOrWhiteSpace(responder))
            {
                prs.Responder(id, responder);
                TempData["Exito"] = "Respuesta publicada con éxito.";
                return RedirectToAction("Home", "Home");
            }
            else
            {
                TempData["Error"] = "La respuesta no puede ser vacía.";
                return RedirectToAction("Index", "Pregunta");
            }
        }

        [HttpPost]
        public ActionResult EliminarPregunta(int id)
        {
            try
            {
                prs.CambiarDeEstado(id);//No se elimina cambia de estado
                TempData["Exito"] = "Pregunta eliminada correctamente."; 
                return RedirectToAction("Index", "Pregunta");
            }
            catch(Exception ex)
            {
                ClientException.LogException(ex, "Error al eliminar la pregunta.");
                return RedirectToAction("Error", "Shared");
            }
        }
    }
}
