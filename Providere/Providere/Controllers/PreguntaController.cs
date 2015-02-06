﻿using System;
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
                TempData["Error"] = "No podes preguntar en tu publicación";
                return RedirectToAction("Home", "Home");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(preguntar))
                {
                    prs.PreguntarEnPublicacion(idUser, id, preguntar);
                    try
                    {
                        //Traer todo de la publicacion, para tener el mail del prestador:
                        Publicacion publicacion = ps.TraerPublicacionPorId(id);
                        mailing.EnviarMailPregunta(publicacion);
                    }
                    catch (System.Net.Mail.SmtpException ex)
                    {
                        ClientException.LogException(ex, "Error al enviar el mail"); //No puede enviar el mail pero igual publica la pregunta
                        TempData["Exito"] = "Su pregunta fue publicada correctamente";
                        return RedirectToAction("Home", "Home");
                    }

                    TempData["Exito"] = "Pregunta publicada correctamente";
                    return RedirectToAction("Home", "Home");
                }
                TempData["Error"] = "La pregunta no puede ser vacia";
                return RedirectToAction("VisualizarPublicacion", "Publicacion", new { idPublicacion = id });
            }
        }

        [HttpPost]
        public ActionResult Responder(int id, string responder)
        {
            if (!string.IsNullOrWhiteSpace(responder))
            {
                prs.TraerPreguntasSinResponder(id, responder);
                TempData["Exito"] = "Respuesta cargada con exito";
                return RedirectToAction("Home", "Home");
            }
            else
            {
                TempData["Error"] = "La respuesta no puede ser vacía";
                return RedirectToAction("Home", "Home");
            }
        }

        [HttpPost]
        public ActionResult EliminarPregunta(int id)
        {
            return RedirectToAction("Index","Index");
        }
    }
}
