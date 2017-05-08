using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RD.Data.Models;
using RD.Data.DAL;
using System.Threading.Tasks;

namespace RD.WEBAPI.Controllers
{
    public class MembresiaVenceController : Controller
    {
        private DbContextRD db = new DbContextRD();

        // GET: /MembresiaVence/
        public async Task<ActionResult> Index()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }
            if (ControllerHelpers.GetPermiso("Afiliados", 5) == false)
                return RedirectToAction("Error", "Home");
            //return View(await db.MembresiaVences.Where(x=> x.Called==false).ToListAsync());
            return View();
        }

        // GET: /MembresiaVence/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ControllerHelpers.GetPermiso("Afiliados", 5) == false)
                return RedirectToAction("Error", "Home");
            MembresiaVence membresiavence = db.MembresiaVences.Find(id);
            if (membresiavence == null)
            {
                return HttpNotFound();
            }
            return View(membresiavence);
        }

        // GET: /MembresiaVence/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /MembresiaVence/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MembresiaVenceID,FechaInicio,FechaFin")] MembresiaVence membresiavence)
        {
            if (ModelState.IsValid)
            {
                db.MembresiaVences.Add(membresiavence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(membresiavence);
        }

        // GET: /MembresiaVence/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembresiaVence membresiavence = db.MembresiaVences.Find(id);
            if (membresiavence == null)
            {
                return HttpNotFound();
            }
            return View(membresiavence);
        }

        // POST: /MembresiaVence/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MembresiaVenceID,FechaInicio,FechaFin")] MembresiaVence membresiavence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(membresiavence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(membresiavence);
        }

        // GET: /MembresiaVence/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MembresiaVence membresiavence = db.MembresiaVences.Find(id);
            if (membresiavence == null)
            {
                return HttpNotFound();
            }
            return View(membresiavence);
        }

        // POST: /MembresiaVence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MembresiaVence membresiavence = db.MembresiaVences.Find(id);
            db.MembresiaVences.Remove(membresiavence);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public class jQueryDataTableParamModel
        {
            /// <summary>
            /// Request sequence number sent by DataTable,
            /// same value must be returned in response
            /// </summary>       
            public string sEcho { get; set; }

            /// <summary>
            /// Text used for filtering
            /// </summary>
            public string sSearch { get; set; }

            /// <summary>
            /// Number of records that should be shown in table
            /// </summary>
            public int iDisplayLength { get; set; }

            /// <summary>
            /// First record that should be shown(used for paging)
            /// </summary>
            public int iDisplayStart { get; set; }

            /// <summary>
            /// Number of columns in table
            /// </summary>
            public int iColumns { get; set; }

            /// <summary>
            /// Number of columns that are used in sorting
            /// </summary>
            public int iSortingCols { get; set; }

            /// <summary>
            /// Comma separated list of column names
            /// </summary>
            public string sColumns { get; set; }
        }
        public JsonResult GetAllAfiAsy(jQueryDataTableParamModel param)
        {
            List<Afiliado> listaf = new List<Afiliado>();
            //var ret = db.Afiliados.ToList();
            var ret = db.MembresiaVences.Include(y => y.AfiliadoM).Where(x => x.FechaFin < DateTime.Now || !x.AfiliadoM.Status.Equals("Activo") || x.Called == false).ToList();
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                ret = db.MembresiaVences.Include(y => y.AfiliadoM).Where(x => x.FechaFin < DateTime.Now || !x.AfiliadoM.Status.Equals("Activo") || x.Called == false && (x.AfiliadoM.Cedula.Contains(param.sSearch) || x.AfiliadoM.Apellido1.Contains(param.sSearch) || x.AfiliadoM.Apellido2.Contains(param.sSearch) || x.AfiliadoM.Nombres.Contains(param.sSearch) || x.AfiliadoM.RNC.Contains(param.sSearch) || x.AfiliadoM.RazonSocial.Contains(param.sSearch))).ToList();
                //ret = db.Afiliados.Where(x => x.Cedula.Contains(param.sSearch) || x.Nombres.Contains(param.sSearch) || x.Apellido1.Contains(param.sSearch) || x.Apellido2.Contains(param.sSearch) || x.RNC.Contains(param.sSearch) || x.RazonSocial.Contains(param.sSearch)).ToList();
            }
            List<string[]> listf = new List<string[]>();
            if (ret != null)
            {
                foreach (var items in ret)
                {
                    string[] arr = new string[9];
                    //arr[0] = items.AfiliadoID.ToString();
                    var i = items.AfiliadoM.AfiliadoIndice.ToString();

                    while (i.Length < 8)
                    {
                        i = "0" + i;
                    }
                    if (i.Length > 4)
                    {
                        i = "7777-2552-" + i.Substring(0, 4) + "-" + i.Substring(4, 4);
                    }
                    arr[0] = i;
                    arr[1] = items.AfiliadoM.Cedula;
                    arr[2] = items.AfiliadoM.Nombres + " " + items.AfiliadoM.Apellido1 + " " + items.AfiliadoM.Apellido2;
                    var dia = items.FechaInicio.Day.ToString();
                    var mes = items.FechaInicio.Month.ToString();
                    var year = items.FechaInicio.Year.ToString();
                    arr[3] = dia+"-"+mes+"-"+year;
                    var dia2 = items.FechaFin.Day.ToString();
                    var mes2 = items.FechaFin.Month.ToString();
                    var year2 = items.FechaFin.Year.ToString();
                    arr[4] = dia2+"-"+mes2+"-"+year2;
                    var hrefdex = "'/Afiliados/Editar/" + items.AfiliadoM.AfiliadoID.ToString() + "'";
                    var btndex = "<a href=" + hrefdex + " class=''>Renovar</a>";
                    arr[5] = btndex;
                    listf.Add(arr);
                }

            }
            var displayedCompanies = listaf
                        .Skip(param.iDisplayStart)
                        .Take(param.iDisplayLength);
            //var valvis = ((displayedCompanies.Count()==0) ? displayedCompanies.Count() : param.iDisplayLength);
            var valvis = param.iDisplayLength;

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = valvis,
                iTotalDisplayRecords = ret.Count(),
                aaData = listf.Skip(param.iDisplayStart).Take(param.iDisplayLength)
                //new List<string[]>() {
                //new string[] {"1", "Microsoft", "Redmond", "USA"},
                //new string[] {"2", "Google", "Mountain View", "USA"},
                //new string[] {"3", "Gowi", "Pancevo", "Serbia"}
                //}
            }, JsonRequestBehavior.AllowGet);
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
