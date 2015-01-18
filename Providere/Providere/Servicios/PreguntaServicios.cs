using System;
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
        public void PreguntarEnPublicacion(int idUsuario, int id, string preguntar)
        {
            prr.PreguntarEnPublicacion(idUsuario, id, preguntar);
        }
    }
}