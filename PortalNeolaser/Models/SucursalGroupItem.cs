using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalNeolaser.Models
{
    public class SucursalGroupItem
    {
        [Key]
        public int SucursalGroupItem_Id { get; set; }
        
        public virtual Sucursal Sucursal { get; set; }
        public virtual GroupItem GroupItem { get; set; }
    }
}