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
    public class UserEstadoesController : Controller
    {
        private DbContextRD db = new DbContextRD();

        // GET: UserEstadoes
        public async Task<ActionResult> Index()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }
            return View(await db.UserEstados.ToListAsync());
        }

        // GET: UserEstadoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEstado userEstado = await db.UserEstados.FindAsync(id);
            if (userEstado == null)
            {
                return HttpNotFound();
            }
            return View(userEstado);
        }

        // GET: UserEstadoes/Create
        public ActionResult Create()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: UserEstadoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserEstadoID,Estado")] UserEstado userEstado)
        {
            if (ModelState.IsValid)
            {
                db.UserEstados.Add(userEstado);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(userEstado);
        }

        // GET: UserEstadoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEstado userEstado = await db.UserEstados.FindAsync(id);
            if (userEstado == null)
            {
                return HttpNotFound();
            }
            return View(userEstado);
        }

        // POST: UserEstadoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserEstadoID,Estado")] UserEstado userEstado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userEstado).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(userEstado);
        }

        // GET: UserEstadoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEstado userEstado = await db.UserEstados.FindAsync(id);
            if (userEstado == null)
            {
                return HttpNotFound();
            }
            return View(userEstado);
        }

        // POST: UserEstadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }
            UserEstado userEstado = await db.UserEstados.FindAsync(id);
            db.UserEstados.Remove(userEstado);
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
