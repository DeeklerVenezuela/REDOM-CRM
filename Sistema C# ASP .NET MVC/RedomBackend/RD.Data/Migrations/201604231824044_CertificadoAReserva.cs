namespace RD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CertificadoAReserva : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reserva", "Certificado_CertificadoDeRegaloID", c => c.Int());
            CreateIndex("dbo.Reserva", "Certificado_CertificadoDeRegaloID");
            AddForeignKey("dbo.Reserva", "Certificado_CertificadoDeRegaloID", "dbo.CertificadoDeRegalo", "CertificadoDeRegaloID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reserva", "Certificado_CertificadoDeRegaloID", "dbo.CertificadoDeRegalo");
            DropIndex("dbo.Reserva", new[] { "Certificado_CertificadoDeRegaloID" });
            DropColumn("dbo.Reserva", "Certificado_CertificadoDeRegaloID");
        }
    }
}
