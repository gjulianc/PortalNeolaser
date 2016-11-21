using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalNeolaser.Areas.Mobile.Models
{
    public class ElementoAuditadoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe introducir la Descripción")]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Debe introducir el nuevo estado")]
        public bool? Estado { get; set; }

        //public string Foto { get; set; }

        //[Required(ErrorMessage = "Debe asignarle una auditoria")]
        //public int? FkAuditoria { get; set; }


        //[Required(ErrorMessage = "Debe asignarle un elemento")]
        //public int? FkElemento { get; set; }
    }
}
