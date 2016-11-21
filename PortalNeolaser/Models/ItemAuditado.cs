using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalNeolaser.Models
{
    public class ItemAuditado
    {
        [Key]
        public int ItemAuditado_Id { get; set; }
        public bool Estado { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }

        public virtual Auditoria Auditoria { get; set; }
        public virtual Item Item { get; set; } 
    }
}