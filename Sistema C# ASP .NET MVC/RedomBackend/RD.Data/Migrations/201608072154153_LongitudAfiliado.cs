namespace RD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LongitudAfiliado : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Afiliado", "Apellido1", c => c.String(maxLength: 256));
            AlterColumn("dbo.Afiliado", "Apellido2", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Afiliado", "Apellido2", c => c.String(maxLength: 50));
            AlterColumn("dbo.Afiliado", "Apellido1", c => c.String(maxLength: 50));
        }
    }
}
