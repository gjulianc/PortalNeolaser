using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalNeolaser.Models
{
    public class FormElementoAuditado
    {
        public FormElementoAuditado(ElementosAuditado e)
        {
            this.Id = e.Id;
            this.Estado = e.Estado;
            this.Descripcion = e.Descripcion;
            this.FkAuditoria = e.FkAuditoria;
            this.FkElemento = e.FkElemento;
            this.Foto = null;
        }
        
        public FormElementoAuditado()
        { }

        public HttpPostedFileBase Foto { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "Debe introducir la Descripción")]
        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Debe introducir el nuevo estado")]
        public bool? Estado { get; set; }



        [Required(ErrorMessage = "Debe asignarle una auditoria")]
        public int? FkAuditoria { get; set; }


        [Required(ErrorMessage = "Debe asignarle un elemento")]
        public int? FkElemento { get; set; }
    }
}