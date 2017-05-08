namespace RD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class objeciones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ObjecionesDummy",
                c => new
                    {
                        ObjecionesDummyID = c.Int(nullable: false, identity: true),
                        Titulo = c.String(maxLength: 50),
                        Descripcion = c.String(storeType: "ntext"),
                        Color = c.String(maxLength: 35),
                    })
                .PrimaryKey(t => t.ObjecionesDummyID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ObjecionesDummy");
        }
    }
}
