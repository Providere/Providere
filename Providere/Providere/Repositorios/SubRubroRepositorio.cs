﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Providere.Models;

namespace Providere.Repositorios
{
    
    public class SubRubroRepositorio
    {
        ProvidereEntities context = new ProvidereEntities();
       
        internal List<SubRubro> obtenerPorRubro(Rubro rubro)
        {
 	         var subRubros = (from im in context.SubRubro
                                  where im.IdRubro == rubro.Id
                                  select im).ToList();
             return subRubros;
        }

    }
}