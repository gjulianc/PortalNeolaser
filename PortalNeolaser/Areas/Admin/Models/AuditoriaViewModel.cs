using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalNeolaser.Areas.Admin.Models
{
    public class AuditoriaViewModel
    {
        [Required(ErrorMessage = "Debe introducir Fecha de Comienzo")]
        [Display(Name = "Fecha y Hora de Comienzo")]
        public DateTime FechaInicio { get; set; }

        
        [Required(ErrorMessage = "Debe introducir Fecha de Finalización")]
        [Display(Name = "Fecha y Hora de Finalización")]
        public DateTime FechaFin{ get; set; }

        [Required(ErrorMessage = "Debe seleccionar Sucursal")]
        public int Sucursal { get; set; }

        [Required(ErrorMessage = "Debe seleccionar Usuario")]
        public int Usuario { get; set; }
    }
}