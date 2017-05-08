using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RD.Business.Core
{
    public class Afiliados
    {
        private static RD.Data.DAL.DbContextRD ctx = new RD.Data.DAL.DbContextRD();
        //Agregar nuevo afiliado
        public static void AddAfiliado(
            RD.Data.Models.Afiliado Afiliado,
            List<RD.Data.Models.Direccion> Direcciones,
            List<RD.Data.Models.TarjetaUsuario> Tarjetas,
            List<RD.Data.Models.Telefono> Telefonos,
            List<RD.Data.Models.EmailUsuario> Emails,
            List<RD.Data.Models.TarjetaAdicional> TarjetasAdicionales,
            int auxE)
        {
            //Asignar AfiliadoIndice o cod. de afiliado si no existe
            if (Afiliado.AfiliadoIndice == null || Afiliado.AfiliadoIndice == 0)
            {
                var lastIndex = ctx.Afiliados.OrderByDescending(e => e.AfiliadoID).Select(y => y.AfiliadoIndice).FirstOrDefault();
                if (lastIndex != null)
                {
                    if (lastIndex >= 11119000)
                        Afiliado.AfiliadoIndice = lastIndex + 1;
                    else
                        Afiliado.AfiliadoIndice = 11119000;
                }
            }

            //Asignar usuario de bienvenida
            var userBienv = ctx.Empleados.Where(x=>x.EmpleadoID == Afiliado.UsuarioBienvenida.EmpleadoID).FirstOrDefault();
            Afiliado.UsuarioBienvenida = userBienv;
            var userReg = ctx.Empleados.Where(x => x.EmpleadoID == auxE).FirstOrDefault();
            Afiliado.UsuarioRegistro = userReg;
            //Asignar usuario de registro
            //Afiliado.UsuarioRegistro = userBienv;

            //Asignar telefonos al usuario
            Afiliado.Telefonos = new List<Data.Models.Telefono>();
            foreach (var i in Telefonos)
            {
                if (!String.IsNullOrEmpty(i.Descripcion))
                {
                    //i.Descripcion = Repository.Hash.CalcularHash(i.Descripcion);//Protegemos los numeros de telefono a traves de hash
                    Afiliado.Telefonos.Add(i);
                }
            }

            //Asignar Tarjetas Adicionales
            Afiliado.TarjetasAdicionales = new List<Data.Models.TarjetaAdicional>();
            
                if(TarjetasAdicionales != null)
                {
                    foreach (var tarAd in TarjetasAdicionales)
                    {
                        if (!String.IsNullOrEmpty(tarAd.Cedula))
                        {
                            //i.Descripcion = Repository.Hash.CalcularHash(i.Descripcion);//Protegemos los numeros de telefono a traves de hash
                            Afiliado.TarjetasAdicionales.Add(tarAd);
                        }
                    }
                }
            
            Afiliado.Emails = new List<Data.Models.EmailUsuario>();

            foreach (var e in Emails)
            {
                if (!String.IsNullOrEmpty(e.Email))
                {
                    Afiliado.Emails.Add(e);
                }
            }

            Afiliado.Tarjetas = new List<Data.Models.TarjetaUsuario>();
            //Asignar tarjetas al usuario
            foreach (var j in Tarjetas)
            {
                if (!String.IsNullOrEmpty(j.NumeroTarjeta))
                {
                    //j.NumeroTarjeta = Repository.Hash.CalcularHash(j.NumeroTarjeta);//Protegemos el num de tarjeta a traves de hash
                    Afiliado.Tarjetas.Add(j);
                }
            }

            //Asignar Direcciones a usuario
            Afiliado.Direcciones = new List<Data.Models.Direccion>();
            foreach (var k in Direcciones)
            {
                if (!String.IsNullOrEmpty(k.Descripcion))
                {
                    //k.Descripcion = Repository.Hash.CalcularHash(k.Descripcion);//Protegemos las direcciones a traves de hash
                    Afiliado.Direcciones.Add(k);
                }
            }

            var membresia = ctx.Membresias.Where(x => x.MembresiaID == Afiliado.CostoMembresia).FirstOrDefault();
            var now = DateTime.Now;
            if (membresia != null)
            {
                Afiliado.FechaVencimiento = now.AddMonths(membresia.Duracion);
                Afiliado.CostoMembresia = membresia.Costo;
            }

            Afiliado.FechaHora = DateTime.Now;
            //En este punto  el modelo debe estar completo listo para salvar
            ctx.Afiliados.Add(Afiliado);
            
            //Cambiar el estado al prospecto
            var ced = Afiliado.Cedula.Replace(" ","");
            ced = ced.Replace("-", "");
            var p = ctx.Prospectos.Where(x => x.Cedula == ced).FirstOrDefault();
            if (p != null)
            {
                p.Afiliado = true;
            }
            

            ctx.SaveChanges();

           

        }

        public static List<RD.Data.Models.UserEstado> GetEstados()
        {
            List<RD.Data.Models.UserEstado> Estados = new List<Data.Models.UserEstado>();
            Estados = ctx.UserEstados.ToList();
            return Estados;
        }

    }
}
