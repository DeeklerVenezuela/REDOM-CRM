namespace RD.Data.Migrations
{
    using RD.Data.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RD.Data.DAL.DbContextRD>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(RD.Data.DAL.DbContextRD context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Logins.Add(new Login { UserName = "ADMIN", Password = "Qfmg2vkXyfGIIsRpj7WoQVz/nz/GmL8Q" });
            //context.Empresas.Add(new Empresa { Direccion = "", Email = "", Nombre = "Nombre de prueba", IDFiscal = "", Telefono = "", Website = "" });
        }
    }
}
