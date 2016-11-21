using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalNeolaser.Models
{
    public class Item
    {
        [Key]
        public int Item_Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual GroupItem GroupItem { get; set; }
        public ICollection<ItemAuditado> ItemsAuditados { get; set; }
    }
}