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
    public class HotelesDummysController : Controller
    {
        private DbContextRD db = new DbContextRD();

        // GET: HotelesDummys
        public ActionResult Index()
        {
            return View(db.HotelesDummy.ToList());
        }

        // GET: HotelesDummys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelDummy hotelDummy = db.HotelesDummy.Find(id);
            if (hotelDummy == null)
            {
                return HttpNotFound();
            }
            return View(hotelDummy);
        }

        // GET: HotelesDummys/Create
        public ActionResult Create()
        {
            ViewBag.Colors = GetColors();
            ViewBag.Hotels = GetAllHoteles();
            return View();
        }

        

        // POST: HotelesDummys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HotelesDummyID,Ubicacion,Color,Hotel")] HotelDummy hotelDummy)
        {         
                db.HotelesDummy.Add(hotelDummy);
                db.SaveChanges();
                return RedirectToAction("Index");
        }

        //Lista de Colores
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
        // GET: HotelesDummys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelDummy hotelDummy = db.HotelesDummy.Find(id);
            if (hotelDummy == null)
            {
                return HttpNotFound();
            }
            ViewBag.Hotels = GetAllHoteles();
            ViewBag.Colors = GetColors();
            return View(hotelDummy);
        }

        // POST: HotelesDummys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HotelesDummyID,Ubicacion,Color,Hotel")] HotelDummy hotelDummy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotelDummy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Colors = GetColors();
            ViewBag.Hotels = GetAllHoteles();
            return View(hotelDummy);
        }

        // GET: HotelesDummys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelDummy hotelDummy = db.HotelesDummy.Find(id);
            if (hotelDummy == null)
            {
                return HttpNotFound();
            }
            return View(hotelDummy);
        }

        // POST: HotelesDummys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HotelDummy hotelDummy = db.HotelesDummy.Find(id);
            db.HotelesDummy.Remove(hotelDummy);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public List<SelectListItem> GetAllHoteles()
        {
            List<HotelDummy> Lhd = new List<HotelDummy>();
            var hoteles = db.Hoteles.ToList();
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem
            {
                Text = "",
                Value = ""
            });
            if (hoteles != null)
            {
                foreach (var i in hoteles)
                {
                    listItems.Add(new SelectListItem
                    {
                        Text = i.Nombre,
                        Value = i.Nombre
                    });
                }
            }
            return listItems;
        }

        public JsonResult GetAllHotelesAjax()
        {
            var r = db.HotelesDummy.ToList();
            return Json(r, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHotelesById(int id)
        {
            var r = db.HotelesDummy.Find(id);
            return Json(r, JsonRequestBehavior.AllowGet);
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
