namespace NEO.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Usuario_Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Apellido1 = c.String(),
                        Apellido2 = c.String(),
                        Telefono = c.String(),
                        Email = c.String(),
                        Foto = c.String(),
                        User = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Rol = c.Int(nullable: false),
                        Cliente_Cliente_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Usuario_Id)
                .ForeignKey("dbo.Clientes", t => t.Cliente_Cliente_Id)
                .Index(t => t.Cliente_Cliente_Id);
            
            CreateTable(
                "dbo.Auditorias",
                c => new
                    {
                        Auditoria_Id = c.Int(nullable: false, identity: true),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaFin = c.DateTime(nullable: false),
                        Sucursal_Sucursal_Id = c.Int(),
                        Usuario_Usuario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Auditoria_Id)
                .ForeignKey("dbo.Sucursals", t => t.Sucursal_Sucursal_Id)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_Usuario_Id)
                .Index(t => t.Sucursal_Sucursal_Id)
                .Index(t => t.Usuario_Usuario_Id);
            
            CreateTable(
                "dbo.ItemAuditadoes",
                c => new
                    {
                        ItemAuditado_Id = c.Int(nullable: false, identity: true),
                        Estado = c.Boolean(nullable: false),
                        Descripcion = c.String(),
                        Foto = c.String(),
                        Auditoria_Auditoria_Id = c.Int(),
                        Item_Item_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ItemAuditado_Id)
                .ForeignKey("dbo.Auditorias", t => t.Auditoria_Auditoria_Id)
                .ForeignKey("dbo.Items", t => t.Item_Item_Id)
                .Index(t => t.Auditoria_Auditoria_Id)
                .Index(t => t.Item_Item_Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Item_Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                        GroupItem_GroupItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Item_Id)
                .ForeignKey("dbo.GroupItems", t => t.GroupItem_GroupItem_Id)
                .Index(t => t.GroupItem_GroupItem_Id);
            
            CreateTable(
                "dbo.GroupItems",
                c => new
                    {
                        GroupItem_Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.GroupItem_Id);
            
            CreateTable(
                "dbo.SucursalGroupItems",
                c => new
                    {
                        SucursalGroupItem_Id = c.Int(nullable: false, identity: true),
                        GroupItem_GroupItem_Id = c.Int(),
                        Sucursal_Sucursal_Id = c.Int(),
                    })
                .PrimaryKey(t => t.SucursalGroupItem_Id)
                .ForeignKey("dbo.GroupItems", t => t.GroupItem_GroupItem_Id)
                .ForeignKey("dbo.Sucursals", t => t.Sucursal_Sucursal_Id)
                .Index(t => t.GroupItem_GroupItem_Id)
                .Index(t => t.Sucursal_Sucursal_Id);
            
            CreateTable(
                "dbo.Sucursals",
                c => new
                    {
                        Sucursal_Id = c.Int(nullable: false, identity: true),
                        CodigoSAP = c.String(),
                        Descripcion = c.String(),
                        Direccion = c.String(),
                        Poblacion = c.String(),
                        Provincia = c.String(),
                        CodPostal = c.String(),
                        Cliente_Cliente_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Sucursal_Id)
                .ForeignKey("dbo.Clientes", t => t.Cliente_Cliente_Id)
                .Index(t => t.Cliente_Cliente_Id);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Cliente_Id = c.Int(nullable: false, identity: true),
                        Cif = c.String(),
                        Nombre = c.String(),
                        Telefono = c.String(),
                        Email = c.String(),
                        Logo = c.String(),
                        Direccion = c.String(),
                        Poblacion = c.String(),
                        Provincia = c.String(),
                        CodPostal = c.String(),
                    })
                .PrimaryKey(t => t.Cliente_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Auditorias", "Usuario_Usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.Auditorias", "Sucursal_Sucursal_Id", "dbo.Sucursals");
            DropForeignKey("dbo.ItemAuditadoes", "Item_Item_Id", "dbo.Items");
            DropForeignKey("dbo.SucursalGroupItems", "Sucursal_Sucursal_Id", "dbo.Sucursals");
            DropForeignKey("dbo.Usuarios", "Cliente_Cliente_Id", "dbo.Clientes");
            DropForeignKey("dbo.Sucursals", "Cliente_Cliente_Id", "dbo.Clientes");
            DropForeignKey("dbo.SucursalGroupItems", "GroupItem_GroupItem_Id", "dbo.GroupItems");
            DropForeignKey("dbo.Items", "GroupItem_GroupItem_Id", "dbo.GroupItems");
            DropForeignKey("dbo.ItemAuditadoes", "Auditoria_Auditoria_Id", "dbo.Auditorias");
            DropIndex("dbo.Sucursals", new[] { "Cliente_Cliente_Id" });
            DropIndex("dbo.SucursalGroupItems", new[] { "Sucursal_Sucursal_Id" });
            DropIndex("dbo.SucursalGroupItems", new[] { "GroupItem_GroupItem_Id" });
            DropIndex("dbo.Items", new[] { "GroupItem_GroupItem_Id" });
            DropIndex("dbo.ItemAuditadoes", new[] { "Item_Item_Id" });
            DropIndex("dbo.ItemAuditadoes", new[] { "Auditoria_Auditoria_Id" });
            DropIndex("dbo.Auditorias", new[] { "Usuario_Usuario_Id" });
            DropIndex("dbo.Auditorias", new[] { "Sucursal_Sucursal_Id" });
            DropIndex("dbo.Usuarios", new[] { "Cliente_Cliente_Id" });
            DropTable("dbo.Clientes");
            DropTable("dbo.Sucursals");
            DropTable("dbo.SucursalGroupItems");
            DropTable("dbo.GroupItems");
            DropTable("dbo.Items");
            DropTable("dbo.ItemAuditadoes");
            DropTable("dbo.Auditorias");
            DropTable("dbo.Usuarios");
        }
    }
}
