using PortalNeolaser.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEO.DataModel.Data
{
    public class ItemMap:EntityTypeConfiguration<Item>
    {
        protected EFDbContext ctx = new EFDbContext();

        public ItemMap()
        {
            //Key
            HasKey(t => t.Item_Id);

            //Property
            Property(t => t.Item_Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Nombre);
            Property(t => t.Descripcion);
            

            //table
            ToTable("Items");
        }
    }
}
