using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalNeolaser.Areas.Admin.Models
{
    public class SucursalViewModel
    {
        [Required(ErrorMessage = "Debe introducir el codigo SAP de la sucursal")]
        [Display(Name = "Codigo SAP")]
        public string CodigoSAP { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Fecha Alta")]
        public DateTime FechaAlta { get; set; }

        [Required(ErrorMessage = "Debe introducir Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Debe introducir Comunidad Autonoma")]
        public string ComunidadAutonoma { get; set; }

        [Required(ErrorMessage = "Debe introducir Población")]
        public string Poblacion { get; set; }

        [Required(ErrorMessage = "Debe introducir Provincia")]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "Debe introducir Código Postal")]
        [Display(Name = "Codigo Postal")]
        public string CodPostal { get; set; }

        [Required(ErrorMessage = "Debe introducir el Cliente")]
        public int Cliente { get; set; }
       
    }
}