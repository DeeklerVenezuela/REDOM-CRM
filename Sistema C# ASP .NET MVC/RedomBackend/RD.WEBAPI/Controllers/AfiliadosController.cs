using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using RD.Data.DAL;
using RD.Data.Models;
using RD.WEBAPI.Models;

namespace RD.WEBAPI.Controllers
{
    public class AfiliadosController : Controller
    {
        private DbContextRD db = new DbContextRD();
        private Boolean returRepor = true;
        // GET: Afiliados
        public async Task<ActionResult> Index()
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso(this.ControllerContext.RouteData.Values["controller"].ToString(), 1) == false)
                return RedirectToAction("Error", "Home");
            ViewBag.Ex = ControllerHelpers.GetPermiso(this.ControllerContext.RouteData.Values["controller"].ToString(), 4);
            //List<Afiliado> listor = new List<Afiliado>();
            //var ret = await db.Afiliados.ToListAsync();
            //if (ret != null)
            //{
            //    foreach (var items in ret)
            //    {
            //        Afiliado afitoRet = new Afiliado();
            //        afitoRet.AfiliadoID = items.AfiliadoID;
            //        afitoRet.AfiliadoIndice = items.AfiliadoIndice;
            //        afitoRet.Nombres = items.Nombres;
            //        afitoRet.Apellido1 = items.Apellido1;
            //        afitoRet.Apellido2 = items.Apellido2;
            //        afitoRet.RazonSocial = items.RazonSocial;
            //        afitoRet.RNC = items.RNC;
            //        afitoRet.Cedula = items.Cedula;
            //        afitoRet.Status = items.Status;
            //        listor.Add(afitoRet);
            //    }
            //}
            
           //return View(db.Afiliados.Take(2));
           return View();
        }

        // GET: Afiliados/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso(this.ControllerContext.RouteData.Values["controller"].ToString(), 1) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Afiliado afiliado = await db.Afiliados.FindAsync(id);
             Afiliado afiliado = db.Afiliados.Include(y => y.Emails).Include(h => h.Telefonos)
                .FirstOrDefault(x => x.AfiliadoID == id);
            if (afiliado == null)
            {
                return HttpNotFound();
            }
            
                ViewBag.Report = false;
           
           
            return View(afiliado);
        }

        public ActionResult TarjetasDetails(int idtarjeta)
        {
            
            List<RD.WEBAPI.Models.TarjetasDetails> listTarD = new List<Models.TarjetasDetails>();
            var resp = db.PagosReservas.Where(x => x.Tarjeta.TarjetaUsuarioID == idtarjeta).ToList();
            if (resp != null)
            {
                foreach (var iresp in resp)
                {
                    var DetailsTarjeta = new RD.WEBAPI.Models.TarjetasDetails();
                    DetailsTarjeta.Fecha = iresp.FechaHora;
                    DetailsTarjeta.Monto = iresp.Monto;
                    DetailsTarjeta.Url = "/Reservas/Details/" + iresp.Reserva.ReservaID.ToString();
                    var nombreHotel = db.Reservas.Where(x=>x.ReservaID == iresp.Reserva.ReservaID).Include(y=>y.Hotel).FirstOrDefault();
                    DetailsTarjeta.Concepto = (nombreHotel.Hotel != null) ? nombreHotel.Hotel.Nombre : "";
                    DetailsTarjeta.Tipo = "RESERVA";
                    listTarD.Add(DetailsTarjeta);
                }
            }
            var memp = db.Pagos.Where(x => x.TarjetaUsuario.TarjetaUsuarioID == idtarjeta).ToList();
            if (memp != null)
            {
                foreach(var imemp in memp){
                    var DetailsTarjeta = new RD.WEBAPI.Models.TarjetasDetails();
                    DetailsTarjeta.Fecha = imemp.FechaHora;
                    DetailsTarjeta.Monto = imemp.MontoPago;
                    DetailsTarjeta.Url = "/Pagos/Details/" + imemp.PagoID.ToString();
                    DetailsTarjeta.Concepto = imemp.Comentario;
                    DetailsTarjeta.Tipo = "MEMBRESÍA";
                    listTarD.Add(DetailsTarjeta);
                }
            }
            return View(listTarD);
        }
        public Boolean returnRepor()//-------------------------------------------------------
        {
            return this.returRepor;
        }

        public ActionResult QuickCreate(int id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso(this.ControllerContext.RouteData.Values["controller"].ToString(), 0) == false)
                return RedirectToAction("Error", "Home");

            var Prospecto = db.Prospectos
                .Where(x => x.ProspectoID == id)
                .FirstOrDefault();

            AfiliadosViewModel Afiliado = new AfiliadosViewModel();

            List<Direccion> Direcciones = new List<Direccion>();
            List<TarjetaUsuario> Tarjetas = new List<TarjetaUsuario>();
            List<Telefono> Telefonos = new List<Data.Models.Telefono>();
            List<EmailUsuario> Emails = new List<EmailUsuario>();

            if (Prospecto.Telefonos.Count > 0)
            {
                foreach (var i in Prospecto.Telefonos)
                {
                    Telefono Item = new Telefono();//Telefono para llenar lista en blanco
                    Item.Descripcion = i.Descripcion;
                    Item.Extension = i.Extension;
                    Item.Tipo = i.Tipo;
                    Telefonos.Add(Item);
                }
            }
            else
            {
                Telefono Item = new Telefono();//Telefono para llenar lista en blanco
                Telefonos.Add(Item);
            }
            
            if (Prospecto.Direcciones.Count > 0)
            {
                foreach (var i in Prospecto.Direcciones)
                {
                    Direccion Item2 = new Direccion();//Direccion para llenar lista en blanco
                    Item2.Descripcion = i.Descripcion;
                    Item2.Tipo = i.Tipo;
                    Direcciones.Add(Item2);
                }
            }
            else
            {
                Direccion Dr = new Direccion();
                Direcciones.Add(Dr);
            }

            TarjetaUsuario Item3 = new TarjetaUsuario();//Direccion para llenar lista en blanco
            Tarjetas.Add(Item3);

            EmailUsuario Item4 = new EmailUsuario();
            Item4.Email = (Prospecto.Email != null ? Prospecto.Email : "");
            Emails.Add(Item4);

            ViewBag.Bancos = RD.Business.Core.Banco.GetBancos();//Buscar todos los bancos
            ViewBag.Tarjetas = RD.Business.Core.Tarjeta.GetTarjetas();//Buscar todas las Tarjetas
            ViewBag.Estados = RD.Business.Core.Afiliados.GetEstados();//Buscar estados de usuarios

            Afiliado.Afiliado = GenerateSfiliadoFromProspecto(Prospecto);
            Afiliado.Tarjetas = new List<RD.Data.Models.TarjetaUsuario>();
            Afiliado.Direcciones = new List<RD.Data.Models.Direccion>();
            Afiliado.Emails = new List<RD.Data.Models.EmailUsuario>();

            Afiliado.Telefonos = Telefonos;
            Afiliado.Direcciones = Direcciones;
            Afiliado.Tarjetas = Tarjetas;
            Afiliado.Emails = Emails;

            ViewBag.TipoTel = new List<SelectListItem>();
            SelectListItem sle = new SelectListItem();
            sle.Text = "CASA";
            sle.Value = "CASA";
            ViewBag.TipoTel.Add(sle);
            SelectListItem sle2 = new SelectListItem();
            sle2.Text = "MOVIL";
            sle2.Value = "MOVIL";
            ViewBag.TipoTel.Add(sle2);
            SelectListItem sle3 = new SelectListItem();
            sle3.Text = "OFICINA";
            sle3.Value = "OFICINA";
            ViewBag.TipoTel.Add(sle3);
            SelectListItem sle4 = new SelectListItem();
            sle4.Text = "OTRO";
            sle4.Value = "OTRO";
            ViewBag.TipoTel.Add(sle4);

            var MA = db.Empresas.Select(x => x.MesesAdicionales).FirstOrDefault();
            ViewBag.MesesAdicionales = MA;
            var TA = db.Empresas.Select(x => x.TarjetasAdicionales).FirstOrDefault();
            ViewBag.TarjetasAdicionales = TA;

            var Membresias = db.Membresias.ToList();
            List<Membresia> SL = new List<Membresia>();
            foreach (var i in Membresias)
            {
                string text =  i.Nombre + " | $" + i.Costo.ToString() + " | " + i.Duracion.ToString() + "Mes(es)";
                Membresia mb = new Membresia();
                mb = i;
                mb.Nombre = text;
                SL.Add(mb);
            }
                
           
            ViewBag.Membresias = SL;
            return View("Create", Afiliado);
        }


        // GET: Afiliados/Create
        public ActionResult Create(RD.WEBAPI.Models.AfiliadosViewModel Af)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso(this.ControllerContext.RouteData.Values["controller"].ToString(), 0) == false)
                return RedirectToAction("Error", "Home");

            RD.WEBAPI.Models.AfiliadosViewModel Afiliado = new RD.WEBAPI.Models.AfiliadosViewModel();
            
                List<Direccion> Direcciones = new List<Direccion>();
                List<TarjetaUsuario> Tarjetas = new List<TarjetaUsuario>();
                List<Telefono> Telefonos = new List<Data.Models.Telefono>();
                List<EmailUsuario> Emails = new List<EmailUsuario>();
                Empleado userWelcome = new Empleado();

                Telefono Item = new Telefono();//Telefono para llenar lista en blanco
                Telefonos.Add(Item);

                Direccion Item2 = new Direccion();//Direccion para llenar lista en blanco
                Direcciones.Add(Item2);

                TarjetaUsuario Item3 = new TarjetaUsuario();//Direccion para llenar lista en blanco
                Tarjetas.Add(Item3);

                EmailUsuario Item4 = new EmailUsuario();
                Emails.Add(Item4);



                ViewBag.UsersWelcome = RD.Business.Core.Empleado.GetEmpleados();
                ViewBag.Bancos = RD.Business.Core.Banco.GetBancos();//Buscar todos los bancos
                ViewBag.Tarjetas = RD.Business.Core.Tarjeta.GetTarjetas();//Buscar todas las Tarjetas
                ViewBag.Estados = RD.Business.Core.Afiliados.GetEstados();//Buscar estados de usuarios
                //ViewBag.costoMembresia = RD.Business.Core.Membresia.GetMembresia ();//Buscar estados de membresia    

                Afiliado.Afiliado = new RD.Data.Models.Afiliado();
                Afiliado.Tarjetas = new List<RD.Data.Models.TarjetaUsuario>();
                Afiliado.Direcciones = new List<RD.Data.Models.Direccion>();
                Afiliado.Emails = new List<RD.Data.Models.EmailUsuario>();
                Afiliado.Afiliado.UsuarioBienvenida = userWelcome;
                Afiliado.Telefonos = Telefonos;
                Afiliado.Direcciones = Direcciones;
                Afiliado.Tarjetas = Tarjetas;
                Afiliado.Emails = Emails;

                ViewBag.TipoTel = new List<SelectListItem>();
                SelectListItem sle = new SelectListItem();
                sle.Text = "CASA";
                sle.Value = "CASA";
                ViewBag.TipoTel.Add(sle);
                SelectListItem sle2 = new SelectListItem();
                sle2.Text = "MOVIL";
                sle2.Value = "MOVIL";
                ViewBag.TipoTel.Add(sle2);
                SelectListItem sle3 = new SelectListItem();
                sle3.Text = "OFICINA";
                sle3.Value = "OFICINA";
                ViewBag.TipoTel.Add(sle3);
                SelectListItem sle4 = new SelectListItem();
                sle4.Text = "OTRO";
                sle4.Value = "OTRO";
                ViewBag.TipoTel.Add(sle4);
                var MA = db.Empresas.Select(x => x.MesesAdicionales).FirstOrDefault();
                ViewBag.MesesAdicionales = MA;
                var TA = db.Empresas.Select(x => x.TarjetasAdicionales).FirstOrDefault();
                ViewBag.TarjetasAdicionales = TA;

                ViewBag.Membresias = db.Membresias.ToList();               
                


            return View(Afiliado);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(RD.WEBAPI.Models.AfiliadosViewModel model)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso(this.ControllerContext.RouteData.Values["controller"].ToString(), 0) == false)
                return RedirectToAction("Error", "Home");

            //returRepor = true;
            ViewBag.Report = false;
            int auxE = Int32.Parse(user);
            //model.Afiliado.UsuarioRegistro.EmpleadoID = auxE;
           // model.Afiliado.UsuarioRegistro = db.Empleados.Where(x => x.EmpleadoID == auxE).FirstOrDefault();
            model.Afiliado.FechaVencimiento = DateTime.Now.AddMonths(model.Afiliado.CostoMembresia);
            var ced = db.Afiliados.FirstOrDefault(x=>x.Cedula == model.Afiliado.Cedula);
            if(ced == null){
                RD.Business.Core.Afiliados.AddAfiliado(model.Afiliado, model.Direcciones, model.Tarjetas, model.Telefonos, model.Emails, model.TarjetasAdicionales, auxE);
                var a=db.Afiliados.Where(x=>x.Cedula == model.Afiliado.Cedula).FirstOrDefault();
                ViewBag.Report = true;
                return View("Details",a);

            }
                           


            //MembresiaVence mva = new MembresiaVence()
            //{
            //    AfiliadoM = model.Afiliado,
            //    FechaInicio = model.Afiliado.FechaHora,
            //    FechaFin = model.Afiliado.FechaVencimiento,
            //    Called = true
            //};
            //db.MembresiaVences.Add(mva);
            db.SaveChanges();
            return View("Details", ced);
            //return RedirectToAction("Details", "Afiliados", new {id= model.Afiliado.AfiliadoID });

        }


        // GET: Afiliados/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso(this.ControllerContext.RouteData.Values["controller"].ToString(), 2) == false)
                return RedirectToAction("Error", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afiliado afiliado = await db.Afiliados.FindAsync(id);
            if (afiliado == null)
            {
                return HttpNotFound();
            }

            RD.WEBAPI.Models.AfiliadosViewModel Afiliado = new RD.WEBAPI.Models.AfiliadosViewModel();

            List<Direccion> Direcciones = new List<Direccion>();
            List<TarjetaUsuario> Tarjetas = new List<TarjetaUsuario>();
            List<Telefono> Telefonos = new List<Data.Models.Telefono>();
            List<EmailUsuario> Emails = new List<EmailUsuario>();


            Telefono Item = new Telefono();//Telefono para llenar lista en blanco
            Telefonos.Add(Item);

            Direccion Item2 = new Direccion();//Direccion para llenar lista en blanco
            Direcciones.Add(Item2);

            TarjetaUsuario Item3 = new TarjetaUsuario();//Direccion para llenar lista en blanco
            Tarjetas.Add(Item3);

            EmailUsuario Item4 = new EmailUsuario();
            Emails.Add(Item4);




            ViewBag.Bancos = RD.Business.Core.Banco.GetBancos();//Buscar todos los bancos
            ViewBag.Tarjetas = RD.Business.Core.Tarjeta.GetTarjetas();//Buscar todas las Tarjetas
            ViewBag.Estados = RD.Business.Core.Afiliados.GetEstados();//Buscar estados de usuarios
            //ViewBag.costoMembresia = RD.Business.Core.Membresia.GetMembresia ();//Buscar estados de membresia    
            ViewBag.Selected = "Selected ";
            var aux = db.Afiliados.Find(id);

            Afiliado.Afiliado = new RD.Data.Models.Afiliado();
            Afiliado.Tarjetas = new List<RD.Data.Models.TarjetaUsuario>();
            Afiliado.Direcciones = new List<RD.Data.Models.Direccion>();
            Afiliado.Emails = new List<RD.Data.Models.EmailUsuario>();
            Afiliado.TarjetasAdicionales = new List<TarjetaAdicional>();

            Afiliado.Afiliado = aux;
            Afiliado.Telefonos = aux.Telefonos;
            Afiliado.Direcciones = aux.Direcciones;
            Afiliado.Tarjetas = aux.Tarjetas;
            Afiliado.Emails = aux.Emails;
            Afiliado.TarjetasAdicionales = aux.TarjetasAdicionales;

            ViewBag.TipoTel = new List<string>();
            ViewBag.TipoTel.Add("CASA");
            ViewBag.TipoTel.Add("OFICINA");
            ViewBag.TipoTel.Add("MOVIL");
            ViewBag.TipoTel.Add("OTRO");
            var MA = db.Empresas.Select(x => x.MesesAdicionales).FirstOrDefault();
            ViewBag.MesesAdicionales = MA;
            var TA = db.Empresas.Select(x => x.TarjetasAdicionales).FirstOrDefault();
            ViewBag.TarjetasAdicionales = TA;

            ViewBag.Membresias = db.Membresias.ToList();               
            return View(Afiliado);
        }

        // POST: Afiliados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Afiliado,Telefonos,Direcciones,Tarjetas,Emails,TarjetasAdicionales,FechaVencimiento")] AfiliadosViewModel model)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso(this.ControllerContext.RouteData.Values["controller"].ToString(), 2) == false)
                return RedirectToAction("Error", "Home");

            if (ModelState.IsValid)
            {
                List<Direccion> Direcciones = new List<Direccion>();
                List<Telefono> Telefonos = new List<Telefono>();
                List<TarjetaUsuario> Tarjetas = new List<TarjetaUsuario>();
                List<TarjetaAdicional> TarjetasAdd = new List<TarjetaAdicional>();
                List<EmailUsuario> Emails = new List<EmailUsuario>();
                List<Pago>Pagos = new List<Pago>();                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
                //Borrado anidado --- guarda en listas

                var AFI = model.Afiliado;
                model.Afiliado = db.Afiliados.Where(x => x.AfiliadoID == model.Afiliado.AfiliadoID).FirstOrDefault();
                model.Afiliado.Apellido1 = AFI.Apellido1;
                model.Afiliado.Apellido2 = AFI.Apellido2;
                model.Afiliado.CostoMembresia = AFI.CostoMembresia;
                model.Afiliado.Cumpleanio = AFI.Cumpleanio;
                model.Afiliado.MesAdicionalCan = AFI.MesAdicionalCan;
                model.Afiliado.MesesAdicionales = AFI.MesesAdicionales;
                model.Afiliado.Nombres = AFI.Nombres;
                model.Afiliado.Observaciones = AFI.Observaciones;
                model.Afiliado.RazonSocial = AFI.RazonSocial;
                model.Afiliado.RNC = AFI.RNC;
                model.Afiliado.Status = AFI.Status;
                model.Afiliado.TarjetaAdicional = AFI.TarjetaAdicional;
                model.Afiliado.FechaVencimiento = AFI.FechaVencimiento;
                model.Afiliado.TarjetaAdicionalCan = (model.TarjetasAdicionales == null) ? 0 : model.TarjetasAdicionales.Count;

                foreach (var i in model.Afiliado.Direcciones)
                    Direcciones.Add(i);

                foreach (var j in model.Afiliado.Telefonos)
                    Telefonos.Add(j);

                foreach (var k in model.Afiliado.Tarjetas)
                {
                        Tarjetas.Add(k);
                }
                
                foreach (var ttp in Tarjetas)
                {
                   var pg = db.Pagos.Where(x => x.TarjetaUsuario.TarjetaUsuarioID == ttp.TarjetaUsuarioID).ToList();
                   foreach (var pgr in pg)
                   {
                      // db.Pagos.Remove(pgr);
                       Pagos.Add(pgr);

                   }
                    
                }    
                foreach (var l in model.Afiliado.TarjetasAdicionales)
                    TarjetasAdd.Add(l);

                foreach (var m in model.Afiliado.Emails)
                    Emails.Add(m);

                //Boorado Definitivo
                if (Direcciones != null)
                {
                    foreach (var ii in Direcciones)
                        db.Direcciones.Remove(ii);
                }

                if (Telefonos != null)
                {
                    foreach (var jj in Telefonos)
                        db.Telefonos.Remove(jj);
                }
                List<Pago> tpr = new List<Pago>();
                if (Tarjetas != null)
                {
                    foreach (var kk in Tarjetas)
                    {
                         tpr = db.Pagos.Where(x => x.TarjetaUsuario.TarjetaUsuarioID == kk.TarjetaUsuarioID).ToList();
                       // tpr = db.Pagos.Where(x => x.TarjetaUsuario.TarjetaUsuarioID == kk.TarjetaUsuarioID && x.Afiliado.AfiliadoID == model.Afiliado.AfiliadoID).ToList();
                        foreach (var ptpr in tpr) {
                            db.Pagos.Remove(ptpr);
                        }
                        db.TarjetasUsuarios.Remove(kk);
                    }
                        
                }

                if (TarjetasAdd != null)
                {
                    foreach(var ll in TarjetasAdd)
                        db.TarjetaAdicional.Remove(ll);
                }

                if (Emails != null)
                {
                    foreach (var mm in Emails)
                        db.Emails.Remove(mm);
                }

                db.SaveChanges();

                List<TarjetaUsuario> TU = new List<TarjetaUsuario>();
                model.Afiliado.Tarjetas = TU;
                //Agrego las nuevas
                foreach (var a in model.Tarjetas)
                {
                    if(!String.IsNullOrEmpty(a.NumeroTarjeta))
                        model.Afiliado.Tarjetas.Add(a);
                    foreach (var pf in tpr)
                    {
                        Pago mva = new Pago()
                        {
                            Afiliado = model.Afiliado,
                            CostoMembresia = model.Afiliado.CostoMembresia,
                            FechaHora = pf.FechaHora,
                            MontoPago = pf.MontoPago,
                            Comentario = pf.Comentario,
                            Comprobante = pf.Comprobante,
                            Empleado = pf.Empleado
                        };
                        pf.TarjetaUsuario = a;
                        db.Pagos.Add(mva);
                        
                    }
                }

                if (model.Telefonos != null)
                {
                    foreach (var b in model.Telefonos)
                        model.Afiliado.Telefonos.Add(b);
                }

                if (model.Direcciones != null)
                {
                    foreach (var c in model.Direcciones)
                        model.Afiliado.Direcciones.Add(c);
                }
                
                if (model.TarjetasAdicionales != null)
                {
                    foreach (var d in model.TarjetasAdicionales)
                        model.Afiliado.TarjetasAdicionales.Add(d);
                }

                if (model.Emails != null)
                {
                    foreach (var e in model.Emails)
                        model.Afiliado.Emails.Add(e);
                }
                var amv = db.MembresiaVences.Where(a => a.AfiliadoM.AfiliadoID == model.Afiliado.AfiliadoID).FirstOrDefault();
                
                if (amv != null) { 
                    int FechaCompar = DateTime.Compare(amv.FechaFin, model.Afiliado.FechaVencimiento);
                    
                    if (FechaCompar < 0)
                    {
                        amv.FechaInicio = DateTime.Now;
                        amv.FechaFin = model.Afiliado.FechaVencimiento;
                        amv.Called = true;
                        db.Entry(amv).State = EntityState.Modified;
                       
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                  

            return View(model);
        }

        // GET: Afiliados/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso(this.ControllerContext.RouteData.Values["controller"].ToString(), 3) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Afiliado afiliado = await db.Afiliados.FindAsync(id);
            if (afiliado == null)
            {
                return HttpNotFound();
            }
            return View(afiliado);
        }

        // POST: Afiliados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso(this.ControllerContext.RouteData.Values["controller"].ToString(), 0) == false)
                return RedirectToAction("Error", "Home");

            
            
            Afiliado afiliado = await db.Afiliados.FindAsync(id);
            //-------------------
            List<Direccion> Direcciones = new List<Direccion>();
            List<Telefono> Telefonos = new List<Telefono>();
            List<TarjetaUsuario> Tarjetas = new List<TarjetaUsuario>();
            List<TarjetaAdicional> TarjetasAdd = new List<TarjetaAdicional>();
            List<EmailUsuario> Emails = new List<EmailUsuario>();
            //Borrado anidado --- guarda en listas
            foreach (var i in afiliado.Direcciones)
                Direcciones.Add(i);

            foreach (var j in afiliado.Telefonos)
                Telefonos.Add(j);

            foreach (var k in afiliado.Tarjetas)
                Tarjetas.Add(k);

            foreach (var l in afiliado.TarjetasAdicionales)
                TarjetasAdd.Add(l);

            foreach (var m in afiliado.Emails)
                Emails.Add(m);

            //Boorado Definitivo
            if (Direcciones != null)
            {
                foreach (var ii in Direcciones)
                    db.Direcciones.Remove(ii);
            }

            if (Telefonos != null)
            {
                foreach (var jj in Telefonos)
                    db.Telefonos.Remove(jj);
            }

            if (Tarjetas != null)
            {
                foreach (var kk in Tarjetas)
                    db.TarjetasUsuarios.Remove(kk);
            }

            if (TarjetasAdd != null)
            {
                foreach (var ll in TarjetasAdd)
                    db.TarjetaAdicional.Remove(ll);
            }
            
            //-------------------------
            db.Afiliados.Remove(afiliado);
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

        //Crear nueva tarjeta de credito a traves de una vista parcial
        public ActionResult CreateNewTarjeta(int Tarjetas)
        {

            ViewBag.Bancos = db.Bancos.ToList();
            ViewBag.Tarjetas = db.Tarjetas.ToList();
            ViewBag.Num = Tarjetas;
            RD.Data.Models.TarjetaUsuario Model = new TarjetaUsuario();
            return PartialView("~/Views/Partial_Views/_NewTarjeta.cshtml", Model);
        }

        //Crear nuevo telefono a traves de una vista parcial
        public ActionResult CreateNewTelefono(int Telefonos)
        {
            ViewBag.Num = Telefonos;
            RD.Data.Models.Telefono Model = new Telefono();
            return PartialView("~/Views/Partial_Views/_NewTelefono.cshtml", Model);
        }

        //Crear nueva direccion a traves de una vista parcial
        public ActionResult CreateNewDireccion(int Direcciones)
        {
            ViewBag.Num = Direcciones;
            RD.Data.Models.Direccion Model = new Direccion();
            return PartialView("~/Views/Partial_Views/_NewDireccion.cshtml", Model);
        }

        public ActionResult CreateNewEmail(int Emails)
        {
            ViewBag.Num = Emails;
            RD.Data.Models.EmailUsuario Model = new EmailUsuario();
            return PartialView("~/Views/Partial_Views/_NewEmail.cshtml", Model);
        }

        public ActionResult CreateNewTarjetaAdicional(int Tarjetas)
        {
            ViewBag.Num = Tarjetas;
            List<TarjetaAdicional> Model = new List<TarjetaAdicional>();
            for (var i = 0; i < Tarjetas; i++)
            {
                TarjetaAdicional t = new TarjetaAdicional();
                Model.Add(t);
            }
            return PartialView("~/Views/Partial_Views/_NewTarjetaAdicional.cshtml", Model);
        }

        private Afiliado GenerateSfiliadoFromProspecto(Prospecto Pr)
        {

            Afiliado Af = new Afiliado();
            Af.Apellido1 = Pr.Apellido1;
            Af.Apellido2 = Pr.Apellido2;
            Af.Cedula = Pr.Cedula;
            Af.Cumpleanio = Pr.FechaDeNacimiento;
            //foreach (var i in Pr.Direcciones)
            //{
            //    Af.Direcciones.Add(i);
            //}
            Af.Email = Pr.Email;
            Af.Nombres = Pr.Nombres;
            Af.Observaciones = Pr.Comentario;
            //foreach (var j in Pr.Telefonos)
            //{
            //    Af.Telefonos.Add(j);
            //}

            return Af;

        }

        public ActionResult DetailsEx(int? id)
        {
            var user = ControllerHelpers.GetString("user");
            if (String.IsNullOrEmpty(user))
            {
                return RedirectToAction("Index", "Login");
            }

            if (ControllerHelpers.GetPermiso(this.ControllerContext.RouteData.Values["controller"].ToString(), 4) == false)
                return RedirectToAction("Error", "Home");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Afiliado AfiliadoEx = db.Afiliados.Include(y=>y.Emails).Include(z=>z.Tarjetas).Include(h=>h.Telefonos)
                .FirstOrDefault(x=>x.AfiliadoID == id);

             
            if (AfiliadoEx == null)
            {
                return HttpNotFound();
            }

            return View(AfiliadoEx);
        }

        //public ActionResult GenerarReporteDeAfiliacion(int id)
        //{
        //    var AF = db.Afiliados.FirstOrDefault();
        //    AF.AfiliadoIndice = 2345;
        //    ReportClass rptH = new ReportClass();
        //    rptH.FileName = Server.MapPath("/Report/RPAfiliado.rpt");
        //    rptH.Load();
        //    rptH.SetDataSource(AF);
        //    Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    return File(stream, "application/pdf");
        //}

        public JsonResult GetSpecialsPermissions(string Tipo, string User, string Password)
        {
            bool Result = false;
            if (String.IsNullOrEmpty(Tipo) || String.IsNullOrEmpty(User) || String.IsNullOrEmpty(Password))
                return Json(Result, JsonRequestBehavior.AllowGet);
            else
            {
                var log = db.Logins.Where(x => x.UserName == User).FirstOrDefault();
                if (log == null)
                    return Json(Result, JsonRequestBehavior.AllowGet);
                bool log_result = Hash(Password, log.Password);
                if (log_result == true && User == "ADMIN")
                {
                    Result = true;
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                if(log_result == false)
                    return Json(Result, JsonRequestBehavior.AllowGet);
                var per = db.UserPermissions.Where(x => x.Empleado.EmpleadoID == log.Empleado.EmpleadoID).FirstOrDefault();
                //Hasta ahora solo veiamos quien hacia la solicitud, ahora veamos si tiene permisos
                Tipo = Tipo.ToUpper();
                if (Tipo == "MESES")
                    Result = per.Especiales[0] == '1' ? true : false;
                if (Tipo == "TARJETAS")
                    Result = per.Especiales[1] == '1' ? true : false;
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllAfiAsy(jQueryDataTableParamModel param)
        {
            List<Afiliado> listaf = new List<Afiliado>();
            //var ret = db.Afiliados.ToList();
            var ret = db.MembresiaVences.Include(y => y.AfiliadoM).Where(x => x.FechaFin >= DateTime.Now).Select(z=>z.AfiliadoM).ToList();
            if (!string.IsNullOrEmpty(param.sSearch))
            {
                ret = db.MembresiaVences.Include(y => y.AfiliadoM).Where(x => x.FechaFin >= DateTime.Now && (x.AfiliadoM.Cedula.Contains(param.sSearch) || x.AfiliadoM.Apellido1.Contains(param.sSearch) || x.AfiliadoM.Apellido2.Contains(param.sSearch) || x.AfiliadoM.Nombres.Contains(param.sSearch) || x.AfiliadoM.RNC.Contains(param.sSearch) || x.AfiliadoM.RazonSocial.Contains(param.sSearch))).Select(z => z.AfiliadoM).ToList();
                //ret = db.Afiliados.Where(x => x.Cedula.Contains(param.sSearch) || x.Nombres.Contains(param.sSearch) || x.Apellido1.Contains(param.sSearch) || x.Apellido2.Contains(param.sSearch) || x.RNC.Contains(param.sSearch) || x.RazonSocial.Contains(param.sSearch)).ToList();
            }
            List<string[]> listf = new List<string[]>();
            if (ret != null)
            {
                foreach (var items in ret)
                {
                    string[] arr = new string[7];
                    //arr[0] = items.AfiliadoID.ToString();
                    var i = items.AfiliadoIndice.ToString();
                    
                    while (i.Length < 8)
                    {
                        i = "0" + i;
                    }
                    if (i.Length > 4)
                    {
                        i = "7777-2552-" + i.Substring(0, 4) + "-" + i.Substring(4, 4);
                    }
                    arr[0] = i;
                    arr[1] = items.Cedula;
                    arr[2] = items.Nombres+" "+items.Apellido1+" "+items.Apellido2;
                    arr[3] = items.RazonSocial;
                    arr[4] = items.RNC;
                    arr[5] = items.Status;
                    var hrefEdit = "'/Afiliados/Edit/"+ items.AfiliadoID.ToString()+"'";
                    var btnEditar = "<a href="+hrefEdit+" class=''>Editar</a>";
                    var hrefdex = "'/Afiliados/DetailsEx/" + items.AfiliadoID.ToString() + "'";
                    var btndex = "<a href=" + hrefdex + " class=''>Detalles +</a>";
                    var hrefdet = "'/Afiliados/Details/" + items.AfiliadoID.ToString() + "'";
                    var btndet = "<a href=" + hrefdet + " class=''>Detalles</a>";
                    var hrefdel = "'/Afiliados/Delete/" + items.AfiliadoID.ToString() + "'";
                    var btndel = "<a href=" + hrefdel + " class=''>Eliminar</a>";
                    arr[6] = btnEditar+"|"+btndet + "|" + btndex  + "|" + btndel;
                    listf.Add(arr);
                }
                
            }
            var displayedCompanies = listaf
                        .Skip(param.iDisplayStart)
                        .Take(param.iDisplayLength); 
            //var valvis = ((displayedCompanies.Count()==0) ? displayedCompanies.Count() : param.iDisplayLength);
            var valvis = param.iDisplayLength;

            return Json(new{
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
        public JsonResult GetFechaVencimiento(int id)
        {
            var memb = db.Membresias.Find(id);
            if (memb != null)
            {
                var venc = DateTime.Now.AddMonths(memb.Duracion);
                var result = venc.Day + "/" + venc.Month + "/" + venc.Year;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            
            return Json(DateTime.Now.AddMonths(1), JsonRequestBehavior.AllowGet);
        }

        static bool Hash(string p1, string p2Hashed)
        {
            var result = Repository.Hash.ValidarClave(p1, p2Hashed);
            return result;
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
    }
}
