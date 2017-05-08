using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RD.Data.Models;
using RD.Data.DAL;

namespace RD.WEBAPI.Controllers
{
    public class GastosController : Controller
    {
        private DbContextRD db = new DbContextRD();

        // GET: /Gastos/
        public ActionResult Index()
        {
            return View(db.Gastos.ToList());
        }

        // GET: /Gastos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gasto gasto = db.Gastos.Find(id);
            if (gasto == null)
            {
                return HttpNotFound();
            }
            return View(gasto);
        }

        // GET: /Gastos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Gastos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="GastoID,Concepto,Observaciones,FechaPago,Nombre,cedula,FechaComprobante,ItbisFacturado,ItbisRetenido,MontoFacturado,RetencionRenta,TipoBienesServicios,NCF,TipoId,DocumentoModificado,status")] Gasto gasto)
        {
            if (ModelState.IsValid)
            {
                db.Gastos.Add(gasto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gasto);
        }

        // GET: /Gastos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gasto gasto = db.Gastos.Find(id);
            if (gasto == null)
            {
                return HttpNotFound();
            }
            return View(gasto);
        }

        // POST: /Gastos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="GastoID,Concepto,Observaciones,FechaPago,Nombre,cedula,FechaComprobante,ItbisFacturado,ItbisRetenido,MontoFacturado,RetencionRenta,TipoBienesServicios,NCF,TipoId,DocumentoModificado,status")] Gasto gasto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gasto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gasto);
        }

        // GET: /Gastos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gasto gasto = db.Gastos.Find(id);
            if (gasto == null)
            {
                return HttpNotFound();
            }
            return View(gasto);
        }

        // POST: /Gastos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gasto gasto = db.Gastos.Find(id);
            db.Gastos.Remove(gasto);
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
