namespace RD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CFPagosR : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PagosReserva", "Comprobante_ComprbanteFiscalID", c => c.Int());
            CreateIndex("dbo.PagosReserva", "Comprobante_ComprbanteFiscalID");
            AddForeignKey("dbo.PagosReserva", "Comprobante_ComprbanteFiscalID", "dbo.ComprobanteFiscal", "ComprbanteFiscalID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PagosReserva", "Comprobante_ComprbanteFiscalID", "dbo.ComprobanteFiscal");
            DropIndex("dbo.PagosReserva", new[] { "Comprobante_ComprbanteFiscalID" });
            DropColumn("dbo.PagosReserva", "Comprobante_ComprbanteFiscalID");
        }
    }
}
