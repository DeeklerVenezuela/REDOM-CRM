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
using System.Web.Helpers;

namespace RD.WEBAPI.Controllers
{
    public class ProspectosController : Controller
    {
        private DbContextRD db = new DbContextRD();

        // GET: Prospectos
        public async Task<ActionResult> Index()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Prospectos", 1) == false)
                return RedirectToAction("Error", "Home");

            List<Prospecto> Prospectos = db.Prospectos.Where(x => x.Activo == true).ToList();
            return View(Prospectos);
        }


        // GET: Prospectos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Prospectos", 1) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prospecto prospecto = await db.Prospectos.FindAsync(id);
            if (prospecto == null)
            {
                return HttpNotFound();
            }
            return View(prospecto);
        }

        // GET: Prospectos/Create
        public ActionResult Create()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Prospectos", 0) == false)
                return RedirectToAction("Error", "Home");

            return View();
        }

        // POST: Prospectos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProspectoID,Nombres,Apellido1,Apellido2,Cedula,Respuesta,Fax,Comentario,FechaDeNacimiento,Email,Activo,Ocupado,Telefonos,Direcciones")] Prospecto prospecto)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Prospectos", 0) == false)
                return RedirectToAction("Error", "Home");

            for (var i = 0; i < prospecto.Direcciones.Count; i++)
            {
                if (String.IsNullOrEmpty(prospecto.Direcciones[i].Descripcion))
                {
                    prospecto.Direcciones.RemoveAt(i);
                }
            }

            for (var i = 0; i < prospecto.Telefonos.Count; i++)
            {
                if (String.IsNullOrEmpty(prospecto.Telefonos[i].Descripcion))
                {
                    prospecto.Telefonos.RemoveAt(i);
                }
            }

            prospecto.FechaHora = DateTime.Now;

            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid == true)
            {
                db.Prospectos.Add(prospecto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }



        public ActionResult AfterAfiliate(int id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Dummies", 0) == false)
                return RedirectToAction("Error", "Home");

            Prospecto prospecto = new Prospecto();

            if (id != null)
            {
                prospecto = db.Prospectos.Find(id);
            }
            var objeciones  = db.Objeciones.ToList();
            ViewBag.Objeciones = objeciones;
            return View("Edit", prospecto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AfterAfiliate([Bind(Include = "ProspectoID,Nombres,Apellido1,Apellido2,Cedula,Respuesta,Fax,FechaHora,Comentario,FechaDeNacimiento,Email,Activo,Ocupado,Direcciones,Telefonos")] Prospecto prospecto)
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
                await GuardarAfiliado(prospecto);
                return RedirectToAction("Index", "Dummies");
            }
            ViewBag.Objeciones = db.Objeciones.ToList();
            return View(prospecto);
        }

        public async Task<bool> GuardarAfiliado(Prospecto prospecto)
        {
            if (prospecto.Telefonos != null)
            {
                foreach (var i in prospecto.Telefonos)
                    db.Entry(i).State = EntityState.Modified;
            }
            if (prospecto.Direcciones != null)
            {
                foreach (var j in prospecto.Direcciones)
                    db.Entry(j).State = EntityState.Modified;
            }
            
            prospecto.FechaHora = DateTime.Now;
            db.Entry(prospecto).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return true;
        }

        // GET: Prospectos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Prospectos", 2) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prospecto prospecto = await db.Prospectos.FindAsync(id);
            if (prospecto == null)
            {
                return HttpNotFound();
            }
            ViewBag.Objeciones = db.Objeciones.ToList();
            return View(prospecto);
        }

        // POST: Prospectos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProspectoID,Nombres,Apellido1,Apellido2,Cedula,Respuesta,Fax,FechaHora,Comentario,FechaDeNacimiento,Email,Activo,Ocupado,Telefonos,Direcciones")] Prospecto prospecto)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Prospectos", 2) == false)
                return RedirectToAction("Error", "Home");

            if (ModelState.IsValid)
            {
                await GuardarAfiliado(prospecto);
                return RedirectToAction("Index");
            }
            ViewBag.Objeciones = db.Objeciones.ToList();
            return View(prospecto);
        }

        // GET: Prospectos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Prospectos", 3) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prospecto prospecto = await db.Prospectos.FindAsync(id);
            if (prospecto == null)
            {
                return HttpNotFound();
            }
            return View(prospecto);
        }

        // POST: Prospectos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Prospectos", 3) == false)
                return RedirectToAction("Error", "Home");

            Prospecto prospecto = await db.Prospectos.FindAsync(id);
            List<Direccion> Direcciones = new List<Direccion>();
            List<Telefono> Telefonos = new List<Telefono>();

            foreach (var i in prospecto.Direcciones)
                Direcciones.Add(i);

            foreach (var j in prospecto.Telefonos)
                Telefonos.Add(j);

            foreach (var k in Direcciones)
                db.Direcciones.Remove(k);

            foreach (var l in Telefonos)
                db.Telefonos.Remove(l);

            db.Prospectos.Remove(prospecto);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> PostDummy([Bind(Include = "ProspectoID,Nombres,Apellido1,Apellido2,Cedula,Respuesta,Fax,FechaHora,Comentario,FechaDeNacimiento,Email,Activo,Ocupado,Telefonos,Direcciones")] Prospecto prospecto)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso("Dummies", 0) == false)
                return RedirectToAction("Error", "Home");

            await GuardarAfiliado(prospecto);
            return RedirectToAction("QuickCreate", "Afiliados", new { id = prospecto.ProspectoID });
        }

        //Se que esto no se hace pero no tenia mucho tiempo y no queria correr con un objeto :(
        public JsonResult SoloGuardar(
            int Id,
            string Nombres,
            string Apellido1,
            string Apellido2,
            string Cedula,
            string Respuesta,
            string Fax,
            string FechaDeNacimiento,
            string Email,
            string Comentario,
            string Tipo1,
            string Descripcion1,
            string Extension1,
            string Tipo2,
            string Descripcion2,
            string Extension2,
            string Tipo3,
            string Descripcion3,
            string Extension3,
            string Direccion1,
            string Direccion2
            )
        {

            Prospecto p = new Prospecto();
            p = db.Prospectos.Where(x => x.ProspectoID == Id).FirstOrDefault();
            p.Nombres = Nombres;
            p.Apellido1 = Apellido1;
            p.Apellido2 = Apellido2;
            p.Cedula = Cedula;
            p.Respuesta = Respuesta;
            p.Fax = Fax;
            p.FechaDeNacimiento = DateTime.Parse(FechaDeNacimiento);
            p.Email = Email;
            p.Afiliado = false;
            p.Comentario = Comentario;
            
            if (p.Telefonos.Count > 0)
            {
                p.Telefonos[0].Descripcion = Descripcion1;
                p.Telefonos[0].Extension = Extension1;
                p.Telefonos[0].Tipo = Tipo1;
            }

            if (p.Telefonos.Count > 1)
            {
                p.Telefonos[1].Descripcion = Descripcion2;
                p.Telefonos[1].Extension =Extension2;
                p.Telefonos[1].Tipo = Tipo2;
            }

            if (p.Telefonos.Count > 2)
            {
                p.Telefonos[2].Descripcion =Descripcion3;
                p.Telefonos[2].Extension =Extension3;
                p.Telefonos[2].Tipo = Tipo3;
            }

            if (p.Direcciones.Count > 0)
            {
                p.Direcciones[0].Descripcion = Direccion1;
            }

            if (p.Direcciones.Count > 1)
            {
                p.Direcciones[1].Descripcion =Direccion2;
            }

            db.Entry(p).State = EntityState.Modified;
            db.SaveChanges();

            return Json("Datos guardados exitosamente",JsonRequestBehavior.AllowGet);
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
