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
    
    public partial class Elemento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Elemento()
        {
            this.ElementosAuditados = new HashSet<ElementosAuditado>();
        }
    
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Nullable<int> FkGrupo { get; set; }
        public Nullable<int> Orden { get; set; }
    
        public virtual GruposElemento GruposElemento { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ElementosAuditado> ElementosAuditados { get; set; }
    }
}
