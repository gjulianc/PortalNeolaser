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
    
    public partial class Auditoria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Auditoria()
        {
            this.ElementosAuditados = new HashSet<ElementosAuditado>();
        }
    
        public int Id { get; set; }
        public Nullable<System.DateTime> FechaInicio { get; set; }
        public Nullable<System.DateTime> FechaFin { get; set; }
        public Nullable<int> FkUsuario { get; set; }
        public Nullable<int> FkSucursal { get; set; }
        public Nullable<bool> Estado { get; set; }
    
        public virtual Sucursal Sucursal { get; set; }
        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ElementosAuditado> ElementosAuditados { get; set; }
    }
}
