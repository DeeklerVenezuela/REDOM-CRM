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
    public class UsuariosController : Controller
    {
        private DbContextRD db = new DbContextRD();

        // GET: Usuarios
        public async Task<ActionResult> Index()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }
                if (ControllerHelpers.GetPermiso("Personas", 1) == false)
                return RedirectToAction("Error","Home");

            List<Login> login = new List<Login>();
            login = db.Logins.ToList();

            List<Login> loginAux = new List<Login>();
 
             foreach(var i in login)
             {
                 if ( i.Empleado != null)
                 {
                     loginAux.Add(i);
                 }

             }

             return View(loginAux);
            
        }

        // GET: Usuarios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }
            if (ControllerHelpers.GetPermiso("Personas", 1) == false)
                return RedirectToAction("Error", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = await db.Logins.FindAsync(id);
           
           
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        } 
          
        // GET: Usuarios/Create
        public ActionResult Create()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }
            if (ControllerHelpers.GetPermiso("Personas", 0) == false)
                return RedirectToAction("Error", "Home");
            var empleados = 
                db.Empleados.ToList()
                .Select(p => new { 
                    ID = p.EmpleadoID,
                    Nombre = p.Nombres +" "+ p.Apellido1 +" "+ p.Apellido2
            });
            ViewBag.Empleado = empleados;
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LoginID,UserName,Password,login")] Login login)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }
            if (ControllerHelpers.GetPermiso("Personas", 0) == false)
                return RedirectToAction("Error", "Home");

            login.Password = Repository.Hash.CalcularHash(login.Password);
            var empleado = db.Empleados.Where(x => x.EmpleadoID == login.LoginID).FirstOrDefault();
            login.Empleado = empleado;
            login.LoginID = 0;
            UserPermision up = new UserPermision();
            
            up.Afiliados = "000000";
            up.Archivos = "00000";
            up.Certificados = "00000";
            up.Cobros = "00000";
            up.Documentacion = "00000";
            up.Dummy = "00000";
            up.Especiales = "00000";
            up.Hoteles = "00000";
            up.Permisos = "00000";
            up.Personas = "00000";
            up.Prospectos = "00000";
            up.Reservas = "00000";
            up.Sistema = "00000";

            up.Empleado = empleado;
            db.UserPermissions.Add(up);
                db.Logins.Add(login);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

        }

        // GET: Usuarios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }
            if (ControllerHelpers.GetPermiso("Personas", 2) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = await db.Logins.FindAsync(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoginID = new SelectList(db.Empleados, "EmpleadoID", "Nombres", login.LoginID);
            login.Password = "";
            return View(login);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LoginID,UserName,Password")] Login login)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }
            if (ControllerHelpers.GetPermiso("Personas", 2) == false)
                return RedirectToAction("Error", "Home");

            if (ModelState.IsValid)
            {
                login.Password = Repository.Hash.CalcularHash(login.Password);
                db.Entry(login).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LoginID = new SelectList(db.Empleados, "EmpleadoID", "Nombres", login.LoginID);
            return View(login);
        }

        // GET: Usuarios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }
            if (ControllerHelpers.GetPermiso("Personas", 3) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = await db.Logins.FindAsync(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }
            if (ControllerHelpers.GetPermiso("Personas", 3) == false)
                return RedirectToAction("Error", "Home");
            List<ControlDummy> CDumm = new List<ControlDummy>();
            Login login = await db.Logins.FindAsync(id);
            var cd = db.ControlDummies.Where(x => x.User.LoginID == id).ToList();
            
            if (login != null)
            {
                if (cd != null)
                {
                    foreach (var dci in cd)
                    {
                        CDumm.Add(dci);
                    }
                    foreach (var idc in CDumm)
                    {
                        db.ControlDummies.Remove(idc);
                    }
                }
            }
            db.Logins.Remove(login);
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
