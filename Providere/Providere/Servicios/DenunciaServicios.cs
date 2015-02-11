using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Repositorios;
using Providere.Models;

namespace Providere.Servicios
{
    public class DenunciaServicios
    {
        DenunciaRepositorio dr = new DenunciaRepositorio();

        /*public List<Denuncia> traerDenunciasDelUltimoMes()
        {
            return dr.traerDenunciasDelUltimoMes();
        }*/

        public void DenunciaComentario(int id)
        {
            dr.DenunciarComentario(id);
        }

        public bool VerificarComentarioDenunciado(int id)
        {
            try
            {
                Denuncia denuncia = dr.VerificarComentarioDenunciado(id);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}