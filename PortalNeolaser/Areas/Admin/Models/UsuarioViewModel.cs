using PortalNeolaser.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalNeolaser.Areas.Admin.Models
{
    
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "Debe introducir Usuario")]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Debe introducir EMail")]
        [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", ErrorMessage = "Por favor, introduzca una direción de email válida.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "EMail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe introducir una Contraseña")]
        [StringLength(100, ErrorMessage = "La {0} debe contener al menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmación Contraseña")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "El password y su confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Debe introducir el Cliente")]
        public int  FkCliente { get; set; }
        [Required(ErrorMessage = "Debe asignarle un Rol al Usuario")]
        public string Rol { get; set; }

             
    }
}
