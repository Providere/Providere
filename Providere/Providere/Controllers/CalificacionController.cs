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
        DenunciaServicios ds = new DenunciaServicios();
        ReplicaServicios rs = new ReplicaServicios();

         
        public ActionResult Index()
        {
            int idUsuario = Convert.ToInt16(this.Session["IdUsuario"]);
            int limite = 4;

            var obtenidasLimite = cs.TraerCalificacionesObtenidas(idUsuario,limite); //Soy calificado
            var otorgadasLimite = cs.TraerCalificacionesOtorgadas(idUsuario,limite); //Soy calificador
            ViewBag.ObtenidasLimite = obtenidasLimite; 
            ViewBag.OtorgadasLimite = otorgadasLimite;

            var obtenidasTodas = cs.TraerCalificacionesObtenidasTodas(idUsuario); 
            var otorgadasTodas = cs.TraerCalificacionesOtorgadasTodas(idUsuario); 
            ViewBag.ObtenidasTodas = obtenidasTodas;
            ViewBag.OtorgadasTodas = otorgadasTodas;

            var replicasTodas = rs.TraerTodasLasReplicas();
            ViewBag.Replicas = replicasTodas;

            ViewBag.Texto = "Comentario denunciado por ser ofensivo hacia terceros. Si sigue infringuiendo las normas de buena conducta, puede ser sanciado";
            ViewBag.Exito = TempData["Exito"];
            ViewBag.Error = TempData["Error"];

            return View();
        }

        public ActionResult CalificarUsuario(int idContratacion, int idTipoCalificacion)
        {
            if (idTipoCalificacion == 1)
                ViewBag.Texto = "por el servicio brindado";
            else
                ViewBag.Texto = "por la contratación de";

            var traigoContratacion = crs.traerPorId(idContratacion);

            ViewBag.Contratacion = traigoContratacion;

           // ViewBag.IdContratacion = idContratacion;
            ViewBag.IdTipoCalificacion = idTipoCalificacion;

            return View();
        }

        [HttpPost]
        public ActionResult CalificarUsuario()
        {

            int idContratacion = Int32.Parse(Request["idContratacion"]);

            int idTipoCalificacion = Int32.Parse(Request["idTipoCalificacion"]);

            int idTipoEvaluacion = Int32.Parse(Request["IdTipoEvaluacion"]);
                
            string comentario = Convert.ToString(Request["Descripcion"]);

            if (ModelState.IsValid)
            {
                cs.calificarUsuario(idContratacion, idTipoEvaluacion, idTipoCalificacion, comentario);

                TempData["Exito"] = "El usuario se ha calificado con éxito";

                return RedirectToAction("Index", "Contratacion");
            }
            else
            {
                TempData["Error"] = "No se pudo guardar la calificación, intentalo nuevamente";
                return RedirectToAction("Index", "Contratacion");
            }
        }

        [HttpPost]
        public ActionResult DenunciarComentario(int id)
        {
            try
            {
                ds.DenunciaComentario(id);
                TempData["Exito"] = "Comentario denunciado con éxito";
                return RedirectToAction("Index", "Calificacion");
            }
            catch (Exception ex)
            {
                ClientException.LogException(ex, "Error al denunciar comentario");
                return RedirectToAction("Error", "Shared");
            }
        }

        [HttpPost]
        public ActionResult ReplicarComentario(int id, string replicar)
        {
            if (!string.IsNullOrWhiteSpace(replicar))
            {
                try
                {
                    rs.ReplicarComentario(id, replicar);
                    TempData["Exito"] = "Comentario replicado con éxito";
                    return RedirectToAction("Index", "Calificacion");
                }
                catch (Exception ex)
                {
                    ClientException.LogException(ex, "Error al replicar comentario");
                    return RedirectToAction("Error", "Shared");
                }
            }
            else
            {
                TempData["Error"] = "La réplica no puede ser vacía";
                return RedirectToAction("Index", "Calificacion");
            }
        }

    }
}
