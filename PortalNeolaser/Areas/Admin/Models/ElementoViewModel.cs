using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalNeolaser.Areas.Admin.Models
{
    public class ElementoViewModel
    {
        [Required(ErrorMessage = "Debe introducir Nombre del Elemento")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }


        [Display(Name = "Descripción del Elemento")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Debe introducir el Grupo al que pertence")]
        public int Grupo { get; set; }

    }
}