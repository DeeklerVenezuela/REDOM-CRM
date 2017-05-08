namespace RD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hoteldummy : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Hotel", "HotelDummy_HotelesDummyID", "dbo.HotelDummy");
            DropIndex("dbo.Hotel", new[] { "HotelDummy_HotelesDummyID" });
            AddColumn("dbo.HotelDummy", "Hotel_HotelID", c => c.Int());
            CreateIndex("dbo.HotelDummy", "Hotel_HotelID");
            AddForeignKey("dbo.HotelDummy", "Hotel_HotelID", "dbo.Hotel", "HotelID");
            DropColumn("dbo.Hotel", "HotelDummy_HotelesDummyID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Hotel", "HotelDummy_HotelesDummyID", c => c.Int());
            DropForeignKey("dbo.HotelDummy", "Hotel_HotelID", "dbo.Hotel");
            DropIndex("dbo.HotelDummy", new[] { "Hotel_HotelID" });
            DropColumn("dbo.HotelDummy", "Hotel_HotelID");
            CreateIndex("dbo.Hotel", "HotelDummy_HotelesDummyID");
            AddForeignKey("dbo.Hotel", "HotelDummy_HotelesDummyID", "dbo.HotelDummy", "HotelesDummyID");
        }
    }
}
