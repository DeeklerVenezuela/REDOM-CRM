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
    public class CertificadosController : Controller
    {
        private DbContextRD db = new DbContextRD();

        // GET: Certificados
        public ActionResult Index()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Certificados", 1) == false)
                return RedirectToAction("Error", "Home");

            return View(db.Certificados.ToList());
        }

        // GET: Certificados/Details/5
        public ActionResult Details(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Sistema", 1) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CertificadoDeRegalo certificadoDeRegalo = db.Certificados.Find(id);
            if (certificadoDeRegalo == null)
            {
                return HttpNotFound();
            }
            return View(certificadoDeRegalo);
        }

        // GET: Certificados/Create
        public ActionResult Create()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Sistema", 0) == false)
                return RedirectToAction("Error", "Home");

            List<SelectListItem> Tipo = new List<SelectListItem>();
            Tipo.Add(new SelectListItem { Text = "ACOMPAÑANTES GRATIS", Value = "ACOMPANANTE" });
            Tipo.Add(new SelectListItem { Text = "NOCHES GRATIS", Value = "NOCHE" });
            Tipo.Add(new SelectListItem { Text = "PORCENTAJE DE DESCUENTO", Value = "PORCENTAJE" });
            ViewBag.Tipo = Tipo;
            return View();
        }

        // POST: Certificados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CertificadoDeRegaloID,Nombre,Tipo,Cantidad,Descuento,Status")] CertificadoDeRegalo certificadoDeRegalo)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Sistema", 0) == false)
                return RedirectToAction("Error", "Home");

                db.Certificados.Add(certificadoDeRegalo);
                db.SaveChanges();
                return RedirectToAction("Index");
            
        }

        // GET: Certificados/Edit/5
        public ActionResult Edit(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Sistema", 2) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CertificadoDeRegalo certificadoDeRegalo = db.Certificados.Find(id);
            
            if (certificadoDeRegalo == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> Tipo = new List<SelectListItem>();
            Tipo.Add(new SelectListItem { Text = "ACOMPAÑANTES GRATIS", Value = "ACOMPANANTE" });
            Tipo.Add(new SelectListItem { Text = "NOCHES GRATIS", Value = "NOCHE" });
            Tipo.Add(new SelectListItem { Text = "PORCENTAJE DE DESCUENTO", Value = "PORCENTAJE" });
            ViewBag.Tipo = Tipo;
            return View(certificadoDeRegalo);
        }

        // POST: Certificados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CertificadoDeRegaloID,Nombre,Tipo,Cantidad,Descuento,Status")] CertificadoDeRegalo certificadoDeRegalo)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Sistema", 2) == false)
                return RedirectToAction("Error", "Home");

            if (ModelState.IsValid)
            {
                db.Entry(certificadoDeRegalo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<SelectListItem> Tipo = new List<SelectListItem>();
            Tipo.Add(new SelectListItem { Text = "ACOMPAÑANTES GRATIS", Value = "ACOMPANANTE" });
            Tipo.Add(new SelectListItem { Text = "NOCHES GRATIS", Value = "NOCHE" });
            Tipo.Add(new SelectListItem { Text = "PORCENTAJE DE DESCUENTO", Value = "PORCENTAJE" });
            ViewBag.Tipo = Tipo;
            return View(certificadoDeRegalo);
        }

        // GET: Certificados/Delete/5
        public ActionResult Delete(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Sistema", 3) == false)
                return RedirectToAction("Error", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CertificadoDeRegalo certificadoDeRegalo = db.Certificados.Find(id);
            if (certificadoDeRegalo == null)
            {
                return HttpNotFound();
            }
            return View(certificadoDeRegalo);
        }

        // POST: Certificados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Sistema", 3) == false)
                return RedirectToAction("Error", "Home");

            CertificadoDeRegalo certificadoDeRegalo = db.Certificados.Find(id);
            db.Certificados.Remove(certificadoDeRegalo);
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
