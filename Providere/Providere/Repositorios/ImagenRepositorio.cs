using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;
using Providere.Servicios;


namespace Providere.Repositorios
{
    public class ImagenRepositorio
    {
        ProvidereEntities context = new ProvidereEntities();

        internal Imagen traerImagenPrincipal(Publicacion publicacion)
        {
            var imagen = (from im in context.Imagen
                          where im.IdPublicacion == publicacion.Id
                          select im).FirstOrDefault();

            return imagen;

        }
    }
}