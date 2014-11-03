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
            [StringLength(20, ErrorMessage = "Maximo 20 caracteres")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object Nombre { get; set; }

            [Required(ErrorMessage = "Campo Obligatorio")]
            [StringLength(20, ErrorMessage = "Maximo 20 caracteres")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object Apellido { get; set; }

            [Required(ErrorMessage = "Campo Obligatorio")]
            [StringLength(25, ErrorMessage = "Maximo 25 caracteres")]
            [DataType(DataType.EmailAddress)]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object Mail { get; set; }

            [Required(ErrorMessage = "Campo Obligatorio")]
            [StringLength(15, ErrorMessage = "Maximo 15 caracteres")]
            [RegularExpression("[0-9]", ErrorMessage = "El telefono debe ser solo numerico")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object Telefono { get; set; }

            [DisplayName("Contraseña")]
            [Required(ErrorMessage = "Campo Obligatorio")]
            //[StringLength(12, ErrorMessage = "Minimo 6 y maximo 12 caracteres")]
            [DataType(DataType.Password)]
            //[Range(6,12)]
            [RegularExpression("[A-Za-z0-9]{6,12}", ErrorMessage = "La contraseña debe ser alfanumerica. Minimo 6 y maximo 12 caracteres")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object Contrasenia { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object FechaCreacion { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object FechaActivacion { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object CodActivacion { get; set; }

        }
    }
}