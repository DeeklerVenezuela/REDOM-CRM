using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RD.Data.Models;

namespace RD.Business.Core
{
    public class VoucherGenerator
    {
        private static RD.Data.DAL.DbContextRD db = new Data.DAL.DbContextRD();

        public static Voucher MainGenerator(TipoComprobante Tipo)
        {
            Voucher Voucher = new Data.Models.Voucher();
            Voucher.Result = false;
            var Ncf = db.NCFS.FirstOrDefault();
            if (Ncf == null)
            {
                Voucher.Mensaje = "NO SE PUEDE GENERAR UN COMPROBANTE PORQUE NO SE HAN ESTABLECIDOS LOS PARÁMETROS DE INFORMACIÓN FISCAL EN EL MÓDULO NCF";
                Voucher.Secuencia = "";
                return Voucher;
            }
            else
            {
                if (ValidatorNCF(Ncf) != false)
                {
                    ComprobanteFiscal CP = new ComprobanteFiscal();
                    CP.FechaCreacion = DateTime.Now;
                    CP.Tipo = Tipo.TipoComprobanteID;
                    if(int.Parse(Ncf.NumeradorFinal) - int.Parse(Ncf.NumeradorActual) <= Ncf.IndicadorDeFinal)
                    {
                        Voucher.Mensaje = "MENSAJE DEL SISTEMA! SOLO SE PUEDEN GENERAR "+ (int.Parse(Ncf.NumeradorFinal) - int.Parse(Ncf.NumeradorActual)).ToString() + " COMPROBANTES FISCALES MÁS.";
                        Voucher.Secuencia = GetSecuencia(Ncf);
                        Voucher.Result = true;
                        CP.Secuencia = Voucher.Secuencia;
                        var CF = SaveComprobante(CP);
                        Voucher.Comprobante = CF;
                        return Voucher;
                    }
                    else
                    {
                        Voucher.Mensaje = "COMPROBANTE GENERADO";
                        Voucher.Secuencia = GetSecuencia(Ncf);
                        Voucher.Result = true;
                        CP.Secuencia = Voucher.Secuencia;
                        var CF = SaveComprobante(CP);
                        Voucher.Comprobante = CF;
                        return Voucher;
                    }     
                }
                Voucher.Mensaje = "MENSAJE DEL SISTEMA! NO SE PUEDEN GENERAR COMPROBANTES SECUENCIA TERMINADA.";
                Voucher.Secuencia = "";
                Voucher.Comprobante = 0;
                return Voucher;
            }

        }

        private static int SaveComprobante(ComprobanteFiscal CP)
        {
            db.Comprobantes.Add(CP);
            db.SaveChanges();
            return db.Comprobantes.Where(x=>x.Secuencia == CP.Secuencia).Select(y=>y.ComprbanteFiscalID).FirstOrDefault();
        }

        private static string GetSecuencia(NCF Ncf)
        {
                var Sec = int.Parse(Ncf.NumeradorActual) + 1;
                Ncf.NumeradorActual = Sec.ToString();
                return Sec.ToString(); 
        }

        private static bool ValidatorNCF(NCF Ncf)
        {
            if (int.Parse(Ncf.NumeradorFinal) - int.Parse(Ncf.NumeradorActual) < 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
