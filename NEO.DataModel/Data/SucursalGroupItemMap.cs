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
    public class SucursalGroupItemMap : EntityTypeConfiguration<SucursalGroupItem>
    {
        protected EFDbContext ctx = new EFDbContext();

        public SucursalGroupItemMap()
        {
            //Key
            HasKey(t => t.SucursalGroupItem_Id);

            //Property
            Property(t => t.SucursalGroupItem_Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
          

            //table
            ToTable("SucursalesGroupItem");
        }
    }
}
