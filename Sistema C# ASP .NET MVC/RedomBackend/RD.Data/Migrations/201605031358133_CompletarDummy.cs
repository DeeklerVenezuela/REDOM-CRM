namespace RD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompletarDummy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HotelDummy",
                c => new
                    {
                        HotelesDummyID = c.Int(nullable: false, identity: true),
                        Ubicacion = c.String(maxLength: 50),
                        Color = c.String(maxLength: 35),
                    })
                .PrimaryKey(t => t.HotelesDummyID);
            
            CreateTable(
                "dbo.ScriptDummy",
                c => new
                    {
                        ScriptDummyID = c.Int(nullable: false, identity: true),
                        Titulo = c.String(maxLength: 50),
                        Descripcion = c.String(storeType: "ntext"),
                        Color = c.String(maxLength: 35),
                    })
                .PrimaryKey(t => t.ScriptDummyID);
            
            AddColumn("dbo.Hotel", "HotelDummy_HotelesDummyID", c => c.Int());
            CreateIndex("dbo.Hotel", "HotelDummy_HotelesDummyID");
            AddForeignKey("dbo.Hotel", "HotelDummy_HotelesDummyID", "dbo.HotelDummy", "HotelesDummyID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Hotel", "HotelDummy_HotelesDummyID", "dbo.HotelDummy");
            DropIndex("dbo.Hotel", new[] { "HotelDummy_HotelesDummyID" });
            DropColumn("dbo.Hotel", "HotelDummy_HotelesDummyID");
            DropTable("dbo.ScriptDummy");
            DropTable("dbo.HotelDummy");
        }
    }
}
