using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Providere.Models
{
    [MetadataType(typeof(UsuarioModel))]
    public partial class Usuario
    {
        public class UsuarioModel
        {
            [Required(ErrorMessage = "Campo Obligatorio")]
            [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object Nombre { get; set; }

            [Required(ErrorMessage = "Campo Obligatorio")]
            [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object Apellido { get; set; }

            [Required(ErrorMessage = "Campo Obligatorio")]
            [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
            [DataType(DataType.EmailAddress)]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object Mail { get; set; }

            [Required(ErrorMessage = "Campo Obligatorio")]
            [StringLength(10, ErrorMessage = "Minimo 5 y maximo 10 caracteres")]
            [DataType(DataType.Password)]
            [RegularExpression("[A-Za-z0-9]{5,10}", ErrorMessage = "La contraseña debe ser alfanumerica. Minimo 5 y maximo 10 caracteres")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object Contrasenia { get; set; }

            //[Required(ErrorMessage = "Campo Obligatorio")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object Ubicacion { get; set; }

            [Required(ErrorMessage = "Campo Obligatorio")]
            //[RegularExpression("[0-9]*", ErrorMessage = "El telefono debe ser solo numerico")]
            [DataType(DataType.PhoneNumber)]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object Telefono { get; set; }

          
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object FechaCreacion { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object FechaActivacion { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object CodActivacion { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object FechaCambioEstado { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object IdEstado { get; set; }


        }
    }
}