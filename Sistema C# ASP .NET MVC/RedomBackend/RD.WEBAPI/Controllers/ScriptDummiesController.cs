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
    public class ScriptDummiesController : Controller
    {
        private DbContextRD db = new DbContextRD();

        // GET: ScriptDummies
        public ActionResult Index()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Sistema", 1) == false)
                return RedirectToAction("Error", "Home");

            return View(db.ScriptDummies.ToList());
        }

        // GET: ScriptDummies/Details/5
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
            ScriptDummy scriptDummy = db.ScriptDummies.Find(id);
            if (scriptDummy == null)
            {
                return HttpNotFound();
            }
            return View(scriptDummy);
        }

        // GET: ScriptDummies/Create
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

        // POST: ScriptDummies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ScriptDummyID,Titulo,Descripcion,Color")] ScriptDummy scriptDummy)
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
                db.ScriptDummies.Add(scriptDummy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Colors = GetColors();
            return View(scriptDummy);
        }

        // GET: ScriptDummies/Edit/5
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
            ScriptDummy scriptDummy = db.ScriptDummies.Find(id);
            if (scriptDummy == null)
            {
                return HttpNotFound();
            }
            ViewBag.Colors = GetColors();
            return View(scriptDummy);
        }

        // POST: ScriptDummies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ScriptDummyID,Titulo,Descripcion,Color")] ScriptDummy scriptDummy)
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
                db.Entry(scriptDummy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Colors = GetColors();
            return View(scriptDummy);
        }

        // GET: ScriptDummies/Delete/5
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
            ScriptDummy scriptDummy = db.ScriptDummies.Find(id);
            if (scriptDummy == null)
            {
                return HttpNotFound();
            }
            return View(scriptDummy);
        }

        // POST: ScriptDummies/Delete/5
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

            ScriptDummy scriptDummy = db.ScriptDummies.Find(id);
            db.ScriptDummies.Remove(scriptDummy);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public JsonResult GetAllScripts()
        {
            List<ScriptDummy> Scripts = new List<ScriptDummy>();
            Scripts = db.ScriptDummies.OrderBy(y => y.Titulo).ToList();
            return Json(Scripts, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetScriptsById(int id)
        {
            ScriptDummy Script = new ScriptDummy();
            Script = db.ScriptDummies.Where(x => x.ScriptDummyID == id).FirstOrDefault();
            return Json(Script, JsonRequestBehavior.AllowGet);
        }

        private List<SelectListItem> GetColors()
        {
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
