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

        public void PreguntarEnPublicacion(int idUser, int id, string preguntar)
        {
            PreguntaRespuesta miPregunta = new PreguntaRespuesta();
            miPregunta.IdPublicacion = Convert.ToInt16(id);
            miPregunta.IdUsuario = Convert.ToInt16(idUser);
            miPregunta.Pregunta = preguntar;
            miPregunta.Estado = 1; //No oculta
            context.PreguntaRespuesta.AddObject(miPregunta);
            context.SaveChanges();
        }

        public List<PreguntaRespuesta> TraerPreguntasSinRespoder(int idUsuario)
        {
            var misPublicaciones = (from publicacion in context.Publicacion
                                    where publicacion.IdUsuario == idUsuario
                                    select new { publicacion.Id });
            List<int> listaDePublicaciones = new List<int>();
            foreach (var item in misPublicaciones)
            {
                listaDePublicaciones.Add(item.Id);
            }
            var resultados = (from preguntas in context.PreguntaRespuesta
                              where (listaDePublicaciones.Contains(preguntas.IdPublicacion) && preguntas.Respuesta == null)
                              select preguntas).ToList();
            return resultados;

        }

        public object TraerPreguntasQueHice(int idUsuario)
        {
            var resultado = (from preg in context.PreguntaRespuesta
                             where preg.IdUsuario == idUsuario && preg.Estado == 1
                             orderby preg.FechaRespuesta descending
                             select preg ).ToList();
            return resultado;
        }

        public void Responder(int id, string responder)
        {
            PreguntaRespuesta miRespuesta =  context.PreguntaRespuesta.Where(e=>e.Id == id).FirstOrDefault();
            miRespuesta.Respuesta = responder;
            miRespuesta.FechaRespuesta = DateTime.Now;
            context.SaveChanges();
        }

        public void CambiarDeEstado(int id)
        {
            PreguntaRespuesta pregunta = context.PreguntaRespuesta.Where(e => e.Id == id).FirstOrDefault();
            if (pregunta.Estado == 1) //No oculta
            {
                pregunta.Estado = 0; //Pasa a estado oculto
            }
            context.SaveChanges();
        }
    }
}