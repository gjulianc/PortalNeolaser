using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalNeolaser.Models
{
    public class GroupItem
    {
        [Key]
        public int GroupItem_Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<SucursalGroupItem> SucursalGroupItems { get; set; }

    } 
}