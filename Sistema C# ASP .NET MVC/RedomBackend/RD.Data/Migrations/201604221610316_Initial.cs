namespace RD.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Afiliado",
                c => new
                    {
                        AfiliadoID = c.Int(nullable: false, identity: true),
                        AfiliadoIndice = c.Int(nullable: false),
                        Cedula = c.String(maxLength: 60),
                        Pasaporte = c.String(maxLength: 60),
                        Nombres = c.String(maxLength: 35),
                        Apellido1 = c.String(maxLength: 20),
                        Apellido2 = c.String(maxLength: 20),
                        RazonSocial = c.String(maxLength: 25),
                        RNC = c.String(maxLength: 60),
                        Email = c.String(maxLength: 30),
                        Cumpleanio = c.DateTime(nullable: false),
                        MesesAdicionales = c.Boolean(nullable: false),
                        MesAdicionalCan = c.Int(nullable: false),
                        TarjetaAdicional = c.Boolean(nullable: false),
                        TarjetaAdicionalCan = c.Int(nullable: false),
                        FechaVencimiento = c.DateTime(nullable: false),
                        FechaHora = c.DateTime(nullable: false),
                        Observaciones = c.String(maxLength: 255),
                        Status = c.String(maxLength: 15),
                        CostoMembresia = c.Int(nullable: false),
                        UsuarioBienvenida_EmpleadoID = c.Int(),
                        UsuarioRegistro_EmpleadoID = c.Int(),
                    })
                .PrimaryKey(t => t.AfiliadoID)
                .ForeignKey("dbo.Empleado", t => t.UsuarioBienvenida_EmpleadoID)
                .ForeignKey("dbo.Empleado", t => t.UsuarioRegistro_EmpleadoID)
                .Index(t => t.UsuarioBienvenida_EmpleadoID)
                .Index(t => t.UsuarioRegistro_EmpleadoID);
            
            CreateTable(
                "dbo.Direccion",
                c => new
                    {
                        DireccionID = c.Int(nullable: false, identity: true),
                        Tipo = c.String(maxLength: 15),
                        Descripcion = c.String(maxLength: 60),
                        Afiliado_AfiliadoID = c.Int(),
                        Prospecto_ProspectoID = c.Int(),
                    })
                .PrimaryKey(t => t.DireccionID)
                .ForeignKey("dbo.Afiliado", t => t.Afiliado_AfiliadoID)
                .ForeignKey("dbo.Prospecto", t => t.Prospecto_ProspectoID)
                .Index(t => t.Afiliado_AfiliadoID)
                .Index(t => t.Prospecto_ProspectoID);
            
            CreateTable(
                "dbo.EmailUsuario",
                c => new
                    {
                        EmailUsuarioID = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 50),
                        Afiliado_AfiliadoID = c.Int(),
                    })
                .PrimaryKey(t => t.EmailUsuarioID)
                .ForeignKey("dbo.Afiliado", t => t.Afiliado_AfiliadoID)
                .Index(t => t.Afiliado_AfiliadoID);
            
            CreateTable(
                "dbo.TarjetaUsuario",
                c => new
                    {
                        TarjetaUsuarioID = c.Int(nullable: false, identity: true),
                        Banco = c.String(maxLength: 30),
                        Tipo = c.String(maxLength: 30),
                        NumeroTarjeta = c.String(maxLength: 180),
                        FechaVencimiento = c.DateTime(nullable: false),
                        Afiliado_AfiliadoID = c.Int(),
                    })
                .PrimaryKey(t => t.TarjetaUsuarioID)
                .ForeignKey("dbo.Afiliado", t => t.Afiliado_AfiliadoID)
                .Index(t => t.Afiliado_AfiliadoID);
            
            CreateTable(
                "dbo.Telefono",
                c => new
                    {
                        TelefonoID = c.Int(nullable: false, identity: true),
                        Tipo = c.String(maxLength: 15),
                        Descripcion = c.String(maxLength: 45),
                        Extension = c.String(maxLength: 6),
                        Afiliado_AfiliadoID = c.Int(),
                        Prospecto_ProspectoID = c.Int(),
                    })
                .PrimaryKey(t => t.TelefonoID)
                .ForeignKey("dbo.Afiliado", t => t.Afiliado_AfiliadoID)
                .ForeignKey("dbo.Prospecto", t => t.Prospecto_ProspectoID)
                .Index(t => t.Afiliado_AfiliadoID)
                .Index(t => t.Prospecto_ProspectoID);
            
            CreateTable(
                "dbo.Empleado",
                c => new
                    {
                        EmpleadoID = c.Int(nullable: false, identity: true),
                        Nombres = c.String(nullable: false, maxLength: 35),
                        Apellido1 = c.String(nullable: false, maxLength: 20),
                        Apellido2 = c.String(maxLength: 20),
                        Cedula = c.String(nullable: false, maxLength: 15),
                        Cumpleanio = c.DateTime(nullable: false),
                        Comentarios = c.String(maxLength: 60),
                        Status = c.String(nullable: false, maxLength: 15),
                        SueldoBase = c.Int(nullable: false),
                        PComisionMembresia = c.Int(nullable: false),
                        PComisionReserva = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmpleadoID);
            
            CreateTable(
                "dbo.Archivo",
                c => new
                    {
                        ArchivoID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 30),
                        FechaHora = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                        Status = c.Boolean(nullable: false),
                        Content = c.Binary(),
                    })
                .PrimaryKey(t => t.ArchivoID);
            
            CreateTable(
                "dbo.Prospecto",
                c => new
                    {
                        ProspectoID = c.Int(nullable: false, identity: true),
                        Nombres = c.String(maxLength: 35),
                        Apellido1 = c.String(maxLength: 20),
                        Apellido2 = c.String(maxLength: 20),
                        Cedula = c.String(maxLength: 30),
                        Respuesta = c.String(maxLength: 255),
                        Fax = c.String(maxLength: 30),
                        FechaHora = c.DateTime(nullable: false),
                        Comentario = c.String(maxLength: 60),
                        FechaDeNacimiento = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 30),
                        Activo = c.Boolean(nullable: false),
                        Ocupado = c.Boolean(nullable: false),
                        Afiliado = c.Boolean(nullable: false),
                        Archivo_ArchivoID = c.Int(),
                        Empleado_EmpleadoID = c.Int(),
                    })
                .PrimaryKey(t => t.ProspectoID)
                .ForeignKey("dbo.Archivo", t => t.Archivo_ArchivoID)
                .ForeignKey("dbo.Empleado", t => t.Empleado_EmpleadoID)
                .Index(t => t.Archivo_ArchivoID)
                .Index(t => t.Empleado_EmpleadoID);
            
            CreateTable(
                "dbo.Banco",
                c => new
                    {
                        BancoID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 30),
                        Descripcion = c.String(maxLength: 120),
                        Tarjeta_TarjetaID = c.Int(),
                    })
                .PrimaryKey(t => t.BancoID)
                .ForeignKey("dbo.Tarjeta", t => t.Tarjeta_TarjetaID)
                .Index(t => t.Tarjeta_TarjetaID);
            
            CreateTable(
                "dbo.ComprobanteFiscal",
                c => new
                    {
                        ComprbanteFiscalID = c.Int(nullable: false, identity: true),
                        Secuencia = c.String(maxLength: 30),
                        FechaCreacion = c.DateTime(nullable: false),
                        Tipo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ComprbanteFiscalID);
            
            CreateTable(
                "dbo.ControlDummy",
                c => new
                    {
                        ControlDummyID = c.Int(nullable: false, identity: true),
                        FechaHora = c.DateTime(nullable: false),
                        Prospecto_ProspectoID = c.Int(),
                        User_LoginID = c.Int(),
                    })
                .PrimaryKey(t => t.ControlDummyID)
                .ForeignKey("dbo.Prospecto", t => t.Prospecto_ProspectoID)
                .ForeignKey("dbo.Login", t => t.User_LoginID)
                .Index(t => t.Prospecto_ProspectoID)
                .Index(t => t.User_LoginID);
            
            CreateTable(
                "dbo.Login",
                c => new
                    {
                        LoginID = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 30),
                        Password = c.String(maxLength: 60),
                        Empleado_EmpleadoID = c.Int(),
                        Rol_RolID = c.Int(),
                    })
                .PrimaryKey(t => t.LoginID)
                .ForeignKey("dbo.Empleado", t => t.Empleado_EmpleadoID)
                .ForeignKey("dbo.Rol", t => t.Rol_RolID)
                .Index(t => t.Empleado_EmpleadoID)
                .Index(t => t.Rol_RolID);
            
            CreateTable(
                "dbo.Rol",
                c => new
                    {
                        RolID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 150),
                        FechaHora = c.DateTime(nullable: false),
                        Creador_EmpleadoID = c.Int(),
                        Permisos_UserPermisionID = c.Int(),
                    })
                .PrimaryKey(t => t.RolID)
                .ForeignKey("dbo.Empleado", t => t.Creador_EmpleadoID)
                .ForeignKey("dbo.UserPermision", t => t.Permisos_UserPermisionID)
                .Index(t => t.Creador_EmpleadoID)
                .Index(t => t.Permisos_UserPermisionID);
            
            CreateTable(
                "dbo.UserPermision",
                c => new
                    {
                        UserPermisionID = c.Int(nullable: false, identity: true),
                        Afiliados = c.String(maxLength: 8),
                        Archivos = c.String(maxLength: 8),
                        Users = c.String(maxLength: 8),
                        Empleadoes = c.String(maxLength: 8),
                        Dummy = c.String(maxLength: 8),
                        Membresias = c.String(maxLength: 8),
                        Cobros = c.String(maxLength: 8),
                        Prospectos = c.String(maxLength: 8),
                        Reservas = c.String(maxLength: 8),
                        Permisos = c.String(maxLength: 8),
                        Reportes = c.String(maxLength: 8),
                        Documentacion = c.String(maxLength: 8),
                        NCF = c.String(maxLength: 8),
                        Empleado_EmpleadoID = c.Int(),
                    })
                .PrimaryKey(t => t.UserPermisionID)
                .ForeignKey("dbo.Empleado", t => t.Empleado_EmpleadoID)
                .Index(t => t.Empleado_EmpleadoID);
            
            CreateTable(
                "dbo.Documentacion",
                c => new
                    {
                        DocumentacionID = c.Int(nullable: false, identity: true),
                        CartaBienvenida = c.Boolean(nullable: false),
                        Carnet = c.Boolean(nullable: false),
                        LabelSobres = c.Boolean(nullable: false),
                        CertificadoRegalo = c.Boolean(nullable: false),
                        CertificadoRegalo2 = c.Boolean(nullable: false),
                        Generada = c.Boolean(nullable: false),
                        Afiliado_AfiliadoID = c.Int(),
                        ModificadoPor_EmpleadoID = c.Int(),
                    })
                .PrimaryKey(t => t.DocumentacionID)
                .ForeignKey("dbo.Afiliado", t => t.Afiliado_AfiliadoID)
                .ForeignKey("dbo.Empleado", t => t.ModificadoPor_EmpleadoID)
                .Index(t => t.Afiliado_AfiliadoID)
                .Index(t => t.ModificadoPor_EmpleadoID);
            
            CreateTable(
                "dbo.Empresa",
                c => new
                    {
                        EmpresaID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 130),
                        IDFiscal = c.String(maxLength: 130),
                        Direccion = c.String(maxLength: 160),
                        Email = c.String(maxLength: 45),
                        Telefono = c.String(maxLength: 200),
                        Website = c.String(maxLength: 200),
                        MesesAdicionales = c.Int(nullable: false),
                        TarjetasAdicionales = c.Int(nullable: false),
                        MontoDocumentacion = c.Int(nullable: false),
                        MontoReserva = c.Int(nullable: false),
                        TasaDeCambio = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmpresaID);
            
            CreateTable(
                "dbo.Gasto",
                c => new
                    {
                        GastoID = c.Int(nullable: false, identity: true),
                        Concepto = c.String(nullable: false, maxLength: 30),
                        Observaciones = c.String(maxLength: 60),
                        FechaHora = c.DateTime(nullable: false),
                        Empleado_EmpleadoID = c.Int(),
                    })
                .PrimaryKey(t => t.GastoID)
                .ForeignKey("dbo.Empleado", t => t.Empleado_EmpleadoID)
                .Index(t => t.Empleado_EmpleadoID);
            
            CreateTable(
                "dbo.Hotel",
                c => new
                    {
                        HotelID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(maxLength: 125),
                        Moneda = c.String(),
                        Simple = c.Int(nullable: false),
                        Doble = c.Int(nullable: false),
                        Triple = c.Int(nullable: false),
                        Cuadruple = c.Int(nullable: false),
                        Ninio = c.Int(nullable: false),
                        NinioGratis = c.Int(nullable: false),
                        ValidoDesde = c.DateTime(nullable: false),
                        ValidoHasta = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.HotelID);
            
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        LogID = c.Int(nullable: false, identity: true),
                        FechaHora = c.DateTime(nullable: false),
                        Direccion = c.String(nullable: false, maxLength: 15),
                        Tipo = c.String(nullable: false, maxLength: 45),
                        Empleado_EmpleadoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LogID)
                .ForeignKey("dbo.Empleado", t => t.Empleado_EmpleadoID, cascadeDelete: true)
                .Index(t => t.Empleado_EmpleadoID);
            
            CreateTable(
                "dbo.Membresia",
                c => new
                    {
                        MembresiaID = c.Int(nullable: false, identity: true),
                        Costo = c.Int(nullable: false),
                        Duracion = c.Int(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MembresiaID);
            
            CreateTable(
                "dbo.NCF",
                c => new
                    {
                        NCFID = c.Int(nullable: false, identity: true),
                        Serie = c.String(maxLength: 25),
                        NumeradorInicial = c.String(maxLength: 25),
                        NumeradorFinal = c.String(maxLength: 25),
                        NumeradorActual = c.String(maxLength: 25),
                        IndicadorDeFinal = c.Int(nullable: false),
                        TipoComprobante_TipoComprobanteID = c.Int(),
                    })
                .PrimaryKey(t => t.NCFID)
                .ForeignKey("dbo.TipoComprobante", t => t.TipoComprobante_TipoComprobanteID)
                .Index(t => t.TipoComprobante_TipoComprobanteID);
            
            CreateTable(
                "dbo.TipoComprobante",
                c => new
                    {
                        TipoComprobanteID = c.Int(nullable: false, identity: true),
                        Codigo = c.String(maxLength: 4),
                        Nombre = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.TipoComprobanteID);
            
            CreateTable(
                "dbo.Pago",
                c => new
                    {
                        PagoID = c.Int(nullable: false, identity: true),
                        FechaHora = c.DateTime(nullable: false),
                        CostoMembresia = c.Int(nullable: false),
                        MontoPago = c.Int(nullable: false),
                        Comentario = c.String(maxLength: 60),
                        Afiliado_AfiliadoID = c.Int(),
                        Comprobante_ComprbanteFiscalID = c.Int(),
                        Empleado_EmpleadoID = c.Int(),
                        TarjetaUsuario_TarjetaUsuarioID = c.Int(),
                    })
                .PrimaryKey(t => t.PagoID)
                .ForeignKey("dbo.Afiliado", t => t.Afiliado_AfiliadoID)
                .ForeignKey("dbo.ComprobanteFiscal", t => t.Comprobante_ComprbanteFiscalID)
                .ForeignKey("dbo.Empleado", t => t.Empleado_EmpleadoID)
                .ForeignKey("dbo.TarjetaUsuario", t => t.TarjetaUsuario_TarjetaUsuarioID)
                .Index(t => t.Afiliado_AfiliadoID)
                .Index(t => t.Comprobante_ComprbanteFiscalID)
                .Index(t => t.Empleado_EmpleadoID)
                .Index(t => t.TarjetaUsuario_TarjetaUsuarioID);
            
            CreateTable(
                "dbo.PagosReserva",
                c => new
                    {
                        PagoReservaID = c.Int(nullable: false, identity: true),
                        Monto = c.Int(nullable: false),
                        FechaHora = c.DateTime(nullable: false),
                        Afiliado_AfiliadoID = c.Int(),
                        Reserva_ReservaID = c.Int(),
                        Tarjeta_TarjetaUsuarioID = c.Int(),
                    })
                .PrimaryKey(t => t.PagoReservaID)
                .ForeignKey("dbo.Afiliado", t => t.Afiliado_AfiliadoID)
                .ForeignKey("dbo.Reserva", t => t.Reserva_ReservaID)
                .ForeignKey("dbo.TarjetaUsuario", t => t.Tarjeta_TarjetaUsuarioID)
                .Index(t => t.Afiliado_AfiliadoID)
                .Index(t => t.Reserva_ReservaID)
                .Index(t => t.Tarjeta_TarjetaUsuarioID);
            
            CreateTable(
                "dbo.Reserva",
                c => new
                    {
                        ReservaID = c.Int(nullable: false, identity: true),
                        FechaHora = c.DateTime(nullable: false),
                        Costo = c.Int(nullable: false),
                        Status = c.String(maxLength: 50),
                        CantidadAdultos = c.Int(nullable: false),
                        CantidadNinios = c.Int(nullable: false),
                        Entrada = c.DateTime(nullable: false),
                        Dias = c.Int(nullable: false),
                        Afiliado_AfiliadoID = c.Int(),
                        Empleado_EmpleadoID = c.Int(),
                        Hotel_HotelID = c.Int(),
                    })
                .PrimaryKey(t => t.ReservaID)
                .ForeignKey("dbo.Afiliado", t => t.Afiliado_AfiliadoID)
                .ForeignKey("dbo.Empleado", t => t.Empleado_EmpleadoID)
                .ForeignKey("dbo.Hotel", t => t.Hotel_HotelID)
                .Index(t => t.Afiliado_AfiliadoID)
                .Index(t => t.Empleado_EmpleadoID)
                .Index(t => t.Hotel_HotelID);
            
            CreateTable(
                "dbo.HabitacionReserva",
                c => new
                    {
                        HabitacionReservaID = c.Int(nullable: false, identity: true),
                        Tipo = c.String(),
                        Cantidad = c.String(),
                        Reserva_ReservaID = c.Int(),
                    })
                .PrimaryKey(t => t.HabitacionReservaID)
                .ForeignKey("dbo.Reserva", t => t.Reserva_ReservaID)
                .Index(t => t.Reserva_ReservaID);
            
            CreateTable(
                "dbo.Proveedor",
                c => new
                    {
                        ProveedorID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ProveedorID);
            
            CreateTable(
                "dbo.Reclamo",
                c => new
                    {
                        ReclamoID = c.Int(nullable: false, identity: true),
                        Status = c.String(nullable: false, maxLength: 15),
                        Titulo = c.String(nullable: false, maxLength: 20),
                        Descripcion = c.String(nullable: false, maxLength: 60),
                        TiempoDeProcesamiento = c.Int(nullable: false),
                        UnidadDeTiempo = c.String(nullable: false, maxLength: 7),
                        FechaHora = c.DateTime(nullable: false),
                        Afiliado_AfiliadoID = c.Int(),
                        Empleado_EmpleadoID = c.Int(),
                    })
                .PrimaryKey(t => t.ReclamoID)
                .ForeignKey("dbo.Afiliado", t => t.Afiliado_AfiliadoID)
                .ForeignKey("dbo.Empleado", t => t.Empleado_EmpleadoID)
                .Index(t => t.Afiliado_AfiliadoID)
                .Index(t => t.Empleado_EmpleadoID);
            
            CreateTable(
                "dbo.TarjetaAdicional",
                c => new
                    {
                        TarjetaAdicionalID = c.Int(nullable: false, identity: true),
                        Cedula = c.String(maxLength: 60),
                        Nombre = c.String(maxLength: 30),
                        Telefono1 = c.String(maxLength: 30),
                        Telefono2 = c.String(maxLength: 30),
                        Email = c.String(maxLength: 30),
                        Direccion = c.String(maxLength: 60),
                    })
                .PrimaryKey(t => t.TarjetaAdicionalID);
            
            CreateTable(
                "dbo.Tarjeta",
                c => new
                    {
                        TarjetaID = c.Int(nullable: false, identity: true),
                        Tipo = c.String(maxLength: 20),
                        Nombre = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.TarjetaID);
            
            CreateTable(
                "dbo.UserEstado",
                c => new
                    {
                        UserEstadoID = c.Int(nullable: false, identity: true),
                        Estado = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.UserEstadoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Banco", "Tarjeta_TarjetaID", "dbo.Tarjeta");
            DropForeignKey("dbo.Reclamo", "Empleado_EmpleadoID", "dbo.Empleado");
            DropForeignKey("dbo.Reclamo", "Afiliado_AfiliadoID", "dbo.Afiliado");
            DropForeignKey("dbo.PagosReserva", "Tarjeta_TarjetaUsuarioID", "dbo.TarjetaUsuario");
            DropForeignKey("dbo.PagosReserva", "Reserva_ReservaID", "dbo.Reserva");
            DropForeignKey("dbo.Reserva", "Hotel_HotelID", "dbo.Hotel");
            DropForeignKey("dbo.HabitacionReserva", "Reserva_ReservaID", "dbo.Reserva");
            DropForeignKey("dbo.Reserva", "Empleado_EmpleadoID", "dbo.Empleado");
            DropForeignKey("dbo.Reserva", "Afiliado_AfiliadoID", "dbo.Afiliado");
            DropForeignKey("dbo.PagosReserva", "Afiliado_AfiliadoID", "dbo.Afiliado");
            DropForeignKey("dbo.Pago", "TarjetaUsuario_TarjetaUsuarioID", "dbo.TarjetaUsuario");
            DropForeignKey("dbo.Pago", "Empleado_EmpleadoID", "dbo.Empleado");
            DropForeignKey("dbo.Pago", "Comprobante_ComprbanteFiscalID", "dbo.ComprobanteFiscal");
            DropForeignKey("dbo.Pago", "Afiliado_AfiliadoID", "dbo.Afiliado");
            DropForeignKey("dbo.NCF", "TipoComprobante_TipoComprobanteID", "dbo.TipoComprobante");
            DropForeignKey("dbo.Log", "Empleado_EmpleadoID", "dbo.Empleado");
            DropForeignKey("dbo.Gasto", "Empleado_EmpleadoID", "dbo.Empleado");
            DropForeignKey("dbo.Documentacion", "ModificadoPor_EmpleadoID", "dbo.Empleado");
            DropForeignKey("dbo.Documentacion", "Afiliado_AfiliadoID", "dbo.Afiliado");
            DropForeignKey("dbo.ControlDummy", "User_LoginID", "dbo.Login");
            DropForeignKey("dbo.Login", "Rol_RolID", "dbo.Rol");
            DropForeignKey("dbo.Rol", "Permisos_UserPermisionID", "dbo.UserPermision");
            DropForeignKey("dbo.UserPermision", "Empleado_EmpleadoID", "dbo.Empleado");
            DropForeignKey("dbo.Rol", "Creador_EmpleadoID", "dbo.Empleado");
            DropForeignKey("dbo.Login", "Empleado_EmpleadoID", "dbo.Empleado");
            DropForeignKey("dbo.ControlDummy", "Prospecto_ProspectoID", "dbo.Prospecto");
            DropForeignKey("dbo.Telefono", "Prospecto_ProspectoID", "dbo.Prospecto");
            DropForeignKey("dbo.Prospecto", "Empleado_EmpleadoID", "dbo.Empleado");
            DropForeignKey("dbo.Direccion", "Prospecto_ProspectoID", "dbo.Prospecto");
            DropForeignKey("dbo.Prospecto", "Archivo_ArchivoID", "dbo.Archivo");
            DropForeignKey("dbo.Afiliado", "UsuarioRegistro_EmpleadoID", "dbo.Empleado");
            DropForeignKey("dbo.Afiliado", "UsuarioBienvenida_EmpleadoID", "dbo.Empleado");
            DropForeignKey("dbo.Telefono", "Afiliado_AfiliadoID", "dbo.Afiliado");
            DropForeignKey("dbo.TarjetaUsuario", "Afiliado_AfiliadoID", "dbo.Afiliado");
            DropForeignKey("dbo.EmailUsuario", "Afiliado_AfiliadoID", "dbo.Afiliado");
            DropForeignKey("dbo.Direccion", "Afiliado_AfiliadoID", "dbo.Afiliado");
            DropIndex("dbo.Reclamo", new[] { "Empleado_EmpleadoID" });
            DropIndex("dbo.Reclamo", new[] { "Afiliado_AfiliadoID" });
            DropIndex("dbo.HabitacionReserva", new[] { "Reserva_ReservaID" });
            DropIndex("dbo.Reserva", new[] { "Hotel_HotelID" });
            DropIndex("dbo.Reserva", new[] { "Empleado_EmpleadoID" });
            DropIndex("dbo.Reserva", new[] { "Afiliado_AfiliadoID" });
            DropIndex("dbo.PagosReserva", new[] { "Tarjeta_TarjetaUsuarioID" });
            DropIndex("dbo.PagosReserva", new[] { "Reserva_ReservaID" });
            DropIndex("dbo.PagosReserva", new[] { "Afiliado_AfiliadoID" });
            DropIndex("dbo.Pago", new[] { "TarjetaUsuario_TarjetaUsuarioID" });
            DropIndex("dbo.Pago", new[] { "Empleado_EmpleadoID" });
            DropIndex("dbo.Pago", new[] { "Comprobante_ComprbanteFiscalID" });
            DropIndex("dbo.Pago", new[] { "Afiliado_AfiliadoID" });
            DropIndex("dbo.NCF", new[] { "TipoComprobante_TipoComprobanteID" });
            DropIndex("dbo.Log", new[] { "Empleado_EmpleadoID" });
            DropIndex("dbo.Gasto", new[] { "Empleado_EmpleadoID" });
            DropIndex("dbo.Documentacion", new[] { "ModificadoPor_EmpleadoID" });
            DropIndex("dbo.Documentacion", new[] { "Afiliado_AfiliadoID" });
            DropIndex("dbo.UserPermision", new[] { "Empleado_EmpleadoID" });
            DropIndex("dbo.Rol", new[] { "Permisos_UserPermisionID" });
            DropIndex("dbo.Rol", new[] { "Creador_EmpleadoID" });
            DropIndex("dbo.Login", new[] { "Rol_RolID" });
            DropIndex("dbo.Login", new[] { "Empleado_EmpleadoID" });
            DropIndex("dbo.ControlDummy", new[] { "User_LoginID" });
            DropIndex("dbo.ControlDummy", new[] { "Prospecto_ProspectoID" });
            DropIndex("dbo.Banco", new[] { "Tarjeta_TarjetaID" });
            DropIndex("dbo.Prospecto", new[] { "Empleado_EmpleadoID" });
            DropIndex("dbo.Prospecto", new[] { "Archivo_ArchivoID" });
            DropIndex("dbo.Telefono", new[] { "Prospecto_ProspectoID" });
            DropIndex("dbo.Telefono", new[] { "Afiliado_AfiliadoID" });
            DropIndex("dbo.TarjetaUsuario", new[] { "Afiliado_AfiliadoID" });
            DropIndex("dbo.EmailUsuario", new[] { "Afiliado_AfiliadoID" });
            DropIndex("dbo.Direccion", new[] { "Prospecto_ProspectoID" });
            DropIndex("dbo.Direccion", new[] { "Afiliado_AfiliadoID" });
            DropIndex("dbo.Afiliado", new[] { "UsuarioRegistro_EmpleadoID" });
            DropIndex("dbo.Afiliado", new[] { "UsuarioBienvenida_EmpleadoID" });
            DropTable("dbo.UserEstado");
            DropTable("dbo.Tarjeta");
            DropTable("dbo.TarjetaAdicional");
            DropTable("dbo.Reclamo");
            DropTable("dbo.Proveedor");
            DropTable("dbo.HabitacionReserva");
            DropTable("dbo.Reserva");
            DropTable("dbo.PagosReserva");
            DropTable("dbo.Pago");
            DropTable("dbo.TipoComprobante");
            DropTable("dbo.NCF");
            DropTable("dbo.Membresia");
            DropTable("dbo.Log");
            DropTable("dbo.Hotel");
            DropTable("dbo.Gasto");
            DropTable("dbo.Empresa");
            DropTable("dbo.Documentacion");
            DropTable("dbo.UserPermision");
            DropTable("dbo.Rol");
            DropTable("dbo.Login");
            DropTable("dbo.ControlDummy");
            DropTable("dbo.ComprobanteFiscal");
            DropTable("dbo.Banco");
            DropTable("dbo.Prospecto");
            DropTable("dbo.Archivo");
            DropTable("dbo.Empleado");
            DropTable("dbo.Telefono");
            DropTable("dbo.TarjetaUsuario");
            DropTable("dbo.EmailUsuario");
            DropTable("dbo.Direccion");
            DropTable("dbo.Afiliado");
        }
    }
}
