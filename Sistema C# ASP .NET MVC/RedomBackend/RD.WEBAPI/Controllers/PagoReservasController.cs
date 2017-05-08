using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RD.Data.DAL;
using RD.Data.Models;

namespace RD.WEBAPI.Controllers
{
    public class PagoReservasController : Controller
    {
        private DbContextRD db = new DbContextRD();

        // GET: PagoReservas
        public ActionResult Index(int id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Pagos", 1) == false)
                return RedirectToAction("Error", "Home");

            ViewBag.Afiliado = db.Reservas
                .Where(x => x.ReservaID == id)
                .Select(y=>y.Afiliado.AfiliadoID)
                .FirstOrDefault();
            return View(db.PagosReservas.Where(x=>x.Reserva.ReservaID == id).ToList());
        }

        // GET: PagoReservas/Details/5
        public ActionResult Details(int? id)
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
            PagosReserva pagoReserva = db.PagosReservas.Find(id);
            if (pagoReserva == null)
            {
                return HttpNotFound();
            }
            return View(pagoReserva);
        }

        // GET: PagoReservas/Create
        public ActionResult Create(int AfiliadoID, int ReservaID)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Pagos", 0) == false)
                return RedirectToAction("Error", "Home");

            //Busqueda de tarjetas del afiliado
            TarjetaUsuario Tu = new TarjetaUsuario();
            List<TarjetaUsuario> TuL = new List<TarjetaUsuario>();
            List<TarjetaUsuario> TuLP = new List<TarjetaUsuario>();
            TuL.Add(Tu);
            Afiliado Afaux = new Afiliado();
            Reserva reser = new Reserva();
            TipoComprobante tpcf = new TipoComprobante();
            PagosReserva pres = new PagosReserva();
            var Tarjs = db.Afiliados
                .Where(x => x.AfiliadoID == AfiliadoID)
                .Include(y => y.Tarjetas)
                .FirstOrDefault();
            if (Tarjs != null)
            {
                if (Tarjs.Tarjetas != null)
                {
                    foreach (var i in Tarjs.Tarjetas)
                    {
                        var d = i.NumeroTarjeta;
                        var subS = d.Substring(12, 4);
                        TarjetaUsuario Tus = new TarjetaUsuario();
                        Tus.NumeroTarjeta = "************" + subS;
                        Tus.TarjetaUsuarioID = i.TarjetaUsuarioID;
                        TuLP.Add(Tus);
                    }
                    foreach (var i in TuLP)
                        TuL.Add(i);
                }
            }
            ViewBag.cf = db.TipoComprobantes.ToList();
            ViewBag.Reserva = ReservaID;
            ViewBag.Tarjetas = TuL;
            ViewBag.AfiID = AfiliadoID;
            pres.Afiliado = Afaux;
            pres.Tarjeta = Tu;
            pres.Reserva = reser;
            return View(pres);
        }

        // POST: PagoReservas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PagoReservaID,Monto,Reserva,Tarjeta,Afiliado,Comprobante")] PagosReserva pagoReserva)
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
                pagoReserva.FechaHora = DateTime.Now;
                var usu = int.Parse(ControllerHelpers.GetString("USER"));
                //pagoReserva.Reserva = db.Reservas.Find(pagoReserva.Reserva.ReservaID);
                var Res = db.Reservas.Where(x => x.ReservaID == pagoReserva.Reserva.ReservaID).FirstOrDefault();
                var Afi = db.Afiliados.Where(x=>x.AfiliadoID == pagoReserva.Afiliado.AfiliadoID).FirstOrDefault();
                var Tarj = db.TarjetasUsuarios.Where(x => x.TarjetaUsuarioID == pagoReserva.Tarjeta.TarjetaUsuarioID).FirstOrDefault();
                var TC = db.TipoComprobantes.Where(x => x.TipoComprobanteID == pagoReserva.Comprobante.ComprbanteFiscalID).FirstOrDefault();
                pagoReserva.Reserva = Res;
                pagoReserva.Tarjeta = Tarj;
                pagoReserva.Afiliado = Afi;
                RD.Data.Models.ComprobanteFiscal CF = new ComprobanteFiscal();
                var NCF = RD.Business.Core.VoucherGenerator.MainGenerator(TC);
                CF = db.Comprobantes.Where(x => x.ComprbanteFiscalID == NCF.Comprobante).FirstOrDefault();
                if (NCF.Result == true)
                {
                    pagoReserva.Comprobante = CF;
                }
                db.PagosReservas.Add(pagoReserva);
                db.SaveChanges();
            }

            //return View("Reservas");//(pagoReserva);
            return RedirectToAction("Index", "Reservas");
        }

        // GET: PagoReservas/Edit/5
        public ActionResult Edit(int? id)
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
            PagosReserva pagoReserva = db.PagosReservas.Find(id);
            if (pagoReserva == null)
            {
                return HttpNotFound();
            }
            return View(pagoReserva);
        }

        // POST: PagoReservas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PagoReservaID,Monto,FechaHora")] PagosReserva pagoReserva)
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
                db.Entry(pagoReserva).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pagoReserva);
        }

        // GET: PagoReservas/Delete/5
        public ActionResult Delete(int? id)
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
            PagosReserva pagoReserva = db.PagosReservas.Find(id);
            if (pagoReserva == null)
            {
                return HttpNotFound();
            }
            return View(pagoReserva);
        }

        // POST: PagoReservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Pagos", 3) == false)
                return RedirectToAction("Error", "Home");

            PagosReserva pagoReserva = db.PagosReservas.Find(id);
            db.PagosReservas.Remove(pagoReserva);
            db.SaveChanges();
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
