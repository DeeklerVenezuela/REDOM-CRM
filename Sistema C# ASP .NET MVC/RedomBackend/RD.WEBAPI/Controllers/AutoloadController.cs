using OfficeOpenXml;
using RD.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RD.WEBAPI.Controllers
{
    public class AutoloadController : Controller
    {
        private RD.Data.DAL.DbContextRD db = new Data.DAL.DbContextRD();
        // GET: Autoload
        public ActionResult Index()
        {
            return RedirectToAction("Create");
        }

        // GET: Autoload/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Insertados = id;
            return View("Index");
        }


        // GET: Autoload/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Autoload/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "ArchivoID,Nombre,FechaHora,Eliminado,Status,Content")] Archivo archivo, HttpPostedFileBase upload)
        {


            try
            {
                if (ModelState.IsValid)
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        byte[] fileBytes = new byte[upload.ContentLength];
                        archivo.Content = fileBytes;
                        var data = upload.InputStream.Read(fileBytes, 0, Convert.ToInt32(upload.ContentLength));
                        var Afiliados = new List<Afiliado>();

                        using (var package = new ExcelPackage(upload.InputStream))
                        {
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;

                            if (noOfRow > 1)
                            {
                                var format = "dd/MM/yyyy";
                                CultureInfo provider = CultureInfo.InvariantCulture;
                                for (int rowIterator = 11; rowIterator <= noOfRow; rowIterator++)
                                {
                                    var Afiliado = new Afiliado();
                                    var IDex = ((workSheet.Cells[rowIterator, 1].Value != null) ? workSheet.Cells[rowIterator, 1].Value.ToString() : "");
                                    
                                        
                                    var FullName = ((workSheet.Cells[rowIterator, 3].Value != null) ? workSheet.Cells[rowIterator, 3].Value.ToString() : "");
                                    Afiliado.Cedula = ((workSheet.Cells[rowIterator, 4].Value != null) ? workSheet.Cells[rowIterator, 4].Value.ToString() : "");
                                    var exced = db.Afiliados.Where(x => x.Cedula == Afiliado.Cedula).FirstOrDefault();
                                    if(exced == null || exced.Cedula == "")
                                    {
                                        if (FullName != "")
                                        {

                                            IDex = IDex.Replace("-", "");
                                            IDex = IDex.Substring(8, 8);
                                            Afiliado.AfiliadoIndice = Int32.Parse(IDex);
                                            Afiliado.Observaciones = ((workSheet.Cells[rowIterator, 2].Value != null) ? workSheet.Cells[rowIterator, 2].Value.ToString() : "");
                                            var ConSpaces = 0;
                                            string FullExitName = "";
                                            foreach (char c in FullName)
                                            {
                                                if (c != ' ' && (ConSpaces == 0 || ConSpaces == 1))
                                                {
                                                    ConSpaces = 1;
                                                    FullExitName += c;
                                                }
                                                if (c == ' ')
                                                {
                                                    if (ConSpaces == 1)
                                                        ConSpaces = 2;

                                                    if (ConSpaces < 2)
                                                    {
                                                        ConSpaces = 1;
                                                        FullExitName += c;
                                                    }
                                                }
                                            }

                                            var Name = FullExitName.Substring(FullExitName.LastIndexOf(' ') + 1);
                                            var LastName = FullName.Replace(Name, " ");
                                            Afiliado.Nombres = ((Name != "") ? Name : "");
                                            Afiliado.Apellido1 = ((LastName != "") ? LastName : "");
                                            List<Direccion> Dir = new List<Direccion>();
                                            List<Telefono> Tel = new List<Telefono>();
                                            List<TarjetaUsuario> Tarjetas = new List<TarjetaUsuario>();
                                            Afiliado.Direcciones = Dir;
                                            Afiliado.Telefonos = Tel;
                                            Afiliado.Tarjetas = Tarjetas;
                                            Direccion Direccion = new Direccion();
                                            Telefono Telefono = new Data.Models.Telefono();
                                            Telefono Telefono2 = new Data.Models.Telefono();
                                            Telefono Telefono3 = new Data.Models.Telefono();
                                            TarjetaUsuario Tarjeta = new TarjetaUsuario();
                                            TarjetaUsuario Tarjeta2 = new TarjetaUsuario();
                                            if ((workSheet.Cells[rowIterator, 5].Value != null))
                                            {
                                                Direccion.Descripcion = workSheet.Cells[rowIterator, 5].Value.ToString();
                                                Direccion.Tipo = "";
                                                Afiliado.Direcciones.Add(Direccion);
                                            }

                                            if (workSheet.Cells[rowIterator, 6].Value != null)
                                            {

                                                Telefono.Descripcion = ((workSheet.Cells[rowIterator, 6].Value != null) ? workSheet.Cells[rowIterator, 6].Value.ToString() : "");
                                                Telefono.Tipo = "CASA";
                                                Telefono.Extension = "";
                                                Afiliado.Telefonos.Add(Telefono);
                                            }

                                            if (workSheet.Cells[rowIterator, 7].Value != null)
                                            {

                                                Telefono2.Descripcion = ((workSheet.Cells[rowIterator, 7].Value != null) ? workSheet.Cells[rowIterator, 7].Value.ToString() : "");
                                                Telefono2.Tipo = "OFICINA";
                                                Telefono2.Extension = "";
                                                Afiliado.Telefonos.Add(Telefono2);
                                            }

                                            if (workSheet.Cells[rowIterator, 8].Value != null)
                                            {

                                                Telefono3.Descripcion = ((workSheet.Cells[rowIterator, 8].Value != null) ? workSheet.Cells[rowIterator, 8].Value.ToString() : "");
                                                Telefono3.Tipo = "MOVIL";
                                                Telefono3.Extension = "";
                                                Afiliado.Telefonos.Add(Telefono3);
                                            }

                                            if (workSheet.Cells[rowIterator, 15].Value != null)
                                            {

                                                Tarjeta.Tipo = ((workSheet.Cells[rowIterator, 13].Value != null) ? workSheet.Cells[rowIterator, 13].Value.ToString() : "");
                                                Tarjeta.Banco = ((workSheet.Cells[rowIterator, 14].Value != null) ? workSheet.Cells[rowIterator, 14].Value.ToString() : "");
                                                Tarjeta.NumeroTarjeta = ((workSheet.Cells[rowIterator, 15].Value != null) ? workSheet.Cells[rowIterator, 15].Value.ToString() : "");
                                                
                                                
                                                string vencimiento = ((workSheet.Cells[rowIterator, 16].Value != null) ? workSheet.Cells[rowIterator, 16].Value.ToString() : "");
                                                if(!String.IsNullOrEmpty(vencimiento))
                                                {
                                                    string[] trimvencimiento = vencimiento.Split('/');
                                                    string yearvencimiento = "20" + trimvencimiento.Last();
                                                    vencimiento = trimvencimiento.First() + "/" + trimvencimiento.First() + "/" + yearvencimiento + " 10:00:00";
                                                    Tarjeta.FechaVencimiento = DateTime.Parse(vencimiento);
                                                }
                                                else
                                                    Tarjeta.FechaVencimiento = DateTime.Now;
                                                Afiliado.Tarjetas.Add(Tarjeta);
                                            }

                                            if (workSheet.Cells[rowIterator, 21].Value != null)
                                            {

                                                Tarjeta2.Tipo = ((workSheet.Cells[rowIterator, 19].Value != null) ? workSheet.Cells[rowIterator, 19].Value.ToString() : "");
                                                Tarjeta2.Banco = ((workSheet.Cells[rowIterator, 20].Value != null) ? workSheet.Cells[rowIterator, 20].Value.ToString() : "");
                                                Tarjeta2.NumeroTarjeta = ((workSheet.Cells[rowIterator, 21].Value != null) ? workSheet.Cells[rowIterator, 21].Value.ToString() : "");
                                                string vencimiento = ((workSheet.Cells[rowIterator, 22].Value != null) ? workSheet.Cells[rowIterator, 22].Value.ToString() : "");
                                                if (!String.IsNullOrEmpty(vencimiento))
                                                {
                                                    string[] trimvencimiento = vencimiento.Split('/');
                                                    string yearvencimiento = "20" + trimvencimiento.Last();
                                                    vencimiento = trimvencimiento.First() + "/" + trimvencimiento.First() + "/" + yearvencimiento + " 10:00:00";
                                                    Tarjeta2.FechaVencimiento = DateTime.Parse(vencimiento);
                                                }
                                                else
                                                    Tarjeta2.FechaVencimiento = DateTime.Now;
                                                Afiliado.Tarjetas.Add(Tarjeta2);
                                            }

                                            Afiliados.Add(Afiliado);
                                            Afiliado.Cumpleanio = ((workSheet.Cells[rowIterator, 12].Text != null) ? DateTime.Parse(workSheet.Cells[rowIterator, 12].Text.ToString()) : DateTime.Now);
                                            
                                            //Calcular el costo de la membresia
                                            var registro = ((workSheet.Cells[rowIterator, 36].Text != null) ? DateTime.ParseExact(workSheet.Cells[rowIterator, 36].Text.ToString(),format,provider) : DateTime.Now);
                                            
                                            var MontoAfiliacion = ((workSheet.Cells[rowIterator, 33].Value != null) ? workSheet.Cells[rowIterator, 33].Value.ToString() : "");
                                            MontoAfiliacion = MontoAfiliacion.Replace("RD$", "");
                                            MontoAfiliacion = MontoAfiliacion.Replace(",00", "");
                                            MontoAfiliacion = MontoAfiliacion.Replace(".", "");
                                            Afiliado.CostoMembresia = int.Parse(MontoAfiliacion);
                                            Afiliado.FechaHora = registro;    
                                            Afiliado.FechaVencimiento = registro.AddYears(1);

                                            db.Afiliados.Add(Afiliado);
                                            try
                                            {
                                                db.SaveChanges();
                                            }
                                            catch (DbEntityValidationException e)
                                            {
                                                foreach (var eve in e.EntityValidationErrors)
                                                {
                                                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                                                    foreach (var ve in eve.ValidationErrors)
                                                    {
                                                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                                            ve.PropertyName, ve.ErrorMessage);
                                                    }
                                                }
                                                throw;
                                            }

                                            //Manejamos cuando se vence la membresia
                                                MembresiaVence mv = new MembresiaVence
                                                {
                                                    AfiliadoM = Afiliado,
                                                    Called = false,
                                                    FechaInicio = registro,
                                                    FechaFin = registro.AddMonths(12)
                                                };
                                                db.MembresiaVences.Add(mv);
                                                db.SaveChanges();
                                            


                                        }
                                    }
                                    
                                }
                            }

                        }

                        return RedirectToAction("Details",new { id = Afiliados.Count});
                    }
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "No es posible guardar los cambios, intente de nuevo mas tarde y si el problema persiste contacte a su administrador del sistema.");
            }

            return View(archivo);
        }


    }
}
