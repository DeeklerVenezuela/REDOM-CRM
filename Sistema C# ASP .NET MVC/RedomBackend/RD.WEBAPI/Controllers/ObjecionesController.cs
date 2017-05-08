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
    public class ObjecionesController : Controller
    {
        private DbContextRD db = new DbContextRD();

        // GET: Objeciones
        public ActionResult Index()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Sistema", 1) == false)
                return RedirectToAction("Error", "Home");

            return View(db.Objeciones.ToList());
        }

        // GET: Objeciones/Details/5
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
            ObjecionesDummy objecionesDummy = db.Objeciones.Find(id);
            if (objecionesDummy == null)
            {
                return HttpNotFound();
            }
            return View(objecionesDummy);
        }

        // GET: Objeciones/Create
        public ActionResult Create()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Sistema", 0) == false)
                return RedirectToAction("Error", "Home");
   
            ViewBag.Colors = GetColors();
            return View();
        }

        // POST: Objeciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ObjecionesDummyID,Titulo,Descripcion,Color")] ObjecionesDummy objecionesDummy)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Sistema", 0) == false)
                return RedirectToAction("Error", "Home");

            if (ModelState.IsValid)
            {
                db.Objeciones.Add(objecionesDummy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Colors = GetColors();
            return View(objecionesDummy);
        }

        // GET: Objeciones/Edit/5
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
            ObjecionesDummy objecionesDummy = db.Objeciones.Find(id);
            if (objecionesDummy == null)
            {
                return HttpNotFound();
            }
            ViewBag.Colors = GetColors();
            return View(objecionesDummy);
        }

        private List<SelectListItem> GetColors() {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem
            {
                Text = "Azul",
                Value = "bg-blue"
            });
            listItems.Add(new SelectListItem
            {
                Text = "Verde",
                Value = "bg-green",
                Selected = true
            });
            listItems.Add(new SelectListItem
            {
                Text = "Amarillo",
                Value = "bg-yellow"
            });
            listItems.Add(new SelectListItem
            {
                Text = "Rojo",
                Value = "bg-red"
            });
            listItems.Add(new SelectListItem
            {
                Text = "Negro",
                Value = "bg-black"
            });
            return listItems;
        } 
        // POST: Objeciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ObjecionesDummyID,Titulo,Descripcion,Color")] ObjecionesDummy objecionesDummy)
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
                db.Entry(objecionesDummy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Colors = GetColors();
            return View(objecionesDummy);
        }

        // GET: Objeciones/Delete/5
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
            ObjecionesDummy objecionesDummy = db.Objeciones.Find(id);
            if (objecionesDummy == null)
            {
                return HttpNotFound();
            }
            return View(objecionesDummy);
        }

        // POST: Objeciones/Delete/5
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
            ObjecionesDummy objecionesDummy = db.Objeciones.Find(id);
            db.Objeciones.Remove(objecionesDummy);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetAllObjeciones()
        {
            List<ObjecionesDummy> Objeciones = new List<ObjecionesDummy>();
            Objeciones = db.Objeciones.OrderBy(y=>y.Titulo).ToList();
            return Json(Objeciones, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetObjecionesById(int id)
        {
            ObjecionesDummy Objeciones = new ObjecionesDummy();
            Objeciones = db.Objeciones.Where(x=>x.ObjecionesDummyID == id).FirstOrDefault();
            return Json(Objeciones, JsonRequestBehavior.AllowGet);
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
