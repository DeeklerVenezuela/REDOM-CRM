namespace RD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class permisos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPermision", "Certificados", c => c.String(maxLength: 8));
            AddColumn("dbo.UserPermision", "Personas", c => c.String(maxLength: 8));
            AddColumn("dbo.UserPermision", "Hoteles", c => c.String(maxLength: 8));
            AddColumn("dbo.UserPermision", "Sistema", c => c.String(maxLength: 8));
            AddColumn("dbo.UserPermision", "Especiales", c => c.String(maxLength: 20));
            DropColumn("dbo.UserPermision", "Users");
            DropColumn("dbo.UserPermision", "Empleadoes");
            DropColumn("dbo.UserPermision", "Membresias");
            DropColumn("dbo.UserPermision", "Reportes");
            DropColumn("dbo.UserPermision", "NCF");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserPermision", "NCF", c => c.String(maxLength: 8));
            AddColumn("dbo.UserPermision", "Reportes", c => c.String(maxLength: 8));
            AddColumn("dbo.UserPermision", "Membresias", c => c.String(maxLength: 8));
            AddColumn("dbo.UserPermision", "Empleadoes", c => c.String(maxLength: 8));
            AddColumn("dbo.UserPermision", "Users", c => c.String(maxLength: 8));
            DropColumn("dbo.UserPermision", "Especiales");
            DropColumn("dbo.UserPermision", "Sistema");
            DropColumn("dbo.UserPermision", "Hoteles");
            DropColumn("dbo.UserPermision", "Personas");
            DropColumn("dbo.UserPermision", "Certificados");
        }
    }
}
