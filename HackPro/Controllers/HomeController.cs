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
                    if (user.password != user.r_password)
                    {
                        ModelState.AddModelError("r_password", "Existe un usuario con el mismo correo");
                    }
                    else
                    {
                        tbl_usuario usuario = new tbl_usuario();
                        usuario.tbl_usuario_correo = user.correo;
                        usuario.tbl_usuario_username = user.username;
                        usuario.tbl_usuario_p_nombre = user.p_nombre;
                        usuario.tbl_usuario_s_nombre = user.s_nombre;
                        usuario.tbl_usuario_p_apellido = user.p_apellido;
                        usuario.tbl_usuario_s_apellido = user.s_apellido;
                        usuario.tbl_usuario_ocupacion = user.ocupacion;
                        usuario.tbl_usuario_password = user.password;
                        usuario.tbl_usuario_fecha_nac = user.fecha_nac;
                        usuario.tbl_usuario_celular = "88888888";//falta que no sea hard-coded
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
                    Session["username"] = "Nexer";
                    return View("Index");
                }
                else
                {
                    ModelState.AddModelError("Password", "Email or Password not valid");
                }
            }
                     
            return View();            
        }
    }
}