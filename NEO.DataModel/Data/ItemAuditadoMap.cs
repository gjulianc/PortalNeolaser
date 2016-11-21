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
    public class ItemAuditadoMap : EntityTypeConfiguration<ItemAuditado>
    {
        protected EFDbContext ctx = new EFDbContext();

        public ItemAuditadoMap()
        {
            //Key
            HasKey(t => t.ItemAuditado_Id);

            //Property
            Property(t => t.ItemAuditado_Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Estado);
            Property(t => t.Descripcion);
            Property(t => t.Foto);


            //table
            ToTable("ItemsAuditados");
        }
    }
}
