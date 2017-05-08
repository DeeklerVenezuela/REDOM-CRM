using RD.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RD.Business.Core
{
    public class Archivo
    {

        //public static byte[] CreateProspectosExcel(int id)
        //{
        //    byte[] result = null;
        //    //string ruta = "";
        //    List<Prospecto> Prospectos = new List<Prospecto>();
        //    using (var db = new RD.Data.DAL.DbContextRD())
        //    {
        //        Prospectos = db.Prospectos.Where(x => x.Archivo.ArchivoID == id).ToList();//Capturamos lista de prospectos del archivo

        //        Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        //        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        //        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        //        object misValue = System.Reflection.Missing.Value;
        //        xlWorkBook = xlApp.Workbooks.Add(misValue);
        //        xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
        //        //Encabezado del archivo
        //        xlWorkSheet.Cells[1, 1] = "Respuesta";
        //        xlWorkSheet.Cells[1, 2] = "Nombre";
        //        xlWorkSheet.Cells[1, 3] = "Primer Apellido";
        //        xlWorkSheet.Cells[1, 4] = "Segundo Apellido";
        //        xlWorkSheet.Cells[1, 5] = "Cédula";
        //        xlWorkSheet.Cells[1, 6] = "Dirección";
        //        xlWorkSheet.Cells[1, 7] = "Dirección 2";
        //        xlWorkSheet.Cells[1, 8] = "Teléfono de casa";
        //        xlWorkSheet.Cells[1, 9] = "Extensión";
        //        xlWorkSheet.Cells[1, 10] = "Teléfono móvil";
        //        xlWorkSheet.Cells[1, 11] = "Extensión";
        //        xlWorkSheet.Cells[1, 12] = "Teléfono de oficina";
        //        xlWorkSheet.Cells[1, 13] = "Extensión";
        //        xlWorkSheet.Cells[1, 14] = "Fax";
        //        xlWorkSheet.Cells[1, 15] = "Email";
        //        xlWorkSheet.Cells[1, 16] = "Fecha de nacimiento";
        //        //End Encabezado del archivo
        //        if (Prospectos != null)
        //        {


        //            //Cuerpo del archivo
        //            for (var i = 0; i < Prospectos.Count; i++)
        //            {
        //                var TCasa = "";
        //                var ECasa = "";
        //                var TOficina = "";
        //                var EOficina = "";
        //                var TMovil = "";
        //                var EMovil = "";
        //                var DPrincipal = "";
        //                var DSecundaria = "";
        //                if (Prospectos[i].Telefonos != null)
        //                {
        //                    foreach (var t in Prospectos[i].Telefonos)
        //                    {
        //                        if (t.Tipo == "CASA")
        //                        {
        //                            TCasa = t.Descripcion;
        //                            ECasa = t.Extension;
        //                        }

        //                        if (t.Tipo == "MOVIL")
        //                        {
        //                            TMovil = t.Descripcion;
        //                            EMovil = t.Extension;
        //                        }

        //                        if (t.Tipo == "OFICINA")
        //                        {
        //                            TOficina = t.Descripcion;
        //                            EOficina = t.Extension;
        //                        }


        //                    }
        //                }
        //                if (Prospectos[i].Direcciones != null)
        //                {
        //                    var CantD = Prospectos[i].Direcciones.Count;
        //                    if (CantD == 2)
        //                    {
        //                        DPrincipal = Prospectos[i].Direcciones[0].Descripcion;
        //                        DSecundaria = Prospectos[i].Direcciones[1].Descripcion;
        //                    }
        //                    else
        //                    {
        //                        DPrincipal = Prospectos[i].Direcciones[0].Descripcion;
        //                    }
        //                }
        //                for (var j = 1; j < 16; j++)
        //                {
        //                    xlWorkSheet.Cells[i + 2, 2] = string.IsNullOrEmpty(Prospectos[i].Nombres) ? "" : Prospectos[i].Nombres;
        //                    xlWorkSheet.Cells[i + 2, 3] = string.IsNullOrEmpty(Prospectos[i].Apellido1) ? "" : Prospectos[i].Apellido1;
        //                    xlWorkSheet.Cells[i + 2, 4] = string.IsNullOrEmpty(Prospectos[i].Apellido2) ? "" : Prospectos[i].Apellido2;
        //                    xlWorkSheet.Cells[i + 2, 5] = string.IsNullOrEmpty(Prospectos[i].Cedula) ? "" : Prospectos[i].Cedula;
        //                    xlWorkSheet.Cells[i + 2, 6] = DPrincipal;
        //                    xlWorkSheet.Cells[i + 2, 7] = DSecundaria;
        //                    xlWorkSheet.Cells[i + 2, 8] = TCasa;
        //                    xlWorkSheet.Cells[i + 2, 9] = ECasa;
        //                    xlWorkSheet.Cells[i + 2, 10] = TMovil;
        //                    xlWorkSheet.Cells[i + 2, 11] = EMovil;
        //                    xlWorkSheet.Cells[i + 2, 12] = TOficina;
        //                    xlWorkSheet.Cells[i + 2, 13] = EOficina;
        //                    xlWorkSheet.Cells[i + 2, 14] = string.IsNullOrEmpty(Prospectos[i].Fax) ? "" : Prospectos[i].Fax;
        //                    xlWorkSheet.Cells[i + 2, 15] = string.IsNullOrEmpty(Prospectos[i].Email) ? "" : Prospectos[i].Email;
        //                    xlWorkSheet.Cells[i + 2, 16] = string.IsNullOrEmpty(Prospectos[i].FechaDeNacimiento.ToString()) ? "" : Prospectos[i].FechaDeNacimiento.ToString();
        //                }
        //            }

        //            //End cuerpo del archivo
        //            var desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        //            //ruta = desktop + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".xls";
        //            string ruta = AppDomain.CurrentDomain.BaseDirectory + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + "_temp";
        //            xlWorkBook.SaveAs(ruta, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
        //            var tempPath = xlWorkBook.FullName;
        //            xlWorkBook.Close();
        //            xlApp.Quit();
        //            //xlWorkBook.Close(true, misValue, misValue);

        //            result = File.ReadAllBytes(tempPath);
        //            File.Delete(tempPath);
        //        }
        //    }
        //    return result;
        //}

    }
}
