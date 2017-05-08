using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RD.WEBAPI.Controllers
{
    public class HomeController : Controller
    {
        private RD.Data.DAL.DbContextRD db = new Data.DAL.DbContextRD();
        public ActionResult Index()
        {
            var user = ControllerHelpers.GetString("USER");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }
            var nombre = ControllerHelpers.GetString("NOMBRE");
            ViewBag.Empresa = db.Empresas.Select(x => x.Nombre).FirstOrDefault();
            ViewBag.User = user;
            ViewBag.Nombre = nombre;

            return View();
        }

        public  Boolean PintarMemb()
        {
            bool res = false;
            var search = db.MembresiaVences.Where(x => x.Called == false).ToList();
            if (search.Count() > 0)
            {
                res = true;
            }
            return res;
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
