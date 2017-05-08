using RD.Data.Models;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace RD.Data.DAL
{
    public class MyDbInitializer : CreateDatabaseIfNotExists<RD.Data.DAL.DbContextRD>
    //public class MyDbInitializer : DropCreateDatabaseAlways<RD.Data.DAL.DbContextRD>
    {
        protected override void Seed(RD.Data.DAL.DbContextRD context)
        {
            // create User Admin to seed the database
            context.Membresias.Add(new Membresia { Duracion = 1, Costo = 200, FechaCreacion = DateTime.Now, Nombre ="Ejemplo" });
            context.UserEstados.Add(new UserEstado { Estado ="Activo"});
            context.Tarjetas.Add( new Tarjeta { Nombre="Visa" , Tipo="Visa" });
            context.Bancos.Add(new Banco { Nombre = "Banesco", Descripcion = "Banesco Banco Universal" });
            context.Status.Add(new Status { StatusID = 1, Fecha = DateTime.Now });
            var hoy = DateTime.Now;
            context.Hoteles.Add(new Hotel { Moneda = "USD", Nombre = "", ValidoDesde = hoy, ValidoHasta = hoy.AddMonths(1), Simple = 200, Doble = 180, Triple = 160, Cuadruple = 140, Ninio = 120, NinioGratis = 0 });
            context.Logins.Add(new Login { UserName = "ADMIN", Password = "Qfmg2vkXyfGIIsRpj7WoQVz/nz/GmL8Q" });
            context.Empresas.Add(new Empresa { Direccion = "", Email = "", Nombre = "Nombre de prueba", IDFiscal = "", Telefono = "", Website = "" });
            base.Seed(context);
        }
    }
}
