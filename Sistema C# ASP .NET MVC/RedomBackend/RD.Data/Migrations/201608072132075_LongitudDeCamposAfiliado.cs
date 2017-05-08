namespace RD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LongitudDeCamposAfiliado : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Afiliado", "Apellido1", c => c.String(maxLength: 50));
            AlterColumn("dbo.Afiliado", "Apellido2", c => c.String(maxLength: 50));
            AlterColumn("dbo.Afiliado", "RazonSocial", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Afiliado", "RazonSocial", c => c.String(maxLength: 25));
            AlterColumn("dbo.Afiliado", "Apellido2", c => c.String(maxLength: 20));
            AlterColumn("dbo.Afiliado", "Apellido1", c => c.String(maxLength: 20));
        }
    }
}
