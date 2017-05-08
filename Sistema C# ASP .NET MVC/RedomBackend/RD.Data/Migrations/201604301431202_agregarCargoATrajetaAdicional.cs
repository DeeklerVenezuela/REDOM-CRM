namespace RD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregarCargoATrajetaAdicional : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TarjetaAdicional", "Cargo", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TarjetaAdicional", "Cargo");
        }
    }
}
