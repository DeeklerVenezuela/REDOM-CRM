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
    public class ReservasController : Controller
    {
        private DbContextRD db = new DbContextRD();

        // GET: Reservas
        public ActionResult Index()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Reservas", 1) == false)
                return RedirectToAction("Error", "Home");
            int montoR = db.Empresas.Select(x => x.MontoReserva).FirstOrDefault();
            //var PagosR = db.PagosReservas.Where(x => x.Monto >= montoR).ToList();
            List<RD.Data.Models.Afiliado> afi = new List<RD.Data.Models.Afiliado>();
            
            var ListadoAf = db
                .Pagos.Where(x=>x.Afiliado.Status == "Activo")
                .Include(x=>x.Afiliado)
                .OrderBy(x=>x.Afiliado.FechaHora)
                .GroupBy(m=>m.Afiliado)
                .ToList();

            if (ListadoAf != null)
            {
                foreach (var items in ListadoAf)
                {
                    
                    if (items != null)
                    {
                        int n = 0;
                        var resul = items.ToList();
                        var afiadd = new RD.Data.Models.Afiliado();
                        afiadd.AfiliadoID = items.Key.AfiliadoID;
                        afiadd.AfiliadoIndice = items.Key.AfiliadoIndice;
                        afiadd.Cedula = items.Key.Cedula;
                        afiadd.Nombres = items.Key.Nombres;
                        afiadd.Apellido1 = items.Key.Apellido1;
                        afiadd.Apellido2 = items.Key.Apellido2;
                        afiadd.FechaHora = items.Key.FechaHora;
                        if (resul != null)
                        {
                            foreach (var itres in resul)
                            {
                                n += itres.MontoPago;
                            }
                        }
                        if (n >= montoR)
                        {
                            afi.Add(afiadd);
                        }
                    }
                }
            }
            //if (AfiliadosExist != null)
            //{
            //    foreach (var items in AfiliadosExist)
            //    {
            //        var pagosAfi = db.PagosReservas.Where(x => x.Afiliado.AfiliadoID == items.AfiliadoID).ToList();
            //        if (pagosAfi != null)
            //        {
            //            int monto = 0;
            //            var afiadd = new RD.Data.Models.Afiliado();
            //            foreach (var items2 in pagosAfi)
            //            {
                            
            //                monto += items2.Monto;
                            
                                
            //                    afiadd.AfiliadoID = items2.Afiliado.AfiliadoID;
            //                    afiadd.AfiliadoIndice = items2.Afiliado.AfiliadoIndice;
            //                    afiadd.Apellido1 = items2.Afiliado.Apellido1;
            //                    afiadd.Nombres = items2.Afiliado.Nombres;
            //                    afiadd.Cedula = items2.Afiliado.Cedula;
            //                    afiadd.FechaHora = items2.Afiliado.FechaHora;
                            
            //            }
            //            if (monto >= montoR)
            //            {
            //                afi.Add(afiadd);
            //            }
            //        }
            //    }
            //}
            
            return View(afi);
            //return View(db.Afiliados.Where(x=>x.AfiliadoID == db.PagosReservas.Where(y=>y.Afiliado.AfiliadoID && y.Monto >= db.Empresas.Select(z=>z.MontoReserva))).ToList());
        }

        // GET: Reservas/Details/5
        public ActionResult Details(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Reservas", 1) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas
                .Where(x => x.ReservaID == id)
                .Include(y => y.Habitaciones)
                .FirstOrDefault();
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        public ActionResult Pagar(int id, int rs)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Reservas", 0) == false)
                return RedirectToAction("Error", "Home");

            return RedirectToAction("Create", "PagoReservas", new { AfiliadoID = id, ReservaID = rs });
        }

        public ActionResult VerPagos(int id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Reservas", 1) == false)
                return RedirectToAction("Error", "Home");

            return RedirectToAction("Index", "PagoReservas", new { id = id });
        }

        // GET: Reservas/Create
        public ActionResult Create(int id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Reservas", 0) == false)
                return RedirectToAction("Error", "Home");

            List<Hotel> Hoteles = new List<Hotel>();
            HabitacionReserva HR = new HabitacionReserva();
            List<HabitacionReserva> LHR = new List<HabitacionReserva>();
            ViewBag.Certificados = db.Certificados.ToList();
            
            LHR.Add(HR);
            Hotel Test = new Hotel();
            Hoteles.Add(Test);
            var Hoteles2 = db.Hoteles.Where(x => x.ValidoHasta >= DateTime.Now).ToList();//db.Hoteles.ToList();
            if (Hoteles2 != null)
            {
                foreach (var i in Hoteles2)
                {
                    Hoteles.Add(i);
                }
            }
            ViewBag.Afiliado = db.Afiliados.Find(id);
            ViewBag.Hoteles = Hoteles;
            Reserva Reserva = new Reserva();
            Reserva.Habitaciones = LHR;
            return View(Reserva);
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservaID,Dias,Entrada,Hotel,Habitaciones,FechaHora,Costo,Status,CantidadAdultos,CantidadNinios,Afiliado")] Reserva reserva)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Reservas", 0) == false)
                return RedirectToAction("Error", "Home");

                reserva.Hotel = db.Hoteles.Find(reserva.Hotel.HotelID);
                reserva.Afiliado = db.Afiliados.Find(reserva.Afiliado.AfiliadoID);
                var usu = int.Parse(ControllerHelpers.GetString("USER"));
                var emp = db.Logins
                    .Where(x => x.LoginID == usu)
                    .Select(y => y.Empleado)
                    .FirstOrDefault();
                reserva.Empleado = emp;
                reserva.FechaHora = DateTime.Now;
                reserva.Status = "Activa";
                db.Reservas.Add(reserva);
                db.SaveChanges();

                return RedirectToAction("Index");
        }

        // GET: Reservas/Edit/5
        public ActionResult Edit(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Reservas", 2) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Where(x=>x.ReservaID == id).Include(x=>x.Afiliado).FirstOrDefault();
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservaID,FechaHora,Costo,Status,CantidadAdultos,CantidadNinios")] Reserva reserva)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Reservas", 2) == false)
                return RedirectToAction("Error", "Home");

            if (ModelState.IsValid)
            {
                db.Entry(reserva).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public ActionResult ReservasUsuario(int id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Reservas", 1) == false)
                return RedirectToAction("Error", "Home");

            List<Reserva> reservas = db.Reservas
                .Where(x=>x.Afiliado.AfiliadoID == id)
                .Include(y=>y.Afiliado)
                .ToList();
            
            if (reservas == null)
            {
                return HttpNotFound();
            }

            List<Reserva> Aux = new List<Reserva>();
            foreach (var i in reservas)
            {
                var pagos = db.PagosReservas.Where(x => x.Reserva.ReservaID == i.ReservaID).ToList();
                Reserva Res = new Reserva();
                Res = i;
                Res.PagoReservas = pagos;
                Aux.Add(Res);
            }

            return View("ReservasList",Aux);
        }

        // GET: Reservas/Delete/5
        public ActionResult Delete(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Reservas", 3) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Where(x => x.ReservaID == id).Include(y => y.Afiliado).FirstOrDefault();
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Reservas", 3) == false)
                return RedirectToAction("Error", "Home");

            Reserva reserva = db.Reservas.Find(id);
            db.Reservas.Remove(reserva);
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

        public ActionResult CreateNewHabitacion(int Habs)
        {
            ViewBag.Num = Habs;
            return PartialView("~/Views/Partial_Views/_NewHabitacion.cshtml");
        }
    }
}
