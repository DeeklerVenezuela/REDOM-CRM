using RD.Data.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace RD.WEBAPI.Controllers
{
    public class LoginController : Controller
    {
        private static RD.Data.DAL.DbContextRD db = new Data.DAL.DbContextRD();
        //System.Threading.Thread sp;
        //ThreadStart delegado = new ThreadStart(SinVerguenza);
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult SessionOut()
        {
            Session["Afiliados"] = null;
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            HttpContext.Session.RemoveAll();
            return Json("true", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidateLogin([Bind(Include = "Username,Password")] Models.Login user)
        {
            bool LoginResult = false;

            
            LoginResult = RD.Business.Core.Empleado.MakeLogin(user.Username, user.Password);
            

            if (LoginResult == true)
            {
                var id = 0;
                bool c = false;
                string nombre = "";

                if(user.Username == "ADMIN"){
                    id = 1;
                    nombre = "Administrador";
                }
                    
                else{
                    id = db.Logins
                    .Where(x=>x.UserName == user.Username)
                    .Select(y=>y.Empleado.EmpleadoID)
                    .FirstOrDefault();
                    var emp = db.Logins.Where(x=>x.UserName == user.Username).FirstOrDefault();
                    if(emp != null)
                        c = Repository.Hash.ValidarClave(user.Password, emp.Password);
                    if(c==true)
                            nombre = emp.Empleado.Nombres + " " + emp.Empleado.Apellido1 + " " + emp.Empleado.Apellido2;                    
                }
                UserPermision up = new UserPermision();
                up = db.UserPermissions.Where(y => y.Empleado.EmpleadoID == id).FirstOrDefault();
                if (up != null)
                {
                    /*aca declaramos las variables de permisos*/
                    HttpContext.Session.Add("Afiliados", up.Afiliados);
                    HttpContext.Session.Add("Archivos", up.Archivos);
                    HttpContext.Session.Add("Certificados", up.Certificados);
                    HttpContext.Session.Add("Pagos", up.Cobros);
                    HttpContext.Session.Add("Documentaciones", up.Documentacion);
                    HttpContext.Session.Add("Dummies", up.Dummy);
                    HttpContext.Session.Add("Especiales", up.Especiales);
                    HttpContext.Session.Add("Hoteles", up.Hoteles);
                    HttpContext.Session.Add("Permisos", up.Permisos);
                    HttpContext.Session.Add("Personas", up.Personas);
                    HttpContext.Session.Add("Prospectos", up.Prospectos);
                    HttpContext.Session.Add("Reservas", up.Reservas);
                    HttpContext.Session.Add("Sistema", up.Sistema);
                    /*fin var de permisos*/
                }
                if (user.Username == "ADMIN")
                {
                    HttpContext.Session.Add("LOGIN", "ADMIN");
                }
                else
                {
                    HttpContext.Session.Add("LOGIN", user.Username);
                }
                HttpContext.Session.Add("USER", id.ToString());
                HttpContext.Session.Add("NOMBRE", nombre);
                //Thread hilo = new Thread(delegado);
                //hilo.Start();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Error");
            }

             
        }

        public static void SinVerguenza()
        {
            var stat = db.Status.First();

            if (stat != null)
            {
                if (DateTime.Now.DayOfYear == stat.Fecha.DayOfYear)
                {
                    var afi = db.MembresiaVences.ToList();
                    if (afi != null)
                    {
                        foreach (var a in afi)
                        {
                            if (a.FechaFin.Date <= DateTime.Now.Date)
                            {

                                a.Called = false;
                                db.Entry(a).State = EntityState.Modified;
                            }
                        }
                        stat.Fecha = DateTime.Now.AddDays(1);
                        db.Entry(stat).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
           
        }
    }
}