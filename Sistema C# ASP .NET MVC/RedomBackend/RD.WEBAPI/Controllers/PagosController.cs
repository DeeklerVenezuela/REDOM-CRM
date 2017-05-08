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

namespace RD.WEBAPI.Controllers
{
    public class PagosController : Controller
    {
        private DbContextRD db = new DbContextRD();

        // GET: Pagos
        public async Task<ActionResult> Index()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Pagos", 1) == false)
                return RedirectToAction("Error", "Home");

            return View(await db.Pagos.ToListAsync());
        }

        // GET: Pagos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Pagos", 1) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago pago = await db.Pagos.FindAsync(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            return View(pago);
        }

        // GET: Pagos/Create
        public ActionResult Create()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Pagos", 0) == false)
                return RedirectToAction("Error", "Home");

            List<TarjetaUsuario> Tarjetas = new List<TarjetaUsuario>();
            ViewBag.TipoComprobante = db.TipoComprobantes.ToList();
            ViewBag.Tarjetas = Tarjetas;
            ViewBag.Afiliados = db.Afiliados.ToList();
            return View();
        }

        public JsonResult GetTarjetaUsuario(int id){
            
            var result = db.Afiliados.Where(x => x.AfiliadoIndice == id).Include(y=>y.Tarjetas).FirstOrDefault();
            if (result != null)
            {
                foreach (var i in result.Tarjetas)
                {
                    var subS = i.NumeroTarjeta.Substring(12, 4);
                    i.NumeroTarjeta = "************" + subS;
                }
            }
            
            return Json(result.Tarjetas,JsonRequestBehavior.AllowGet);
        }

        // POST: Pagos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PagoID,MontoPago,Comentario,Afiliado,TarjetaUsuario,Comprobante")] Pago pago)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Pagos", 0) == false)
                return RedirectToAction("Error", "Home");

            if (ModelState.IsValid)
            {
                //Preparar pago para inserción
                var Afiliado = db.Afiliados.Where(x => x.AfiliadoIndice == pago.Afiliado.AfiliadoIndice).FirstOrDefault();
                pago.CostoMembresia = Afiliado.CostoMembresia;
                pago.Afiliado = Afiliado;
                pago.FechaHora = DateTime.Now;
                var MontoMinimo = db.Empresas.Select(x => x.MontoDocumentacion).FirstOrDefault(); //Para generar la doc.
                pago.TarjetaUsuario = db.TarjetasUsuarios.Where(x => x.TarjetaUsuarioID == pago.TarjetaUsuario.TarjetaUsuarioID).FirstOrDefault();
                var TC = db.TipoComprobantes.Where(x => x.TipoComprobanteID == pago.PagoID).FirstOrDefault();
                pago.PagoID = 0;
                int pagoHecho = pago.MontoPago;
                var usu = int.Parse(ControllerHelpers.GetString("USER"));
                //pago.Empleado = db.Logins.Where(x => x.LoginID == usu).Select(y=>y.Empleado).FirstOrDefault();
                var idL = db.Logins.Where(x => x.Empleado.EmpleadoID == usu).Select(y => y.LoginID).FirstOrDefault();
                pago.Empleado = db.Logins.Where(x => x.LoginID == idL).Select(y => y.Empleado).FirstOrDefault();
                RD.Data.Models.ComprobanteFiscal CF = new ComprobanteFiscal();
                var NCF = RD.Business.Core.VoucherGenerator.MainGenerator(TC);
                CF = db.Comprobantes.Where(x=>x.ComprbanteFiscalID == NCF.Comprobante).FirstOrDefault();
                if (NCF.Result == true)
                {
                    if (pago.Comprobante == null)
                            pago.Comprobante = CF;
                    var d = db.Pagos.Where(x => x.Afiliado.AfiliadoID == Afiliado.AfiliadoID).ToList();
                    var m = 0;
                    foreach (var t in d)
                    {
                        m = m + t.MontoPago;
                    }
                    m = m + pagoHecho;
                    
                    db.Pagos.Add(pago);
                    if (m >= Afiliado.CostoMembresia)
                    {
                        var acmv = db.MembresiaVences.Where(x => x.AfiliadoM.AfiliadoID == Afiliado.AfiliadoID).FirstOrDefault();
                        if (acmv != null)
                        {
                            acmv.FechaInicio = Afiliado.FechaHora;
                            acmv.FechaFin = Afiliado.FechaVencimiento;
                            acmv.Called = true;
                            db.Entry(acmv).State = EntityState.Modified;
                        }
                        else
                        {
                            MembresiaVence mva = new MembresiaVence()
                            {
                                AfiliadoM = Afiliado,
                                FechaInicio = Afiliado.FechaHora,
                                FechaFin = Afiliado.FechaVencimiento,
                                Called = true
                            };
                            db.MembresiaVences.Add(mva);
                        }
                    }
                    if (m >= MontoMinimo)
                    {
                        Documentacion Doc = new Documentacion()
                        {
                            Afiliado = Afiliado,
                            Generada = false,
                            Carnet = false,
                            CartaBienvenida = false,
                            CertificadoRegalo = false,
                            CertificadoRegalo2 = false,
                            LabelSobres = false
                        };
                        db.Documentaciones.Add(Doc);                       
                        
                    }
                    await db.SaveChangesAsync();
                    return RedirectToAction("Create");
                }
               
                

                
            }

            return View("Index");
        }

        // GET: Pagos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Pagos", 2) == false)
                return RedirectToAction("Error", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago pago = await db.Pagos.FindAsync(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            return View(pago);
        }

        // POST: Pagos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "PagoID,FechaHora,CostoMembresia,MontoPago,Comentario")] Pago pago)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Pagos", 2) == false)
                return RedirectToAction("Error", "Home");

            if (ModelState.IsValid)
            {
                db.Entry(pago).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pago);
        }

        // GET: Pagos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Pagos", 3) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago pago = await db.Pagos.FindAsync(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            return View(pago);
        }

        // POST: Pagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Pagos", 3) == false)
                return RedirectToAction("Error", "Home");

            Pago pago = await db.Pagos.FindAsync(id);
            db.Pagos.Remove(pago);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
