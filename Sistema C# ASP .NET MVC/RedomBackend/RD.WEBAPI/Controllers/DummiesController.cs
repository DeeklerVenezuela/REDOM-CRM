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
    public class DummiesController : Controller
    {
        private DbContextRD db = new DbContextRD();

        // GET: Dummies
        public ActionResult Index()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Dummies", 1) == false)
                return RedirectToAction("Error", "Home");
            ViewBag.SetResult = "";
            //Get user connected
            var SessionUser = int.Parse(ControllerHelpers.GetString("user"));
            var Dummies = db.ControlDummies
                .Where(x => x.User.Empleado.EmpleadoID == SessionUser)
                .OrderBy(y => y.FechaHora)
                .ToList();
            if (Dummies.Count > 0)
            {
                var LastDummy = Dummies.Last();
                ViewBag.State = "";
                return View(LastDummy);
            }
            else
            {
                ViewBag.State = "Usted no ha trabajado a ningún prospecto anteriormente";
                return View();
            }
                

        }

        // GET: Dummies/Details/5
        public ActionResult Details(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Dummies", 1) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prospecto prospecto = db.Prospectos.Find(id);
            if (prospecto == null)
            {
                return HttpNotFound();
            }
            return View(prospecto);
        }

        // GET: Dummies/Create
        public ActionResult Create()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Dummies", 0) == false)
                return RedirectToAction("Error", "Home");
            return View();
        }

        // POST: Dummies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProspectoID,Nombres,Apellido1,Apellido2,Cedula,Respuesta,Fax,FechaHora,Comentario,FechaDeNacimiento,Email,Activo,Ocupado")] Prospecto prospecto)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Dummies", 0) == false)
                return RedirectToAction("Error", "Home");

            if (ModelState.IsValid)
            {
                db.Prospectos.Add(prospecto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prospecto);
        }

        // GET: Dummies/Edit/5
        public ActionResult Edit(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Dummies", 2) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prospecto prospecto = db.Prospectos.Find(id);
            if (prospecto == null)
            {
                return HttpNotFound();
            }
            return View(prospecto);
        }

        // POST: Dummies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProspectoID,Nombres,Apellido1,Apellido2,Cedula,Respuesta,Fax,FechaHora,Comentario,FechaDeNacimiento,Email,Activo,Ocupado")] Prospecto prospecto)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Dummies", 2) == false)
                return RedirectToAction("Error", "Home");

            if (ModelState.IsValid)
            {
                db.Entry(prospecto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prospecto);
        }

        // GET: Dummies/Delete/5
        public ActionResult Delete(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Dummies", 3) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prospecto prospecto = db.Prospectos.Find(id);
            if (prospecto == null)
            {
                return HttpNotFound();
            }
            return View(prospecto);
        }

        // POST: Dummies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Dummies", 3) == false)
                return RedirectToAction("Error", "Home");

            Prospecto prospecto = db.Prospectos.Find(id);
            db.Prospectos.Remove(prospecto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetProspecto()
        {
            var user = ControllerHelpers.GetString("user");
            //ViewBag.SetResult = "";
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Dummies", 0) == false)
                return RedirectToAction("Error", "Home");

            var prospecto = db.Prospectos.Where(x => x.Activo == true && x.Ocupado == false).FirstOrDefault();
            if (prospecto != null)
            {
                prospecto.Ocupado = true;
                db.Entry(prospecto).State = EntityState.Modified;
                ControlDummy CD = new ControlDummy();
                CD.FechaHora = DateTime.Now;
                CD.Prospecto = prospecto;
                var SessionUser = int.Parse(ControllerHelpers.GetString("user"));
                var userS = db.Logins.Where(x => x.Empleado.EmpleadoID == SessionUser).FirstOrDefault();
                CD.User = userS;
                db.ControlDummies.Add(CD);
                db.SaveChanges();
                return RedirectToAction("AfterAfiliate", "Prospectos", new { id = prospecto.ProspectoID });
            }
            else
            {
                ViewBag.SetResult = "Actualmente No existen prospectos.";
                var SessionUser2 = int.Parse(ControllerHelpers.GetString("user"));
                var Dummies = db.ControlDummies
                    .Where(x => x.User.Empleado.EmpleadoID == SessionUser2)
                    .OrderBy(y => y.FechaHora)
                    .ToList();
                if (Dummies.Count > 0)
                {
                    var LastDummy = Dummies.Last();
                    ViewBag.Status = "No hay prospectos para seleccionar";
                    return View("Index", LastDummy);
                }
                return View("Index");
            }
            
            
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
