using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Providere.Models
{
     [MetadataType(typeof(CalificacionModel))]
    public partial class Calificacion
    {
         public class CalificacionModel
         {
             [Required(ErrorMessage = "Campo Obligatorio")]
             [DisplayFormat(ConvertEmptyStringToNull = false)]
             public object IdTipoEvaluacion { get; set; }

             [Required(ErrorMessage = "Campo Obligatorio")]
             [DisplayFormat(ConvertEmptyStringToNull = false)]
             public object Descripcion { get; set; }
         }
    }
}