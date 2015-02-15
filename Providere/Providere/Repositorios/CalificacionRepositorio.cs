using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;
using Providere.Servicios;

namespace Providere.Repositorios
{
    public class CalificacionRepositorio
    {
        ProvidereEntities context = new ProvidereEntities();


        internal void calificarUsuario(string comentario, Contratacion contratacion, TipoEvaluacion tipoEvaluacion, TipoCalificacion tipoCalificacion, int idTipoCalificacion)
        {
            Calificacion calificacion = new Calificacion();

            if (idTipoCalificacion == 1)
            {
                calificacion.IdCalificador = contratacion.IdUsuario;
                calificacion.IdCalificado = contratacion.Publicacion.IdUsuario;
            }
            else
            {
                calificacion.IdCalificador = contratacion.Publicacion.IdUsuario;
                calificacion.IdCalificado = contratacion.IdUsuario;
            }

            calificacion.Descripcion = comentario;
            calificacion.IdContratacion = contratacion.Id;
            calificacion.IdTipoCalificacion = tipoCalificacion.Id;
            calificacion.IdTipoEvaluacion = tipoEvaluacion.Id;
            calificacion.FechaCalificacion = DateTime.Now;
            calificacion.FlagDenunciado = 0; //no fue denunciada esa calificacion todavia
            calificacion.FlagReplicado = 0; //no fue replicado esa calificacion todavia

            context.Calificacion.AddObject(calificacion);

            if (idTipoCalificacion == 1)
            {
                Contratacion cambioEstado = context.Contratacion.Where(e => e.Id == contratacion.Id).FirstOrDefault();
                cambioEstado.FlagCalificoCliente = 1; //en este caso se marca que el cliente califico al prestador

                Puntaje puntaje = context.Puntaje.Where(e => e.IdPublicacion == contratacion.IdPublicacion).FirstOrDefault();
                // Cuando califica el cliente hace el cambio en el puntaje de la publicacion

                if (puntaje == null) // Este bloque de código es para llenar la tabla Puntaje perteneciente al prestador por ser dueño de la publicación
                {
                    Puntaje puntajeNuevo = new Puntaje();

                    puntajeNuevo.IdPublicacion = contratacion.IdPublicacion;

                    if (tipoEvaluacion.Id == 1)
                    {
                        puntajeNuevo.Positivo = 1;
                    }
                    else
                    {
                        if (tipoEvaluacion.Id == 2)
                        {
                            puntajeNuevo.Neutro = 1;
                        }
                        else
                        {
                            puntajeNuevo.Negativo = 1;
                        }
                    }

                    var total = puntajeNuevo.Positivo + puntajeNuevo.Neutro + puntajeNuevo.Negativo;

                    puntajeNuevo.Total = Convert.ToInt16(total);

                    context.Puntaje.AddObject(puntajeNuevo);

                }
                else
                {
                    if (tipoEvaluacion.Id == 1)
                    {
                        puntaje.Positivo = Convert.ToInt16(puntaje.Positivo + 1);
                        puntaje.Total = Convert.ToInt16(puntaje.Total + 1);
                    }
                    else
                    {
                        if (tipoEvaluacion.Id == 2)
                        {
                            puntaje.Neutro = Convert.ToInt16(puntaje.Neutro + 1);
                            // Puntaje Total no tiene cambios.
                        }
                        else
                        {
                            puntaje.Negativo = Convert.ToInt16(puntaje.Negativo + 1);
                            puntaje.Total = Convert.ToInt16(puntaje.Total - 1);
                        }
                    } 
                }

            }
            else
            {
                Contratacion cambioEstado = context.Contratacion.Where(e => e.Id == contratacion.Id).FirstOrDefault();
                cambioEstado.FlagCalificoProveedor = 1; //en este caso se marca que el prestador califico al cliente

                PuntajeCliente puntajeCliente = context.PuntajeCliente.Where(e => e.IdUsuario == contratacion.IdUsuario).FirstOrDefault();
                // Cuando califica el prestador hace el cambio en el puntaje del cliente

                if (puntajeCliente == null) // Este bloque de código es para llenar la tabla Puntaje Cliente
                {
                    PuntajeCliente puntajeClienteNuevo = new PuntajeCliente();

                    puntajeClienteNuevo.IdUsuario = contratacion.IdUsuario;

                    if (tipoEvaluacion.Id == 1)
                    {
                        puntajeClienteNuevo.Positivo = 1;
                    }
                    else
                    {
                        if (tipoEvaluacion.Id == 2)
                        {
                            puntajeClienteNuevo.Neutro = 1;
                        }
                        else
                        {
                            puntajeClienteNuevo.Negativo = 1;
                        }
                    }

                    var total = puntajeClienteNuevo.Positivo + puntajeClienteNuevo.Neutro + puntajeClienteNuevo.Negativo;

                    puntajeClienteNuevo.Total = Convert.ToInt16(total);

                    context.PuntajeCliente.AddObject(puntajeClienteNuevo);

                }
                else
                {
                    if (tipoEvaluacion.Id == 1)
                    {
                        puntajeCliente.Positivo = Convert.ToInt16(puntajeCliente.Positivo + 1);
                        puntajeCliente.Total = Convert.ToInt16(puntajeCliente.Total + 1);
                    }
                    else
                    {
                        if (tipoEvaluacion.Id == 2)
                        {
                            puntajeCliente.Neutro = Convert.ToInt16(puntajeCliente.Neutro + 1);
                            // Puntaje Total no tiene cambios.
                        }
                        else
                        {
                            puntajeCliente.Negativo = Convert.ToInt16(puntajeCliente.Negativo + 1);
                            puntajeCliente.Total = Convert.ToInt16(puntajeCliente.Total - 1);
                        }
                    }
                }

            }

            context.SaveChanges();
        }

        internal List<Calificacion> obtenerNegativasDeUsuario(Usuario usuario)
        {
            return context.Calificacion.Where(x => x.Usuario.Id == usuario.Id).Where(x => x.Denuncia.Count > 1).ToList();
        }

        public List<Calificacion> TraerCalificaciones(int idPublicacion)
        {
            var contratacion = (from contrata in context.Contratacion
                                where contrata.IdPublicacion == idPublicacion
                                select new { contrata.Id });
            List<int> listaDeContrataciones = new List<int>();
            foreach (var item in contratacion)
            {
                listaDeContrataciones.Add(item.Id);
            }
            var resultado = (from calificacion in context.Calificacion
                             where (listaDeContrataciones.Contains(calificacion.IdContratacion) && calificacion.IdTipoCalificacion == 1 && calificacion.IdTipoEvaluacion == 1)
                             orderby calificacion.FechaCalificacion descending
                             select calificacion).ToList();
            return resultado;
        }

        //Trae las primeras 5 calificaciones para mostrar en la publicacion
        public object TraerPrimerasCalificaciones(int limite, int idPublicacion)
        {
            var contratacion = (from contrata in context.Contratacion
                                where contrata.IdPublicacion == idPublicacion
                                select new { contrata.Id });
            List<int> listaDeContrataciones = new List<int>();
            foreach (var item in contratacion)
            {
                listaDeContrataciones.Add(item.Id);
            }
            var resultado = (from calificacion in context.Calificacion
                             where (listaDeContrataciones.Contains(calificacion.IdContratacion) && calificacion.IdTipoCalificacion == 1 && calificacion.IdTipoEvaluacion == 1) //para el prestador
                             orderby calificacion.FechaCalificacion descending
                             select calificacion).Take(limite).ToList();
            return resultado;
        }


        public object TraerCalificacionesObtenidas(int idUsuario, int limite)
        {
            var resultados = (from califica in context.Calificacion
                              where califica.IdCalificado == idUsuario
                              select califica).Take(limite).ToList();
            return resultados;
        }

        public object TraerCalificacionesOtorgadas(int idUsuario, int limite)
        {
            var resultados = (from califica in context.Calificacion
                              where califica.IdCalificador == idUsuario
                              select califica).Take(limite).ToList();
            return resultados;
        }

        public object TraerCalificacionObtenidasTodas(int idUsuario)
        {
            var resultados = (from califica in context.Calificacion
                              where califica.IdCalificado == idUsuario
                              select califica).ToList();
            return resultados;
        }

        public object TraerCalificacionesOtorgadasTodas(int idUsuario)
        {
            var resultados = (from califica in context.Calificacion
                              where califica.IdCalificador == idUsuario
                              select califica).ToList();
            return resultados;
        }
    }
}