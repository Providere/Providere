﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Repositorios;
using Providere.Models;

namespace Providere.Servicios
{
    public class PreguntaServicios
    {
        PreguntaRepositorio prr = new PreguntaRepositorio();
        public void PreguntarEnPublicacion(int idUser, int id, string preguntar)
        {
            prr.PreguntarEnPublicacion(idUser, id, preguntar);
        }

        public List<PreguntaRespuesta> TraerPreguntasSinResponder(int idUsuario)
        {
            return prr.TraerPreguntasSinRespoder(idUsuario);
        }

        public object TraerPreguntasQueHice(int idUsuario)
        {
            return prr.TraerPreguntasQueHice(idUsuario);
        }

        public void TraerPreguntasSinResponder(int id, string responder)
        {
           prr.Responder(id, responder);
        }
    }
}