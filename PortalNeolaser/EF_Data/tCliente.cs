//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PortalNeolaser.EF_Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tCliente
    {
        public tCliente()
        {
            this.tSucursales = new HashSet<tSucursale>();
            this.tUsuarios = new HashSet<tUsuario>();
        }
    
        public int Id { get; set; }
        public string Cif { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public string Direccion { get; set; }
        public string Poblacion { get; set; }
        public string Provincia { get; set; }
        public string codPostal { get; set; }
    
        public virtual ICollection<tSucursale> tSucursales { get; set; }
        public virtual ICollection<tUsuario> tUsuarios { get; set; }
    }
}
