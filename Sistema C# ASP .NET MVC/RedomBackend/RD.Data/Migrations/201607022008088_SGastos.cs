namespace RD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SGastos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gasto", "status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Gasto", "status");
        }
    }
}
