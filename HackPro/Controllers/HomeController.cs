using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using HackPro.Models;

namespace HackPro.Controllers
{
    public class HomeController : Controller
    {
        public string getMonthInWords(int Month)
        {
            switch (Month)
            {
                case 1:
                    return "Ene.";
                    break;
                case 2:
                    return "Feb.";
                    break;
                case 3:
                    return "Marzo";
                    break;
                case 4:
                    return "Abril";
                    break;
                case 5:
                    return "Mayo";
                    break;
                case 6:
                    return "Junio";
                    break;
                case 7:
                    return "Julio";
                    break;
                case 8:
                    return "Ago.";
                    break;
                case 9:
                    return "Seot.";
                    break;
                case 10:
                    return "Oct.";
                    break;
                case 11:
                    return "Nov.";
                    break;
                default:
                    return "Dec.";
            }
        }

        // GET: Home
        public ActionResult LogOut()
        {
            Session["UserId"] = null;
            Session["Name"] = null;
            Session["Username"] = null;
            Session["Correo"] = null;
            Session["Ocupacion"] = null;
            Session["Since"] = null;
            Session["Admin"] = null;
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Index()
        {
            if (Session["UserId"] != null)
                return RedirectToAction("Index", "Logged");
            else
                return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Usuarios user)
        {
            if (ModelState.IsValid)
            {
                hackprodb_1Entities db = new hackprodb_1Entities();
                var exist = db.tbl_usuario.Where(a => a.tbl_usuario_correo == user.correo);
                if (exist.Any())
                {
                    ModelState.AddModelError("correo", "Existe un usuario con el mismo correo");
                }
                else
                {
                    var existuser = db.tbl_usuario.Where(a => a.tbl_usuario_username == user.username);
                    if (existuser.Any())
                    {
                        ModelState.AddModelError("username", "Existe un usuario con el mismo username");
                    }
                    else
                    {
                        if (user.password != user.r_password)
                        {
                            ModelState.AddModelError("r_password", "Existe un usuario con el mismo correo");
                        }
                        else
                        {
                            var usuario = new tbl_usuario();
                            usuario.tbl_usuario_correo = user.correo;
                            usuario.tbl_usuario_username = user.username;
                            usuario.tbl_usuario_p_nombre = user.p_nombre;
                            usuario.tbl_usuario_s_nombre = user.s_nombre ?? "";
                            usuario.tbl_usuario_p_apellido = user.p_apellido;
                            usuario.tbl_usuario_s_apellido = user.s_apellido ?? "";
                            usuario.tbl_usuario_ocupacion = user.ocupacion;
                            usuario.tbl_usuario_password = user.password;
                            usuario.tbl_usuario_fecha_nac = user.fecha_nac;
                            usuario.tbl_usuario_celular = user.celular;//falta que no sea hard-coded
                            usuario.tbl_usuario_activo = true;
                            usuario.tbl_usuario_genero = user.genero;
                            usuario.tbl_usuario_fecha_crea = DateTime.Now;
                            usuario.tbl_usuario_admin = false;
                            db.tbl_usuario.Add(usuario);
                            db.SaveChanges();
                            return RedirectToAction("Login", "Home");
                        }
                    }
                    
                }

                
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login log)
        {
            if (ModelState.IsValid)
            {
                hackprodb_1Entities db = new hackprodb_1Entities();
                var login = db.tbl_usuario.Where(p => p.tbl_usuario_correo.Equals(log.email)  && p.tbl_usuario_password.Equals(log.password));
                if (login.Count() == 1)
                {
                    Session["UserId"] = login.First().tbl_usuario_id;
                    Session["Name"] = login.First().tbl_usuario_p_nombre + " " + login.First().tbl_usuario_p_apellido;
                    Session["Correo"] = log.email;
                    Session["Username"] = login.First().tbl_usuario_username;
                    Session["Ocupacion"] = login.First().tbl_usuario_ocupacion;
                    Session["Since"] = getMonthInWords(login.First().tbl_usuario_fecha_crea.Month) + " " + login.First().tbl_usuario_fecha_crea.Year;
                    Session["Admin"] = login.First().tbl_usuario_admin;
                    if(login.First().tbl_usuario_admin)
                        return RedirectToAction("Index", "Admin");
                    return RedirectToAction("Index", "Logged");
                }
                else
                {
                    ModelState.AddModelError("Password", "Email or Password not valid");
                }
            }
                     
            return View();            
        }

        public ActionResult Evento(int id)
        {
            var model = new Evento();
            var db = new hackprodb_1Entities();

            var ev = db.tbl_evento.Find(id);
            if (ev == null)
                return RedirectToAction("Error404");

            model.id = ev.tbl_evento_id;
            model.tbl_cat_evento = ev.tbl_cat_evento_id;
            model.tbl_evento_cal_jurado = ev.tbl_evento_cal_jurado;
            model.tbl_evento_cal_pueblo = ev.tbl_evento_cal_pueblo;
            model.tbl_evento_desc = ev.tbl_evento_desc;
            model.tbl_evento_duracion = ev.tbl_evento_duracion;
            model.tbl_evento_fecha_fin = ev.tbl_evento_fecha_fin;
            model.tbl_evento_fecha_inicio = ev.tbl_evento_fecha_inicio;
            model.tbl_evento_lugar = ev.tbl_evento_lugar;
            model.tbl_evento_lugar_x = ev.tbl_evento_lugar_x;
            model.tbl_evento_lugar_y = ev.tbl_evento_lugar_y;
            model.tbl_evento_nombre = ev.tbl_evento_nombre;
            model.tbl_evento_precio = ev.tbl_evento_precio;
            model.tbl_evento_presupuesto = ev.tbl_evento_presupuesto;
            model.tbl_evento_url = ev.tbl_evento_url;

            return View(model);
        }

        [HttpGet]
        public ActionResult Error404()
        {
            ViewBag.Title = "Pagina no encontrada";
            return View();
        }
    }
}