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
    public class DocumentacionesController : Controller
    {
        private DbContextRD db = new DbContextRD();

        // GET: Documentaciones
        public ActionResult Index()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Documentaciones", 1) == false)
                return RedirectToAction("Error", "Home");

            //Aqui va la lógica de la documentación
            //var Afiliados = db.Afiliados.ToList(); //Primero capturamos a todos los afiliados
            //var MontoMinimo = db.Empresas.Select(x => x.MontoDocumentacion).FirstOrDefault();
            //List<int> IdsElim = new List<int>();

            //foreach (var i in Afiliados)
            //{
            //    var d = db.Pagos.Where(x => x.Afiliado.AfiliadoID == i.AfiliadoID).ToList();
            //    var m = 0;
            //    foreach (var t in d)
            //    {
            //        m = m + t.MontoPago;
            //    }

            //    if (m < MontoMinimo)
            //        IdsElim.Add(i.AfiliadoID);
            //}

            //var Aux = Afiliados;

            //if (Aux.Count > 0)
            //{
            //    for (var f = 0; f < Aux.Count; f++)
            //    {
            //        Aux.RemoveAt(f);
            //    }
            //}//En este punto ya hemos quitado de la lista a quienes no cumplen con la condición

            //var Documentaciones = db.Documentaciones.ToList();
            
            //for (var c = 0; c < Aux.Count; c++)
            //{
            //    var r = Documentaciones.Where(x => x.Afiliado.AfiliadoID == Aux[c].AfiliadoID).FirstOrDefault();
            //    if (r != null)
            //        Aux.RemoveAt(c);
            //}//En este punto sabemos que documentaciones no se han generado ahora viene lo bueno

            //foreach (var y in Aux)
            //{
            //    Documentacion Doc = new Documentacion() { 
            //        Afiliado = y,
            //        Generada = false,
            //        Carnet = false,
            //        CartaBienvenida = false,
            //        CertificadoRegalo = false,
            //        CertificadoRegalo2 = false,
            //        LabelSobres = false
            //    };

            //    Documentaciones.Add(Doc);
            //}

            //db.SaveChanges();

            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Carnets", Value = "0" });

            items.Add(new SelectListItem { Text = "Label", Value = "1" });

            //items.Add(new SelectListItem { Text = "Tarjetas", Value = "2", Selected = true });

            ViewBag.ReportType = items;

            return View(db.Documentaciones.ToList());
        }

        // GET: Documentaciones/Details/5
        public ActionResult Details(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Documentaciones", 1) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documentacion documentacion = db.Documentaciones.Find(id);
            if (documentacion == null)
            {
                return HttpNotFound();
            }
            return View(documentacion);
        }

        // GET: Documentaciones/Create
        public ActionResult Create()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Documentaciones", 0) == false)
                return RedirectToAction("Error", "Home");

            return View();
        }

        // POST: Documentaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DocumentacionID,CartaBienvenida,Carnet,LabelSobres,CertificadoRegalo,CertificadoRegalo2,Generada")] Documentacion documentacion)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Documentaciones", 0) == false)
                return RedirectToAction("Error", "Home");

            if (ModelState.IsValid)
            {
                db.Documentaciones.Add(documentacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(documentacion);
        }

        // GET: Documentaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Documentaciones", 2) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documentacion documentacion = db.Documentaciones.Find(id);
            if (documentacion == null)
            {
                return HttpNotFound();
            }
            return View(documentacion);
        }

        // POST: Documentaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DocumentacionID,CartaBienvenida,Carnet,LabelSobres,CertificadoRegalo,CertificadoRegalo2,Generada")] Documentacion documentacion)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Documentaciones", 2) == false)
                return RedirectToAction("Error", "Home");

            if (ModelState.IsValid)
            {
                db.Entry(documentacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(documentacion);
        }

        // GET: Documentaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Documentaciones", 3) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Documentacion documentacion = db.Documentaciones.Find(id);
            if (documentacion == null)
            {
                return HttpNotFound();
            }
            return View(documentacion);
        }

        // POST: Documentaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Documentaciones", 3) == false)
                return RedirectToAction("Error", "Home");

            Documentacion documentacion = db.Documentaciones.Find(id);
            db.Documentaciones.Remove(documentacion);
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
