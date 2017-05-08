namespace RD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MembresiaVence : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MembresiaVence",
                c => new
                    {
                        MembresiaVenceID = c.Int(nullable: false, identity: true),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaFin = c.DateTime(nullable: false),
                        AfiliadoM_AfiliadoID = c.Int(),
                    })
                .PrimaryKey(t => t.MembresiaVenceID)
                .ForeignKey("dbo.Afiliado", t => t.AfiliadoM_AfiliadoID)
                .Index(t => t.AfiliadoM_AfiliadoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MembresiaVence", "AfiliadoM_AfiliadoID", "dbo.Afiliado");
            DropIndex("dbo.MembresiaVence", new[] { "AfiliadoM_AfiliadoID" });
            DropTable("dbo.MembresiaVence");
        }
    }
}
