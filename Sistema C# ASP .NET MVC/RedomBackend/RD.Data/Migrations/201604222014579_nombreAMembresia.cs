namespace RD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nombreAMembresia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Membresia", "Nombre", c => c.String(maxLength: 45));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Membresia", "Nombre");
        }
    }
}
