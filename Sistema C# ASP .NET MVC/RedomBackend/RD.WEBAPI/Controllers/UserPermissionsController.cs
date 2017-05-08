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
    public class UserPermissionsController : Controller
    {
        private DbContextRD db = new DbContextRD();

        // GET: UserPermissions
        public async Task<ActionResult> Index()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }
            return View(await db.UserPermissions.ToListAsync());
        }

        // GET: UserPermissions/Details/5
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
            UserPermision userPermision = await db.UserPermissions.FindAsync(id);
            if (userPermision == null)
            {
                return HttpNotFound();
            }
            return View(userPermision);
        }

        // GET: UserPermissions/Create
        public ActionResult Create()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: UserPermissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserPermisionID,Afiliados,Archivos,Users,Empleadoes,Dummy,Membresias,Cobros,Prospectos,Reservas,Permisos,Reportes")] UserPermision userPermision)
        {
            if (ModelState.IsValid)
            {
                db.UserPermissions.Add(userPermision);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(userPermision);
        }


        // GET: UserPermissions/Edit/5
        public ActionResult Edit(int? id)
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
            UserPermision up = new UserPermision();
            up =  db.UserPermissions.Where(y=>y.Empleado.EmpleadoID == id).FirstOrDefault();
            up.Afiliados = up.Afiliados == null ? "0000000" : up.Afiliados;
            up.Archivos = up.Archivos == null ? "00000" : up.Archivos;
            up.Certificados = up.Certificados == null ? "00000" : up.Certificados;
            up.Cobros = up.Cobros == null ? "00000" : up.Cobros;
            up.Documentacion = up.Documentacion == null ? "00000" : up.Documentacion;
            up.Dummy = up.Dummy == null ? "00000" : up.Dummy;
            up.Especiales = up.Especiales == null ? "00000" : up.Especiales;
            up.Hoteles = up.Hoteles == null ? "00000" : up.Hoteles;
            up.Permisos = up.Permisos == null ? "00000" : up.Permisos;
            up.Personas = up.Personas == null ? "00000" : up.Personas;
            up.Prospectos = up.Prospectos == null ? "00000" : up.Prospectos;
            up.Reservas = up.Reservas == null ? "00000" : up.Reservas;
            up.Sistema = up.Sistema == null ? "00000" : up.Sistema;
            if (up == null)
            {
                return HttpNotFound();
            }
            @ViewBag.Empleado = up.Empleado.Nombres + " " +up.Empleado.Apellido1 + " " + up.Empleado.Apellido2;
            return View(up);
        }

        // POST: UserPermissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserPermisionID,Afiliados,Archivos,Certificados,Personas,Hoteles,Dummy,Cobros,Prospectos,Reservas,Permisos,Documentacion,Sistema,Especiales")] UserPermision userPermision)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userPermision).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Usuarios");
            }
            return View(userPermision);
        }

        // GET: UserPermissions/Delete/5
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
            UserPermision userPermision = await db.UserPermissions.FindAsync(id);
            if (userPermision == null)
            {
                return HttpNotFound();
            }
            return View(userPermision);
        }

        // POST: UserPermissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }
            UserPermision userPermision = await db.UserPermissions.FindAsync(id);
            db.UserPermissions.Remove(userPermision);
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
