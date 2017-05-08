namespace RD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregarTrabajadoAProspecto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prospecto", "Trabajado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prospecto", "Trabajado");
        }
    }
}
