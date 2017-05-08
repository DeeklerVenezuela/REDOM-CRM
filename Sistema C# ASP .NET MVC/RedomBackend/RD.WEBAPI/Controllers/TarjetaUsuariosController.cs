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
    public class TarjetaUsuariosController : Controller
    {
        private DbContextRD db = new DbContextRD();

        // GET: TarjetaUsuarios
        public async Task<ActionResult> Index()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }
            return View(await db.TarjetasUsuarios.ToListAsync());
        }

        // GET: TarjetaUsuarios/Details/5
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
            TarjetaUsuario tarjetaUsuario = await db.TarjetasUsuarios.FindAsync(id);
            if (tarjetaUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tarjetaUsuario);
        }

        // GET: TarjetaUsuarios/Create
        public ActionResult Create()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: TarjetaUsuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TarjetaUsuarioID,FechaVencimiento,NumeroTarjeta")] TarjetaUsuario tarjetaUsuario)
        {
            if (ModelState.IsValid)
            {
                db.TarjetasUsuarios.Add(tarjetaUsuario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tarjetaUsuario);
        }

        // GET: TarjetaUsuarios/Edit/5
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
            TarjetaUsuario tarjetaUsuario = await db.TarjetasUsuarios.FindAsync(id);
            if (tarjetaUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tarjetaUsuario);
        }

        // POST: TarjetaUsuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TarjetaUsuarioID,FechaVencimiento,NumeroTarjeta")] TarjetaUsuario tarjetaUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tarjetaUsuario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tarjetaUsuario);
        }

        // GET: TarjetaUsuarios/Delete/5
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
            TarjetaUsuario tarjetaUsuario = await db.TarjetasUsuarios.FindAsync(id);
            if (tarjetaUsuario == null)
            {
                return HttpNotFound();
            }
            return View(tarjetaUsuario);
        }

        // POST: TarjetaUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }
            TarjetaUsuario tarjetaUsuario = await db.TarjetasUsuarios.FindAsync(id);
            db.TarjetasUsuarios.Remove(tarjetaUsuario);
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
