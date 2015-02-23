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
            [Required(ErrorMessage = "Campo Obligatorio.")]
            [StringLength(25, ErrorMessage = "Máximo 25 caracteres.")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object Nombre { get; set; }

            [Required(ErrorMessage = "Campo Obligatorio.")]
            [StringLength(25, ErrorMessage = "Máximo 25 caracteres.")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object Apellido { get; set; }

            [Required(ErrorMessage = "Campo Obligatorio.")]
            [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
            [DataType(DataType.EmailAddress)]
            [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Formato de correo electrónico inválido.")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object Mail { get; set; }

            [Required(ErrorMessage = "Campo Obligatorio.")]
            [DataType(DataType.Password)]
            [RegularExpression(@"^(?=(.*\d){1})(?=(.*[A-Z]){1}).{5,10}$", ErrorMessage = "Formato de contraseña inválido.")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object Contrasenia { get; set; }

           
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object Ubicacion { get; set; }

            [Required(ErrorMessage = "Campo Obligatorio.")]
            [StringLength(20, ErrorMessage = "Máximo 20 caracteres.")]
            [RegularExpression("[0-9]*", ErrorMessage = "El teléfono debe ser solo numérico.")]
            [DataType(DataType.PhoneNumber)]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object Telefono { get; set; }

            
            [DisplayName("DNI")]
            [Required(ErrorMessage = "Campo Obligatorio.")]
            [StringLength(8, MinimumLength=8, ErrorMessage = "Formato inválido.")]
            [RegularExpression("[0-9]*", ErrorMessage = "El DNI debe ser solo numérico.")]
            [DataType(DataType.PhoneNumber)]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object Dni { get; set; }
          
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public object FechaCreacion { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false, DataFormatString = "{0:dd/MM/yyyy}")]
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