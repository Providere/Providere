using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Providere.Models
{
    [MetadataType(typeof(PublicacionModel))]
    public partial class Publicacion
    {
         public class PublicacionModel
         {

             [Required(ErrorMessage = "Campo Obligatorio")]
             [DisplayFormat(ConvertEmptyStringToNull = false)]
             public object Titulo { get; set; }

             [Required(ErrorMessage = "Campo Obligatorio")]
             [DisplayFormat(ConvertEmptyStringToNull = false)]
             public object Descripcion { get; set; }

             //[DisplayFormat(ConvertEmptyStringToNull = true)]
             //public object Precio { get; set; }

              [Required(ErrorMessage = "Campo Obligatorio")]
             [DisplayFormat(ConvertEmptyStringToNull = false)]
             public object IdRubro { get; set; }

              //[Required(ErrorMessage = "Campo Obligatorio")]
              //[DisplayFormat(ConvertEmptyStringToNull = true)]
              public object IdSubRubro { get; set; }
         }
    }
}