using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;
using Providere.Servicios;

namespace Providere.Repositorios
{
    public class PreguntaRepositorio
    {
        ProvidereEntities context = new ProvidereEntities();

        public void PreguntarEnPublicacion(int idUsuario, int id, string preguntar)
        {
            PreguntaRespuesta miPregunta = new PreguntaRespuesta();
            miPregunta.IdPublicacion = Convert.ToInt16(id);
            miPregunta.IdUsuario = Convert.ToInt16(idUsuario);
            miPregunta.Pregunta = preguntar;
            context.PreguntaRespuesta.AddObject(miPregunta);
            context.SaveChanges();
        }
    }
}