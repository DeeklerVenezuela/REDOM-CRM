namespace RD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class longitudDeDireccion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Direccion", "Descripcion", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Direccion", "Descripcion", c => c.String(maxLength: 60));
        }
    }
}
