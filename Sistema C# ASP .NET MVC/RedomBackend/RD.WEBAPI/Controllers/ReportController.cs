using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa.Options;
using Rotativa;
using RD.Data.Models;
using System.Net;
using System.Threading.Tasks;
using System.Data.Entity;

namespace RD.WEBAPI.Controllers
{
    public class ReportController : Controller
    {
        private RD.Data.DAL.DbContextRD db = new Data.DAL.DbContextRD();

        // GET: /Report/
        public ActionResult Index()
        {
            return PartialView("~/Partial_Views/_AfiliadoReport.cshtml");
        }

        public ActionResult PrintIndex()
        {
            //var model = new AfiliadosController();
            return new ActionAsPdf("Index", new { name = "Giorgio" }) { FileName = "Test.pdf" };
            //return new Rotativa.ViewAsPdf("GeneratePDF", model) { FileName = "TestViewAsPdf.pdf" };
        }

        public ActionResult Report(int ids)
        {
            return new ActionAsPdf("Afiliacion", new { id = ids }) { FileName = "Afiliacion_Report.pdf" };
        }

        public ActionResult Afiliacion(int id)
        {
            Afiliado afiliado = db.Afiliados.Include(y => y.Emails).Include(j=>j.UsuarioRegistro).Include(h => h.Telefonos).Include(t=>t.TarjetasAdicionales).FirstOrDefault(x => x.AfiliadoID == id);
            afiliado.RazonSocial = afiliado.UsuarioRegistro.Nombres + " " + afiliado.UsuarioRegistro.Apellido1 + " " + afiliado.UsuarioRegistro.Apellido2;
            
            return View(afiliado);
        }

        
	}
}