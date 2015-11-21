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
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
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
                            return View("Login");
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
                    
                    return View("Index");
                }
                else
                {
                    ModelState.AddModelError("Password", "Email or Password not valid");
                }
            }
                     
            return View();            
        }

        [HttpGet]
        public ActionResult CrearEvento()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearEvento(Evento even)
        {
            if (ModelState.IsValid)
            {
                var db = new hackprodb_1Entities();
                var evento = new tbl_evento();
                
                evento.tbl_evento_duracion = even.tbl_evento_duracion;
                evento.tbl_evento_precio = even.tbl_evento_precio;
                evento.tbl_usuario_id = 22;
                evento.tbl_cat_evento_id = even.tbl_cat_evento;
                evento.tbl_evento_activo = true;
                evento.tbl_evento_cal_jurado = even.tbl_evento_cal_jurado;
                evento.tbl_evento_cal_pueblo = even.tbl_evento_cal_pueblo;
                evento.tbl_evento_desc = even.tbl_evento_desc;
                evento.tbl_evento_fecha_fin = even.tbl_evento_fecha_fin;
                evento.tbl_evento_fecha_inicio = even.tbl_evento_fecha_inicio;
                evento.tbl_evento_lugar = even.tbl_evento_lugar;
                evento.tbl_evento_lugar_x = even.tbl_evento_lugar_x;
                evento.tbl_evento_lugar_y = even.tbl_evento_lugar_y;
                evento.tbl_evento_nombre = even.tbl_evento_nombre;
                evento.tbl_evento_presupuesto = even.tbl_evento_presupuesto;
                evento.tbl_evento_url = even.tbl_evento_url;

                db.tbl_evento.Add(evento);
                db.SaveChanges();
            }
            return View();
        }

        [HttpGet]
        public ActionResult CrearEquipo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearEquipo(Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                var db = new hackprodb_1Entities();
                var equipos = new tbl_equipo();

                equipos.tbl_equipo_nombre = equipo.tbl_equipo_nombre;
                equipos.tbl_equipo_activo = true;
                equipos.tbl_equipo_fecha_creacion = DateTime.Today;
                equipos.tbl_equipo_usuario_admin = 22;

                db.tbl_equipo.Add(equipos);
                db.SaveChanges();
            }
            return View();
        }
    }
}