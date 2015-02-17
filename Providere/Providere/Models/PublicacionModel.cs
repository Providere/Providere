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

             [DisplayName("Título")]
             [Required(ErrorMessage = "Campo Obligatorio")]
             [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
             [RegularExpression("^[A-Z a-z]*$",ErrorMessage = "Ingresa solo letras.")]
             [DisplayFormat(ConvertEmptyStringToNull = false)]
             public object Titulo { get; set; }

             [DisplayName("Descripción")]
             [Required(ErrorMessage = "Campo Obligatorio")]
             [DisplayFormat(ConvertEmptyStringToNull = false)]
             public object Descripcion { get; set; }

             [Required(ErrorMessage = "Campo Obligatorio")]
             public object Precio { get; set; }

            
             [DisplayFormat(ConvertEmptyStringToNull = false)]
             public object IdRubro { get; set; }

              
             //[DisplayFormat(ConvertEmptyStringToNull = true)]
             public object IdSubRubro { get; set; }

             [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
             public DateTime FechaCreacion { get; set; }

             [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
             public DateTime FechaEdicion { get; set; }

         }
    }
}