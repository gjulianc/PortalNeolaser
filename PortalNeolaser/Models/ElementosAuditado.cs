//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PortalNeolaser.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ElementosAuditado
    {
        public int Id { get; set; }
        public Nullable<bool> Estado { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }
        public Nullable<int> FkAuditoria { get; set; }
        public Nullable<int> FkElemento { get; set; }
        public string Observaciones { get; set; }
    
        public virtual Auditoria Auditoria { get; set; }
        public virtual Elemento Elemento { get; set; }
    }
}
