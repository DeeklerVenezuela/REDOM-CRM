namespace RD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ampliarcomentario : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Prospecto", "Comentario", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Prospecto", "Comentario", c => c.String(maxLength: 60));
        }
    }
}
