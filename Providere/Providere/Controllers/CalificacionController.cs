using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Providere.Servicios;
using Providere.Models;

namespace Providere.Controllers
{
    public class CalificacionController : Controller
    {

        CalificacionServicios cs = new CalificacionServicios();
        ContratacionServicios crs = new ContratacionServicios();

        public ActionResult CalificarUsuario(int idContratacion, int idTipoCalificacion)
        {
            if (idTipoCalificacion == 1)
                ViewBag.Texto = "por el servicio brindado";
            else
                ViewBag.Texto = "por la contratación de";

            var traigoContratacion = crs.traerPorId(idContratacion);

            ViewBag.Contratacion = traigoContratacion;

            ViewBag.IdTipoCalificacion = idTipoCalificacion;

            return View();
        }

        [HttpPost]
        public ActionResult CalificarUsuario()
        {

            int idContratacion = Int32.Parse(Request["idContratacion"]);

            int idTipoCalificacion = Int32.Parse(Request["idTipoCalificacion"]);

            int idTipoEvaluacion = Int32.Parse(Request["calificacion"]);
                
            string comentario = Convert.ToString(Request["Descripcion"]);

            if (!string.IsNullOrWhiteSpace(comentario))
            {
                cs.calificarUsuario(idContratacion, idTipoEvaluacion, idTipoCalificacion, comentario);

                TempData["Exito"] = "El usuario se ha calificado con éxito";

                return RedirectToAction("Index", "Contratacion");
            }
            else
            {
                TempData["Error"] = "El comentario no puede ser vacío";
                return RedirectToAction("Index", "Contratacion");
            }
        }
    }
}
