using OfficeOpenXml;
using RD.WEBAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using System.IO.Compression;
using Ionic.Zip;
namespace RD.WEBAPI.Controllers
{
    public class ControllerHelpers
    {
        static HttpSessionState Session
        {
            get
            {
                if (HttpContext.Current == null)
                    throw new ApplicationException("No existe Http Context!");

                return HttpContext.Current.Session;
            }
        }

        public static T Get<T>(string key)
        {
            if (Session[key] == null)
                return default(T);
            else
                return (T)Session[key];
        }

        public static void Set<T>(string key, T value)
        {
            Session[key] = value;
        }

        public static string GetString(string key)
        {
            string s = Get<string>(key);
            return s == null ? string.Empty : s;
        }

        public static void SetString(string key, string value)
        {
            Set<string>(key, value);
        }
        //Method GenerateExcel: Carnet 
        internal static Task GenerateXLS(string tipo,List<Data.Models.Documentacion> datasource, string filePath)
        {
            return Task.Run(() =>
            {
                using (ExcelPackage pck = new ExcelPackage())
                {

                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Carnet");
                    ws.Cells[1, 1].Value = "NUMERO DEL SOCIO";
                    ws.Cells[1, 2].Value = "NOMBRE";
                    ws.Cells[1, 3].Value = "DIRECCION";
                    ws.Cells[1, 4].Value = "TEL. 1";
                    ws.Cells[1, 5].Value = "TEL. 2";
                    ws.Cells[1, 6].Value = "TEL. 3";
                    if (tipo == "0")
                    {
                        ws.Cells[1, 7].Value = "EXPIRACION";
                    }

                    if (datasource != null)
                    {
                        for (int i = 0; i < datasource.Count(); i++)
                        {
                            var TCasa = "";
                            var ECasa = "";
                            var TOficina = "";
                            var EOficina = "";
                            var TMovil = "";
                            var EMovil = "";
                            var DPrincipal = "";
                            //var DSecundaria = "";
                            if (datasource[i].Afiliado.Telefonos != null)
                            {
                                foreach (var t in datasource[i].Afiliado.Telefonos)
                                {
                                    if (t.Tipo == "CASA")
                                    {
                                        TCasa = t.Descripcion;
                                        ECasa = t.Extension;
                                    }

                                    if (t.Tipo == "MOVIL")
                                    {
                                        TMovil = t.Descripcion;
                                        EMovil = t.Extension;
                                    }

                                    if (t.Tipo == "OFICINA")
                                    {
                                        TOficina = t.Descripcion;
                                        EOficina = t.Extension;
                                    }


                                }
                            }
                            if (datasource[i].Afiliado.Direcciones != null)
                            {
                                //var CantD = datasource[i].Afiliado.Direcciones.Count;
                                //if (CantD == 2)
                                //{
                                    DPrincipal = datasource[i].Afiliado.Direcciones[0].Descripcion;
                                //    DSecundaria = datasource[i].Direcciones[1].Descripcion;
                                //}
                                //else
                                //{
                                //    DPrincipal = datasource[i].Direcciones[0].Descripcion;
                                //}
                            }
                            string Nom = datasource[i].Afiliado.Nombres, ape= datasource[i].Afiliado.Apellido1;
                            string NombreG = Nom + " " + ape;
                            var idAfi = datasource[i].Afiliado.AfiliadoIndice.ToString();
                            string AfiIdF="";
                             if (idAfi.Length > 4)
                            {
                                AfiIdF = "7777-2552-" + idAfi.Substring(1, 4) + "-" + idAfi.Substring(4, 4);
                            }
                             else
                             {
                                 AfiIdF = idAfi;
                             }
                            ws.Cells[i + 2, 1].Value = AfiIdF;
                            ws.Cells[i + 2, 2].Value = NombreG;//string.IsNullOrEmpty(datasource[i].Afiliado.Nombres) ? "" : datasource[i].Afiliado.Nombres;
                           // ws.Cells[i + 2, 3].Value = string.IsNullOrEmpty(datasource[i].Apellido1) ? "" : datasource[i].Apellido1;
                           // ws.Cells[i + 2, 4].Value = string.IsNullOrEmpty(datasource[i].Apellido2) ? "" : datasource[i].Apellido2;
                           // ws.Cells[i + 2, 5].Value = string.IsNullOrEmpty(datasource[i].Cedula) ? "" : datasource[i].Cedula;
                            ws.Cells[i + 2, 3].Value = DPrincipal;
                           // ws.Cells[i + 2, 7].Value = DSecundaria;
                            ws.Cells[i + 2, 4].Value = ECasa+" "+TCasa;
                            ws.Cells[i + 2, 5].Value = EMovil+" "+TMovil;
                            ws.Cells[i + 2, 6].Value = EOficina+" "+TOficina;
                            if (tipo == "0")
                            {                            
                                ws.Cells[i + 2, 7].Value = datasource[i].Afiliado.FechaVencimiento.Day.ToString()+"-"+datasource[i].Afiliado.FechaVencimiento.Month.ToString()+"-"+datasource[i].Afiliado.FechaVencimiento.Year.ToString();
                            }
                        }
                    }

                    pck.SaveAs(new FileInfo(filePath));
                }
            });
        }
        internal static Task GenerateXLS(List<Data.Models.Prospecto> datasource, string filePath, int id)
        {
            return Task.Run(() =>
            {
                using (ExcelPackage pck = new ExcelPackage())
                {

                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Prospectos");
                    ws.Cells[1, 1].Value = "Respuesta";
                    ws.Cells[1, 2].Value = "Nombre";
                    ws.Cells[1, 3].Value = "Primer Apellido";
                    ws.Cells[1, 4].Value = "Segundo Apellido";
                    ws.Cells[1, 5].Value = "Cédula";
                    ws.Cells[1, 6].Value = "Dirección";
                    ws.Cells[1, 7].Value = "Dirección 2";
                    ws.Cells[1, 8].Value = "Teléfono de casa";
                    ws.Cells[1, 9].Value = "Extensión";
                    ws.Cells[1, 10].Value = "Teléfono móvil";
                    ws.Cells[1, 11].Value = "Extensión";
                    ws.Cells[1, 12].Value = "Teléfono de oficina";
                    ws.Cells[1, 13].Value = "Extensión";
                    ws.Cells[1, 14].Value = "Fax";
                    ws.Cells[1, 15].Value = "Email";
                    ws.Cells[1, 16].Value = "Fecha de nacimiento";

                    if (datasource != null)
                    {
                        for (int i = 0; i < datasource.Count(); i++)
                        {
                            var TCasa = "";
                            var ECasa = "";
                            var TOficina = "";
                            var EOficina = "";
                            var TMovil = "";
                            var EMovil = "";
                            var DPrincipal = "";
                            var DSecundaria = "";
                            if (datasource[i].Telefonos != null)
                            {
                                foreach (var t in datasource[i].Telefonos)
                                {
                                    if (t.Tipo == "CASA")
                                    {
                                        TCasa = t.Descripcion;
                                        ECasa = t.Extension;
                                    }

                                    if (t.Tipo == "MOVIL")
                                    {
                                        TMovil = t.Descripcion;
                                        EMovil = t.Extension;
                                    }

                                    if (t.Tipo == "OFICINA")
                                    {
                                        TOficina = t.Descripcion;
                                        EOficina = t.Extension;
                                    }


                                }
                            }
                            if (datasource[i].Direcciones != null)
                            {
                                var CantD = datasource[i].Direcciones.Count;
                                if (CantD == 2)
                                {
                                    DPrincipal = datasource[i].Direcciones[0].Descripcion;
                                    DSecundaria = datasource[i].Direcciones[1].Descripcion;
                                }
                                else
                                {
                                    DPrincipal = datasource[i].Direcciones[0].Descripcion;
                                }
                            }
                            ws.Cells[i + 2, 1].Value = string.IsNullOrEmpty(datasource[i].Respuesta) ? "" : datasource[i].Respuesta; ;
                            ws.Cells[i + 2, 2].Value = string.IsNullOrEmpty(datasource[i].Nombres) ? "" : datasource[i].Nombres;
                            ws.Cells[i + 2, 3].Value = string.IsNullOrEmpty(datasource[i].Apellido1) ? "" : datasource[i].Apellido1;
                            ws.Cells[i + 2, 4].Value = string.IsNullOrEmpty(datasource[i].Apellido2) ? "" : datasource[i].Apellido2;
                            ws.Cells[i + 2, 5].Value = string.IsNullOrEmpty(datasource[i].Cedula) ? "" : datasource[i].Cedula;
                            ws.Cells[i + 2, 6].Value = DPrincipal;
                            ws.Cells[i + 2, 7].Value = DSecundaria;
                            ws.Cells[i + 2, 8].Value = TCasa;
                            ws.Cells[i + 2, 9].Value = ECasa;
                            ws.Cells[i + 2, 10].Value = TMovil;
                            ws.Cells[i + 2, 11].Value = EMovil;
                            ws.Cells[i + 2, 12].Value = TOficina;
                            ws.Cells[i + 2, 13].Value = EOficina;
                            ws.Cells[i + 2, 14].Value = string.IsNullOrEmpty(datasource[i].Fax) ? "" : datasource[i].Fax;
                            ws.Cells[i + 2, 15].Value = string.IsNullOrEmpty(datasource[i].Email) ? "" : datasource[i].Email;
                            ws.Cells[i + 2, 16].Value = string.IsNullOrEmpty(datasource[i].FechaDeNacimiento.ToString()) ? "" : datasource[i].FechaDeNacimiento.ToString();
  

                        }
                    }

                    pck.SaveAs(new FileInfo(filePath));
                }
            }); 
        }
       
        public static bool GetPermiso(string controller, int method)
        {
            var user = GetString("login");
            bool result = false;
            if (!String.IsNullOrEmpty(user))
            {
                if(user == "ADMIN")
                    return true;
            }
            var mainPermision = GetString(controller);            
            if (mainPermision != null)
            {
                var per = mainPermision[method];
                if (  per == '1') result = true;
            }
            return result;
        }

        public static Task GenerarNomina(string filePath, List<Nomina> NominaE)
        {
            return Task.Run(() =>
            {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    float TotalMontoVentasConfirmadas = 0, TotalCobrarTotal = 0;
                    int TotalVentasConfirmadas =0, aux = 1;
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("NÓMINA");
                    ws.Cells[1, 1].Value = "NOMBRE";
                    ws.Cells[1, 2].Value = "CEDULA";
                    ws.Cells[1, 3].Value = "VENTAS CONFIRMADAS";
                    ws.Cells[1, 4].Value = "VENTAS DEL PUNTO";
                    ws.Cells[1, 5].Value = "TOTAL DE VENTAS";
                    ws.Cells[1, 6].Value = "MONTOS DE LAS VENTAS CONFIRMADAS";
                    ws.Cells[1, 7].Value = "MONTOS DE LAS VENTAS DEL PUNTO";
                    ws.Cells[1, 8].Value = "MONTOS DEL TURNO DE LA MAÑANA";
                    ws.Cells[1, 9].Value = "MONTOS DEL TURNO DE LA TARDE"; 
                    ws.Cells[1, 10].Value = "BONOS";
                    ws.Cells[1, 11].Value = "SUB-TOTAL";
                    ws.Cells[1, 12].Value = "DESCUENTO DE SEGURO";
                    ws.Cells[1, 13].Value = "TOTAL A COBRAR";
                    ws.Cells[1, 14].Value = "FIRMA";
                    

                    if (NominaE != null)
                    {
                        for (int i = 0; i < NominaE.Count(); i++)
                        {
                            ws.Cells[i + 2, 1].Value = NominaE[i].nombre;
                            ws.Cells[i + 2, 2].Value = NominaE[i].cedula;
                            ws.Cells[i + 2, 3].Value = NominaE[i].ventasConfirmadas;
                            ws.Cells[i + 2, 4].Value = "---";
                            ws.Cells[i + 2, 5].Value = "---";
                            ws.Cells[i + 2, 6].Value = NominaE[i].MontoVentasConfirmadas;
                            ws.Cells[i + 2, 7].Value = "----";
                            ws.Cells[i + 2, 8].Value = "----";
                            ws.Cells[i + 2, 9].Value = "----";
                            ws.Cells[i + 2, 10].Value = "----";
                            ws.Cells[i + 2, 11].Value = "----";
                            ws.Cells[i + 2, 12].Value = "----";
                            ws.Cells[i + 2, 13].Value = NominaE[i].totalACobrar;
                            ws.Cells[i + 2, 14].Value = "_______________________";
                            aux = i+2;
                            TotalVentasConfirmadas += NominaE[i].ventasConfirmadas;
                            TotalMontoVentasConfirmadas += NominaE[i].MontoVentasConfirmadas;
                            TotalCobrarTotal += NominaE[i].totalACobrar;
                        }
                        ws.Cells[aux + 2, 1].Value = "TOTAL";
                        ws.Cells[aux + 2, 2].Value = "    ";
                        ws.Cells[aux + 2, 3].Value = TotalVentasConfirmadas;
                        ws.Cells[aux + 2, 4].Value = "    ";
                        ws.Cells[aux + 2, 5].Value = "    ";
                        ws.Cells[aux + 2, 6].Value = TotalMontoVentasConfirmadas;
                        ws.Cells[aux + 2, 7].Value = "    ";
                        ws.Cells[aux + 2, 8].Value = "    ";
                        ws.Cells[aux + 2, 9].Value = "    ";
                        ws.Cells[aux + 2, 10].Value = "    ";
                        ws.Cells[aux + 2, 11].Value = "     ";
                        ws.Cells[aux + 2, 12].Value = "     ";
                        ws.Cells[aux + 2, 13].Value = TotalCobrarTotal;
                    }

                    pck.SaveAs(new FileInfo(filePath));
                }
            });
        }

        public static Task Generar607(List<pago> ListP, string Path)
        {
            return Task.Run(() => {
                string first = "607" + "  " + "CED_PER";
                List<string> itemL = new List<string>();
                String line = "";
                for (int i = 0; i < ListP.Count(); i++)
                {

                    line = line + "\n\r" +"  "+ ListP[i].rncedula + "1" + ListP[i].ncomprobantef + "                   " + ListP[i].itbisFacturado + ListP[i].montofacturado + "\n\r";
                    itemL.Add(line);
                }
                string txtfinal = first + "\n\r" + line;
                System.IO.File.WriteAllText(Path, txtfinal);
                //string startPath = Path;
                //string zipPath = Path+".zip";
                ////ZipFile.CreateFromDirectory(startPath, zipPath);
                //using (ZipFile zip = new ZipFile())
                //{
                //    zip.AddFile(Path);
                //    zip.Save(zipPath);
                //    }
            });
        }
               
        
    }
}
