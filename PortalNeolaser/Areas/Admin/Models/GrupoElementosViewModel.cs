using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalNeolaser.Areas.Admin.Models
{
    public class GrupoElementosViewModel
    {
        [Required(ErrorMessage = "Debe introducir Nombre del Grupo")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

       
        [Display(Name = "Descripción del Grupo")]
        public string Descripcion { get; set; }

       
    }
}
