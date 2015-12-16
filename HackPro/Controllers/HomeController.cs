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
                case 2:
                    return "Feb.";
                case 3:
                    return "Marzo";
                case 4:
                    return "Abril";
                case 5:
                    return "Mayo";
                case 6:
                    return "Junio";
                case 7:
                    return "Julio";
                case 8:
                    return "Ago.";
                case 9:
                    return "Seot.";
                case 10:
                    return "Oct.";
                case 11:
                    return "Nov.";
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

            var db = new hackprodb_1Entities();
            var eventos = db.tbl_evento.ToList();

            var lista = "";
            int cant_eventos = eventos.Count();
            for (int i = 0; i < cant_eventos; i++)
            {

                if (!eventos[i].tbl_evento_activo)
                    continue;

                lista += "{\"Id\":" + (i + 1) + ",";
                lista += "\"PlaceName\": \"" + eventos[i].tbl_evento_lugar + "\",";
                lista += "\"Fecha\": \"" + eventos[i].tbl_evento_fecha_inicio + "\", ";
                lista += "\"NombreEvento\": \"" + eventos[i].tbl_evento_nombre + "\", ";
                lista += "\"GeoLat\": \"" + eventos[i].tbl_evento_lugar_x + "\", ";
                lista += "\"GeoLong\": \"" + eventos[i].tbl_evento_lugar_y + "\"}";

                if (i != cant_eventos - 1)
                    lista += ",";
            }
            @ViewBag.HtmlStr = lista;

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
                var login = db.tbl_usuario.Where(p => p.tbl_usuario_correo.Equals(log.email) && p.tbl_usuario_password.Equals(log.password));
                if (login.Count() == 1)
                {
                    Session["UserId"] = login.First().tbl_usuario_id;
                    Session["Name"] = login.First().tbl_usuario_p_nombre + " " + login.First().tbl_usuario_p_apellido;
                    Session["Correo"] = log.email;
                    Session["Username"] = login.First().tbl_usuario_username;
                    Session["Ocupacion"] = login.First().tbl_usuario_ocupacion;
                    Session["Since"] = getMonthInWords(login.First().tbl_usuario_fecha_crea.Month) + " " + login.First().tbl_usuario_fecha_crea.Year;
                    Session["Admin"] = login.First().tbl_usuario_admin;
                    if (login.First().tbl_usuario_admin)
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
            var model = new EventoView();
            var db = new hackprodb_1Entities();

            var ev = db.tbl_evento.Find(id);
            if (ev == null)
                return RedirectToAction("Error404");

            model.id = ev.tbl_evento_id;
            model.tbl_cat_evento = db.tbl_cat_evento.Find(ev.tbl_cat_evento_id).tbl_cat_evento_desc;
            model.tbl_evento_cal_jurado = ev.tbl_evento_cal_jurado;
            model.tbl_evento_cal_pueblo = ev.tbl_evento_cal_pueblo;
            model.tbl_evento_desc = ev.tbl_evento_desc;
            model.tbl_evento_duracion = ev.tbl_evento_duracion;
            model.tbl_evento_fecha_fin = ev.tbl_evento_fecha_fin.ToLongDateString().ToUpper();
            model.tbl_evento_fecha_inicio = ev.tbl_evento_fecha_inicio.ToLongDateString().ToUpper();
            model.tbl_evento_lugar = ev.tbl_evento_lugar;
            model.tbl_evento_lugar_x = ev.tbl_evento_lugar_x;
            model.tbl_evento_lugar_y = ev.tbl_evento_lugar_y;
            model.tbl_evento_nombre = ev.tbl_evento_nombre;
            model.tbl_evento_precio = ev.tbl_evento_precio;
            model.tbl_evento_presupuesto = ev.tbl_evento_presupuesto;
            model.tbl_evento_url = ev.tbl_evento_url;


            string html = "";
            //"< ul style = \"margin: 0; padding: 0\" >< li >< div class=\"row\" alig=\"center\">";

            if (!model.tbl_evento_cal_jurado && !model.tbl_evento_cal_pueblo)
            {
                html += "<div class=\"col-lg-3 col-xs-6\">";
                html += "<div class=\"small-box bg-red\"><div class=\"inner\"><h3>SIN</h3><p>Calificacion</p></div>";
                html += "<div class=\"icon\"><i class=\"ion ion-close\"></i></div>";
                //html += "<a href = \"#\" class=\"small-box-footer\">detalles<i class=\"fa fa-arrow-circle-right\"></i></a>";
                html += "</div></div>";
            }
            else
            {
                if (model.tbl_evento_cal_jurado)
                {
                    html += "<div class=\"col-lg-3 col-xs-6\">";
                    html += "<div class=\"small-box bg-green\"><div class=\"inner\"><h3>CAL.</h3><p>JURADO</p></div>";
                    html += "<div class=\"icon\"><i class=\"ion ion-checkmark\"></i></div>";
                    //html += "<a href = \"#\" class=\"small-box-footer\">detalles<i class=\"fa fa-arrow-circle-right\"></i></a>";
                    html += "</div></div>";
                }
                if (model.tbl_evento_cal_pueblo)
                {
                    html += "<div class=\"col-lg-3 col-xs-6\">";
                    html += "<div class=\"small-box bg-green\"><div class=\"inner\"><h3>CAL.</h3><p>PUEBLO</p></div>";
                    html += "<div class=\"icon\"><i class=\"ion ion-checkmark\"></i></div>";
                    //html += "<a href = \"#\" class=\"small-box-footer\">detalles<i class=\"fa fa-arrow-circle-right\"></i></a>";
                    html += "</div></div>";
                }
            }
            //html += "</div></li></ul>";

            ViewBag.HtmlStr = html;

            var equipos = db.tbl_equipo_evento.Where(p => p.tbl_evento_id == id).ToList();
            model.equipos_registrados = equipos.Count();
            model.proyectos_registrados = db.tbl_proyecto.Count(p => p.tbl_evento_id == id);
            model.jurados_registrados = db.tbl_jurado.Count(p => p.tbl_evento_id == id);
            model.usuarios_registrados = 0;
            foreach (var n in equipos)
            {
                model.usuarios_registrados += db.tbl_equipo_usuario.Count(p => p.tbl_equipo_id == n.tbl_equipo_id);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Error404()
        {
            ViewBag.Title = "Pagina no encontrada";
            return View();
        }

        [HttpGet]
        public ActionResult UsuariosEvento(int id)
        {
            var db = new hackprodb_1Entities();
            var evento = db.tbl_evento.Find(id);
            if (evento == null)
                RedirectToAction("Error404");
            var equipos = db.tbl_equipo_evento.Where(p => p.tbl_evento_id == id).ToList();
            var html = "";

            foreach (var n in equipos)
            {

                var usuarios = db.tbl_equipo_usuario.Where(p => p.tbl_equipo_id == n.tbl_equipo_id).ToList();
                foreach (var m in usuarios)
                {
                    html += "<div class=\"col-md-3 col-sm-6 col-xs-12\"><div class=\"info-box\">";
                    html += "<span class=\"info-box-icon bg-aqua\"><i class=\"fa fa-envelope-o\"></i></span>";
                    html += "<div class=\"info-box-content\"><span class=\"info-box-text\">" + db.tbl_equipo.Find(n.tbl_equipo_id).tbl_equipo_nombre + "</span>";
                    html += "<span class=\"info-box-number\">" + db.tbl_usuario.Find(m.tbl_usaurio_id).tbl_usuario_username + "</span></div></div></div>";
                }
            }
            ViewBag.HtmlStr = html;
            return View();
        }

        [HttpGet]
        public ActionResult EquiposEvento(int id)
        {
            var db = new hackprodb_1Entities();
            var evento = db.tbl_evento.Find(id);
            if (evento == null)
                RedirectToAction("Error404");
            var equipos = db.tbl_equipo_evento.Where(p => p.tbl_evento_id == id).ToList();
            var html = "";

            foreach (var n in equipos)
            {
                int admin = db.tbl_equipo.Find(n.tbl_equipo_id).tbl_equipo_usuario_admin;
                html += "<div class=\"col-md-3 col-sm-6 col-xs-12\"><div class=\"info-box\">";
                html += "<span class=\"info-box-icon bg-aqua\"><i class=\"fa fa-envelope-o\"></i></span>";
                html += "<div class=\"info-box-content\"><span class=\"info-box-text\">" + db.tbl_usuario.Find(admin).tbl_usuario_username + "</span>";
                html += "<span class=\"info-box-number\">" + db.tbl_equipo.Find(n.tbl_equipo_id).tbl_equipo_nombre + "</span></div></div></div>";
            }
            ViewBag.HtmlStr = html;
            return View();
        }

        [HttpGet]
        public ActionResult ProyectosEvento(int id)
        {
            var db = new hackprodb_1Entities();
            var evento = db.tbl_evento.Find(id);
            if (evento == null)
                RedirectToAction("Error404");
            var proyectos = db.tbl_proyecto.Where(p => p.tbl_evento_id == id).ToList();
            var html = "";

            foreach (var n in proyectos)
            {
                html += "<div class=\"col-md-3 col-sm-6 col-xs-12\"><div class=\"info-box\">";
                html += "<span class=\"info-box-icon bg-aqua\"><i class=\"fa fa-envelope-o\"></i></span>";
                html += "<div class=\"info-box-content\"><span class=\"info-box-text\">" + n.tbl_proyecto_git + "</span>";
                html += "<span class=\"info-box-number\">" + n.tbl_proyecto_nombre + "</span></div></div></div>";
            }
            ViewBag.HtmlStr = html;
            return View();
        }

        [HttpGet]
        public ActionResult JuradosEvento(int id)
        {
            var db = new hackprodb_1Entities();
            var evento = db.tbl_evento.Find(id);
            if (evento == null)
                RedirectToAction("Error404");
            var jurados = db.tbl_jurado.Where(p => p.tbl_evento_id == id).ToList();
            var html = "";

            foreach (var n in jurados)
            {
                int user = n.tbl_jurado_id;
                html += "<div class=\"col-md-3 col-sm-6 col-xs-12\"><div class=\"info-box\">";
                html += "<span class=\"info-box-icon bg-aqua\"><i class=\"fa fa-envelope-o\"></i></span>";
                html += "<div class=\"info-box-content\"><span class=\"info-box-text\">" + db.tbl_usuario.Find(user).tbl_usuario_username + "</span>";
                html += "<span class=\"info-box-number\">" + db.tbl_usuario.Find(user).tbl_usuario_ocupacion + "</span></div></div></div>";
            }
            ViewBag.HtmlStr = html;
            return View();
        }

        [HttpGet]
        public ActionResult Equipo(int id)
        {
            var model = new Equipo();
            var db = new hackprodb_1Entities();
            var equipo = db.tbl_equipo.Find(id);
            if (equipo == null)
                return RedirectToAction("Error404");
            model.id = id;
            model.tbl_equipo_nombre = equipo.tbl_equipo_nombre;

            var usuarios = db.tbl_equipo_usuario.Where(p => p.tbl_equipo_id == id).ToList();
            var proyectos = db.tbl_proyecto.Where(p => p.tbl_equipo_id == id).ToList();
            var eventos = db.tbl_equipo_evento.Where(p => p.tbl_equipo_id == id).ToList();

            string users = "", projects = "", events = "";

            foreach (var m in usuarios)
            {
                users += "<div class=\"col-md-3 col-sm-6 col-xs-12\"><div class=\"info-box\">";
                users += "<span class=\"info-box-icon bg-aqua\"><i class=\"fa fa-envelope-o\"></i></span>";
                users += "<div class=\"info-box-content\"><span class=\"info-box-text\">" + db.tbl_usuario.Find(m.tbl_usaurio_id).tbl_usuario_ocupacion + "</span>";
                users += "<span class=\"info-box-number\">" + db.tbl_usuario.Find(m.tbl_usaurio_id).tbl_usuario_username + "</span></div></div></div>";
            }

            foreach (var n in proyectos)
            {
                projects += "<div class=\"col-md-3 col-sm-6 col-xs-12\"><div class=\"info-box\">";
                projects += "<span class=\"info-box-icon bg-aqua\"><i class=\"fa fa-envelope-o\"></i></span>";
                projects += "<div class=\"info-box-content\"><span class=\"info-box-text\">" + n.tbl_proyecto_git + "</span>";
                projects += "<span class=\"info-box-number\">" + n.tbl_proyecto_nombre + "</span></div></div></div>";
            }

            foreach (var n in eventos)
            {
                var evento = db.tbl_evento.Find(n.tbl_evento_id);
                events += "<div class=\"col-md-3 col-sm-6 col-xs-12\"><div class=\"info-box\">";
                events += "<span class=\"info-box-icon bg-aqua\"><i class=\"fa fa-envelope-o\"></i></span>";
                events += "<div class=\"info-box-content\"><span class=\"info-box-text\">" + evento.tbl_evento_nombre + "</span>";
                events += "<span class=\"info-box-number\">" + evento.tbl_evento_lugar + "</span></div></div></div>";
            }

            ViewBag.eventos = events;
            ViewBag.proyectos = projects;
            ViewBag.usuarios = users;

            return View(model);
        }
    }
}