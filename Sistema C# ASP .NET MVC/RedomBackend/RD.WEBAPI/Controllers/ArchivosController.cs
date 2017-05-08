using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RD.Data.DAL;
using RD.Data.Models;
using System.Data.Entity.Infrastructure;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.IO;
using System.Net.Http.Headers;
using RD.WEBAPI.Models;
using System.Collections;


namespace RD.WEBAPI.Controllers
{
    public class ArchivosController : Controller
    {
        private DbContextRD db = new DbContextRD();

        // GET: Archivos
        public async Task<ActionResult> Index()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso(this.ControllerContext.RouteData.Values["controller"].ToString(), 1) == false)
                return RedirectToAction("Error", "Home");

            return View(await db.Archivos.ToListAsync());
        }

        // GET: Archivos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso(this.ControllerContext.RouteData.Values["controller"].ToString(), 1) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archivo archivo = await db.Archivos.FindAsync(id);
            if (archivo == null)
            {
                return HttpNotFound();
            }
            return View(archivo);
        }

        // GET: Archivos/Create
        public ActionResult Create()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso(this.ControllerContext.RouteData.Values["controller"].ToString(), 0) == false)
                return RedirectToAction("Error", "Home");
            return View();
        }

        // POST: Archivos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArchivoID,Nombre,FechaHora,Eliminado,Status,Content")] Archivo archivo, HttpPostedFileBase upload)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso(this.ControllerContext.RouteData.Values["controller"].ToString(), 0) == false)
                return RedirectToAction("Error", "Home");

            try
            {
                if (ModelState.IsValid)
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        byte[] fileBytes = new byte[upload.ContentLength];
                        archivo.Content = fileBytes;
                        var data = upload.InputStream.Read(fileBytes, 0, Convert.ToInt32(upload.ContentLength));
                        var Prospectos = new List<Prospecto>();

                        using (var package = new ExcelPackage(upload.InputStream))
                        {
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;

                            if (noOfRow > 1)
                            {

                                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                                {
                                    var Prospecto = new Prospecto();

                                    Prospecto.Comentario = ((workSheet.Cells[rowIterator, 1].Value != null) ? workSheet.Cells[rowIterator, 1].Value.ToString() : "");
                                    Prospecto.Nombres = ((workSheet.Cells[rowIterator, 2].Value != null) ? workSheet.Cells[rowIterator, 2].Value.ToString() : "");
                                    Prospecto.Apellido1 = ((workSheet.Cells[rowIterator, 3].Value != null) ? workSheet.Cells[rowIterator, 3].Value.ToString() : "");
                                    Prospecto.Apellido2 = ((workSheet.Cells[rowIterator, 4].Value != null) ? workSheet.Cells[rowIterator, 4].Value.ToString() : "");
                                    Prospecto.Cedula = ((workSheet.Cells[rowIterator, 5].Value != null) ? workSheet.Cells[rowIterator, 5].Value.ToString() : "");
                                    List<Direccion> Dir = new List<Direccion>();
                                    List<Telefono> Tel = new List<Telefono>();
                                    Prospecto.Direcciones = Dir;
                                    Prospecto.Telefonos = Tel;
                                    if ((workSheet.Cells[rowIterator, 6].Value != null))
                                    {
                                        Direccion Direccion = new Direccion();
                                        Direccion.Descripcion = ((workSheet.Cells[rowIterator, 6].Value != null) ? workSheet.Cells[rowIterator, 6].Value.ToString() : "");
                                        Direccion.Tipo = "";
                                        Prospecto.Direcciones.Add(Direccion);
                                    }

                                    if (workSheet.Cells[rowIterator, 7].Value != null)
                                    {
                                        Direccion Direccion = new Direccion();
                                        Direccion.Descripcion = ((workSheet.Cells[rowIterator, 7].Value != null) ? workSheet.Cells[rowIterator, 7].Value.ToString() : "");
                                        Direccion.Tipo = "";
                                        Prospecto.Direcciones.Add(Direccion);
                                    }

                                    if (workSheet.Cells[rowIterator, 8].Value != null)
                                    {
                                        Telefono Telefono = new Data.Models.Telefono();
                                        Telefono.Descripcion = ((workSheet.Cells[rowIterator, 8].Value != null) ? workSheet.Cells[rowIterator, 8].Value.ToString() : "");
                                        Telefono.Tipo = "CASA";
                                        Telefono.Extension = ((workSheet.Cells[rowIterator, 9].Value != null) ? workSheet.Cells[rowIterator, 9].Value.ToString() : "");
                                        Prospecto.Telefonos.Add(Telefono);
                                    }

                                    if (workSheet.Cells[rowIterator, 10].Value != null)
                                    {
                                        Telefono Telefono = new Data.Models.Telefono();
                                        Telefono.Descripcion = ((workSheet.Cells[rowIterator, 10].Value == null) ? "" : workSheet.Cells[rowIterator, 10].Value.ToString());
                                        Telefono.Tipo = "MOVIL";
                                        Telefono.Extension = ((workSheet.Cells[rowIterator, 11].Value == null) ? "" : workSheet.Cells[rowIterator, 11].Value.ToString());
                                        Prospecto.Telefonos.Add(Telefono);
                                    }

                                    if (workSheet.Cells[rowIterator, 12].Value != null)
                                    {
                                        Telefono Telefono = new Data.Models.Telefono();
                                        Telefono.Descripcion = ((workSheet.Cells[rowIterator, 12].Value == null) ? "" : workSheet.Cells[rowIterator, 12].Value.ToString());
                                        Telefono.Tipo = "OFICINA";
                                        Telefono.Extension = ((workSheet.Cells[rowIterator, 13].Value == null) ? "" : workSheet.Cells[rowIterator, 13].Value.ToString());
                                        Prospecto.Telefonos.Add(Telefono);
                                    }

                                    Prospecto.Fax = ((workSheet.Cells[rowIterator, 14].Value == null) ? "" : workSheet.Cells[rowIterator, 14].Value.ToString());
                                    Prospecto.Email = ((workSheet.Cells[rowIterator, 15].Value == null) ? "" : workSheet.Cells[rowIterator, 15].Value.ToString());

                                    if ((workSheet.Cells[rowIterator, 16].Value != null))
                                    {
                                        var dob = workSheet.Cells[rowIterator, 16].Value.ToString();
                                        Prospecto.FechaDeNacimiento = DateTime.Parse(dob);
                                    }
                                    else
                                    {
                                        Prospecto.FechaDeNacimiento = DateTime.Now.AddYears(-20);
                                    }
                                    Prospecto.Activo = archivo.Status;

                                    var usu = int.Parse(ControllerHelpers.GetString("USER"));
                                    Prospecto.Empleado = db.Logins.Where(x => x.LoginID == usu).Select(y => y.Empleado).FirstOrDefault();
                                    Prospecto.FechaHora = DateTime.Now;
                                    Prospecto.Ocupado = false;
                                    Prospecto.Afiliado = false;
                                    Prospectos.Add(Prospecto);
                                }

                                if (Prospectos != null)
                                {
                                    archivo.Prospectos = new List<Prospecto>();
                                    foreach (var i in Prospectos)
                                    {
                                        archivo.Prospectos.Add(i);
                                    }

                                }
                                archivo.FechaHora = DateTime.Now;
                                db.Archivos.Add(archivo);
                                db.SaveChanges();
                            }
                            
                        }
                        
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "No es posible guardar los cambios, intente de nuevo mas tarde y si el problema persiste contacte a su administrador del sistema.");
            }

            return View(archivo);
        }

        // GET: Archivos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso(this.ControllerContext.RouteData.Values["controller"].ToString(), 2) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archivo archivo = await db.Archivos.FindAsync(id);
            if (archivo == null)
            {
                return HttpNotFound();
            }
            return View(archivo);
        }

        // POST: Archivos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ArchivoID,Nombre,FechaHora,Eliminado,Status,Content")] Archivo archivo)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso(this.ControllerContext.RouteData.Values["controller"].ToString(), 2) == false)
                return RedirectToAction("Error", "Home");

            if (ModelState.IsValid)
            {
                db.Entry(archivo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(archivo);
        }

        // GET: Archivos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso(this.ControllerContext.RouteData.Values["controller"].ToString(), 3) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archivo archivo = await db.Archivos.FindAsync(id);
            if (archivo == null)
            {
                return HttpNotFound();
            }
            return View(archivo);
        }

        // POST: Archivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso(this.ControllerContext.RouteData.Values["controller"].ToString(), 3) == false)
                return RedirectToAction("Error", "Home");

            Archivo archivo = await db.Archivos.FindAsync(id);

            List<Prospecto> prospectos = new List<Prospecto>();

            List<Direccion> Direcciones = new List<Direccion>();
            List<Telefono> Telefonos = new List<Telefono>();

            foreach (var g in archivo.Prospectos)
                prospectos.Add(g);

            foreach (var prospecto in prospectos)
            {
                if (prospecto.Direcciones != null)
                {
                    for (var i = 0; i < prospecto.Direcciones.Count; i++ )
                        prospecto.Direcciones.RemoveAt(i);
                }

                if (prospecto.Telefonos != null)
                {
                    for (var i = 0; i < prospecto.Telefonos.Count; i++)
                        prospecto.Telefonos.RemoveAt(i);
                }

                var cd = db.ControlDummies.Where(x => x.Prospecto.ProspectoID == prospecto.ProspectoID).FirstOrDefault();//Eliminar control de dummy si trabajé al prospecto
                if(cd != null)
                    db.ControlDummies.Remove(cd);

                db.Prospectos.Remove(prospecto);
                Direcciones = null;
                Telefonos = null;
            }

            db.Archivos.Remove(archivo);

            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult DeshabilitarArchivo(int id, bool act)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso(this.ControllerContext.RouteData.Values["controller"].ToString(), 2) == false)
                return RedirectToAction("Error", "Home");

            Archivo archivo = new Archivo();
            archivo = db.Archivos.Where(x => x.ArchivoID == id).Include(y => y.Prospectos).FirstOrDefault();
            if (archivo != null)
            {
                archivo.Status = act;
                if (archivo.Prospectos.Count > 0)
                {
                    foreach (var i in archivo.Prospectos)
                    {
                        i.Activo = act;
                    }
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<FileResult> GetXLSReport(int id)
        {

            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\";
            string filename = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".xlsx";
            string filePath = Path.Combine(path, filename);

            List<Prospecto> prospectsList = new List<Prospecto>();
            prospectsList = db.Prospectos.Where(x => x.Archivo.ArchivoID == id).ToList();

            await RD.WEBAPI.Controllers.ControllerHelpers.GenerateXLS(prospectsList, filePath, id);

            byte[] resultado = null;
            resultado = System.IO.File.ReadAllBytes(filePath);
            System.IO.File.Delete(filePath);
            string name = db.Archivos.Where(x=>x.ArchivoID == id).Select(y=>y.Nombre).FirstOrDefault();
            return File(resultado, "application/vnd.ms-excel", name + "_descarga.xlsx");
        }
        //Generar Excel documenacion
        public async Task<JsonResult> GetXLSReport(List<int> ListEx , string tip)
        {
            string tipo="";
            if (tip == "0")
            {
                tipo = "Carnets-";
            }else if(tip=="1"){
                tipo = "Labels-";
            }
            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\";
            string filename = tipo+DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".xlsx";
            string filePath = Path.Combine(path, filename);
            string resp = "http://" + this.HttpContext.Request.Url.Host +":"+this.HttpContext.Request.Url.Port + "/Images/" + filename;
            List<Documentacion> ListdocList = new List<Documentacion>();
            foreach (var itemL in ListEx) {
                var d = db.Documentaciones.Where(x => x.DocumentacionID == itemL).FirstOrDefault();
                ListdocList.Add(d);
                if (tip == "0")
                    d.Carnet = true;
                if (tip == "1")
                    d.LabelSobres = true;
                db.Entry(d).State = EntityState.Modified;
            }
            db.SaveChanges();
            

            await RD.WEBAPI.Controllers.ControllerHelpers.GenerateXLS(tip,ListdocList, filePath);

            //byte[] resultado = null;
            //resultado = System.IO.File.ReadAllBytes(filePath);
            ////System.IO.File.Delete(filePath);
            //return File(resultado, "application/vnd.ms-excel", "Documentacion_descarga.xlsx");
            List<string> res = new List<string>();
            res.Add(resp);
            res.Add(filePath);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GenerarNomina(string tip)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\";
            string filename = "Nomina.xlsx";
            string filePath = Path.Combine(path, filename);
            string resp = "http://" + this.HttpContext.Request.Url.Host + ":" + this.HttpContext.Request.Url.Port + "/Images/" + filename;
            List<Nomina> empleadosNomina = new List<Nomina>();
            //Se buscan todos los empleados del sistema
            var empleados = db.Empleados.ToList();
            if (empleados != null)
            {
                foreach (var item in empleados)
                {
                    //List<object> valEmple = new List<object>();
                    float porcReserva = item.PComisionReserva;                     
                    float porcMembresia = item.PComisionMembresia;
                    float tporcR = 0, tporcM = 0, totalaCob = 0;
                    string nombre = item.Nombres.ToString() +" "+ item.Apellido1.ToString();
                    string cedula = item.Cedula;
                    int ventasConfirmadas = 0, ventasMemb = 0, ventasReserv = 0, ventasTotal = 0;
                    float pagosConfirmados = 0;
                    // Se buscan todas las membresias (Afiliaciones que haya hecho el Empleado)
                    var membresias = db.Afiliados.Where(x => x.UsuarioRegistro.EmpleadoID == item.EmpleadoID).ToList();
                    #region R1
                    if (membresias != null)
                    {
                        foreach (var itemMemb in membresias)
                        {
                            float costoMembresia = itemMemb.CostoMembresia;// Para saber el costo de la membresia de cada afiiliado.
                            // Se buscan todos los pagos que hayan hecho los afiliados para verificar cuales estan confirmados
                            var pagosDeAfiliados = db.Pagos.Where(x => x.Afiliado.AfiliadoID == itemMemb.AfiliadoID).ToList();
                            float pagoTotalAfiliado = 0;
                            if (pagosDeAfiliados != null)
                            {
                                foreach (var pagosA in pagosDeAfiliados)
                                {
                                    pagoTotalAfiliado += pagosA.MontoPago;
                                }
                            }
                            if (pagoTotalAfiliado >= costoMembresia)
                            {
                                ventasConfirmadas += 1;
                                pagosConfirmados += pagoTotalAfiliado;
                                tporcM += pagoTotalAfiliado;
                            }
                            ventasMemb += 1;
                        }
                    }
               
                #endregion
                    //Se buscan todas las Reservas realizadas por ese empleado.
                    var reservas = db.Reservas.Where(x=>x.Empleado.EmpleadoID == item.EmpleadoID).ToList();
                    #region reser
                    if (reservas != null)
                    {                        
                        foreach (var pagoReserv in reservas)
                        {
                            float costoReserva = pagoReserv.Costo;
                            float montoReserPag = 0;
                            // Se uscan todos las Reservas pagas de ese afiliado.
                            var pagosReservasAfil = db.PagosReservas.Where(x => x.Afiliado.AfiliadoID == pagoReserv.Afiliado.AfiliadoID).ToList();
                            if (pagosReservasAfil != null)
                            {
                                foreach (var montopagados in pagosReservasAfil)
                                {
                                    montoReserPag += montopagados.Monto;
                                }
                            }
                            if (montoReserPag >= costoReserva)
                            {
                                ventasConfirmadas += 1;
                                pagosConfirmados += montoReserPag;
                                tporcR += montoReserPag;
                            }
                            ventasReserv += 1;
                        }
                    }
                    #endregion
                    //valEmple.Add(nombre);
                    //valEmple.Add(cedula);
                    //valEmple.Add(ventasConfirmadas);
                    //valEmple.Add()
                    totalaCob = (tporcM * porcMembresia / 100) + (tporcR * porcReserva / 100);
                    var nomina = new RD.WEBAPI.Models.Nomina();
                    nomina.nombre = nombre;
                    nomina.cedula = cedula;
                    nomina.ventasConfirmadas = ventasConfirmadas;
                    nomina.MontoVentasConfirmadas = pagosConfirmados;
                    nomina.totalACobrar = totalaCob;
                    empleadosNomina.Add(nomina);
                }
            }
            await RD.WEBAPI.Controllers.ControllerHelpers.GenerarNomina(filePath,empleadosNomina);

            List<string> res = new List<string>();
            res.Add(resp);
            res.Add(filePath);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public async Task<Boolean> DeleteFile(string path)
        {

            await Task.Delay(10000);
            System.IO.File.Delete(path);
            return true;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Report607()
        {
            List<pago> Pagos = new List<pago>();
            float itbis = 18;
            var pagoMem = db.Pagos.ToList();
            //var pagosG = new pago();
            if (pagoMem != null)
            {
                foreach (var ipaM in pagoMem) {
                    var pagosMem = new pago();
                    pagosMem.montofacturado = ipaM.CostoMembresia;
                    pagosMem.rncedula = ipaM.Afiliado.Cedula;
                    pagosMem.itbisFacturado = (ipaM.CostoMembresia * itbis) / 100;
                    var comprobante = db.Comprobantes.Where(x => x.ComprbanteFiscalID == ipaM.Comprobante.ComprbanteFiscalID).FirstOrDefault();
                    pagosMem.ncomprobantef = comprobante.Secuencia;
                    pagosMem.fechaComprobante = comprobante.FechaCreacion;
                    Pagos.Add(pagosMem);
                }               
                 
            }
            var pagoRes = db.PagosReservas.ToList();
            if (pagoRes != null)
            {
                foreach (var ipaR in pagoRes)
                {
                    var pagosRes = new pago();
                    pagosRes.montofacturado = ipaR.Monto;
                    pagosRes.rncedula = ipaR.Afiliado.Cedula;
                    pagosRes.itbisFacturado = (ipaR.Monto * itbis) / 100;
                    var comprobanteR = db.Comprobantes.Where(x => x.ComprbanteFiscalID == ipaR.Comprobante.ComprbanteFiscalID).FirstOrDefault();
                    pagosRes.ncomprobantef = comprobanteR.Secuencia;
                    pagosRes.fechaComprobante = comprobanteR.FechaCreacion;
                    Pagos.Add(pagosRes);

                }
            }
            return View(Pagos);
        }
        public async Task<JsonResult> Reporte607(List<Object>List)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\";
            string filename = "DGII_F_607_CI_PER.txt";
            string filePath = Path.Combine(path, filename);
            string resp = "http://" + this.HttpContext.Request.Url.Host + ":" + this.HttpContext.Request.Url.Port + "/Images/" + filename;
            //string resp = "http://" + this.HttpContext.Request.Url.Host + ":" + this.HttpContext.Request.Url.Port + "/" + filename;
            List<pago> PagoR = new List<pago>();
            foreach (var itemL in List)
            {
                IEnumerable myList = itemL as IEnumerable;
                if (myList != null)
                {
                    pago pagoAux = new pago();
                    int n = 0;
                    foreach (object element in myList)
                    {
                        string aux = element.ToString();
                        string aux2 = aux.Trim();
                        string elemento = aux2.Replace("/n", "");
                        if (n == 0)
                        {
                            pagoAux.rncedula = elemento.ToString();
                        }
                        else if (n == 1)
                        {
                            pagoAux.tipoIdentificacion = elemento.ToString();
                        }
                        else if (n == 2)
                        {
                            pagoAux.ncomprobantef = elemento.ToString();
                        }
                        else if (n == 3)
                        {
                            pagoAux.fechaComprobante = Convert.ToDateTime(elemento);
                        }
                        else if (n == 4)
                        {
                            pagoAux.itbisFacturado = Convert.ToSingle(elemento);
                        }
                        else
                        {
                            pagoAux.montofacturado = Convert.ToSingle(elemento);
                        } 
                        
                        n++;
                    }
                    PagoR.Add(pagoAux);
                }
            }
            await RD.WEBAPI.Controllers.ControllerHelpers.Generar607(PagoR, filePath);
            List<string> res = new List<string>();
            res.Add(resp + ".zip");
            res.Add(filePath);

         //Response.Clear(); 
         //Response.AddHeader("Content-Disposition",
         //           "attachment; filename=" + "File.txt");
         //Response.AddHeader("Content-Length", 
         //           resp.Length.ToString());
         //Response.ContentType = "application/octet-stream";
         //Response.WriteFile(filePath + filename);
         //Response.End();
      
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public void loadFile(){
            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\";
            string filename = "DGII_F_607_CI_PER.txt";
            string filePath = Path.Combine(path, filename);
            string resp = "http://" + this.HttpContext.Request.Url.Host + ":" + this.HttpContext.Request.Url.Port + "/Images/" + filename;
           
            Response.Clear();
            Response.AddHeader("Content-Disposition",
                       "attachment; filename=" +filename);
            Response.AddHeader("Content-Length",
                       resp.Length.ToString());
            Response.ContentType = "text/plain";
            Response.WriteFile(filePath);
            Response.End();
        }
    }

    
}
