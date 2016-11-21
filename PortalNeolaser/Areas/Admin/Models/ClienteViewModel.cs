using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalNeolaser.Areas.Admin.Models
{
    public class ClienteViewModel
    {
        [Required(ErrorMessage = "Debe introducir el CIF")]
        [Display(Name = "CIF")]
        public string Cif { get; set; }

        [Required(ErrorMessage = "Debe introducir el Nombre del Cliente")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        
        [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", ErrorMessage = "Por favor, introduzca una direción de email válida.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "EMail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe introducir el Teléfono")]
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Debe introducir Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Debe introducir Población")]
        public string Poblacion { get; set; }

        [Required(ErrorMessage = "Debe introducir Provincia")]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "Debe introducir Código Postal")]
        [Display(Name = "Codigo Postal")]
        public string CodPostal { get; set; }
    }
}
