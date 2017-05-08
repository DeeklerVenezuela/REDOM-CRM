namespace RD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CalledMV : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembresiaVence", "Called", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MembresiaVence", "Called");
        }
    }
}
