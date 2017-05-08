using RD.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD.Data.DAL
{
    public class DbContextRD : DbContext
    {
        public DbContextRD()
            : base("DefaultConnection2")
        {
            Database.SetInitializer<DbContextRD>(new MyDbInitializer());
            //Database.SetInitializer<DbContextRD>(new CreateDatabaseIfNotExists<DbContextRD>());
            //Database.SetInitializer<DbContextRD>(new CreateDatabaseIfNotExists<DbContextRD>());
        }

        #region dbsets
        //All DbSet to create MySQL tables
        public DbSet<Afiliado> Afiliados { get; set; }
        public DbSet<MembresiaVence> MembresiaVences { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Archivo> Archivos { get; set; }
        public DbSet<Banco> Bancos { get; set; }
        public DbSet<CertificadoDeRegalo> Certificados { get; set; }
        public DbSet<ComprobanteFiscal> Comprobantes { get; set; }
        public DbSet<ControlDummy> ControlDummies { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Documentacion> Documentaciones { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<EmailUsuario> Emails { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Hotel> Hoteles { get; set; }
        public DbSet<HotelDummy> HotelesDummy { get; set; } 
        public DbSet<Login> Logins { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Membresia> Membresias { get; set; }
        public DbSet<NCF> NCFS { get; set; }
        public DbSet<ObjecionesDummy> Objeciones { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<PagosReserva> PagosReservas { get; set; }
        public DbSet<Prospecto> Prospectos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Reclamo> Reclamos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<ScriptDummy> ScriptDummies { get; set; }
        public DbSet<Tarjeta> Tarjetas { get; set; }
        public DbSet<TarjetaAdicional> TarjetaAdicional { get; set; }
        public DbSet<TarjetaUsuario> TarjetasUsuarios { get; set; }
        public DbSet<Telefono> Telefonos { get; set; }
        public DbSet<TipoComprobante> TipoComprobantes { get; set; }
        public DbSet<UserEstado> UserEstados { get; set; }
        public DbSet<UserPermision> UserPermissions { get; set; }
        //
        #endregion
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
        }
    }
}
