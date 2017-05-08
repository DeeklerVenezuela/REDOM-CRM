namespace RD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GastosSecond : DbMigration
    {
        public override void Up()
        {            
            AddColumn("dbo.Gasto", "Nombre", c => c.String());
            AddColumn("dbo.Gasto", "cedula", c => c.String());
        }
        
        public override void Down()
        {            
            DropColumn("dbo.Gasto", "cedula");
            DropColumn("dbo.Gasto", "Nombre");
        }
    }
}
