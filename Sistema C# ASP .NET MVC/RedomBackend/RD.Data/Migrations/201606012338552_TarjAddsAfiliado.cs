namespace RD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TarjAddsAfiliado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TarjetaAdicional", "Afiliado_AfiliadoID", c => c.Int());
            CreateIndex("dbo.TarjetaAdicional", "Afiliado_AfiliadoID");
            AddForeignKey("dbo.TarjetaAdicional", "Afiliado_AfiliadoID", "dbo.Afiliado", "AfiliadoID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TarjetaAdicional", "Afiliado_AfiliadoID", "dbo.Afiliado");
            DropIndex("dbo.TarjetaAdicional", new[] { "Afiliado_AfiliadoID" });
            DropColumn("dbo.TarjetaAdicional", "Afiliado_AfiliadoID");
        }
    }
}
