namespace RD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hotelError : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HotelDummy", "Hotel_HotelID", "dbo.Hotel");
            DropIndex("dbo.HotelDummy", new[] { "Hotel_HotelID" });
            AddColumn("dbo.HotelDummy", "Hotel", c => c.String(maxLength: 50));
            DropColumn("dbo.HotelDummy", "Hotel_HotelID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HotelDummy", "Hotel_HotelID", c => c.Int());
            DropColumn("dbo.HotelDummy", "Hotel");
            CreateIndex("dbo.HotelDummy", "Hotel_HotelID");
            AddForeignKey("dbo.HotelDummy", "Hotel_HotelID", "dbo.Hotel", "HotelID");
        }
    }
}
