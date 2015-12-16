using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HackPro.Models;

namespace HackPro.Controllers
{
    public class LoggedController : Controller
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

        // GET: Logged
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");

            var db = new hackprodb_1Entities();
            var eventos = db.tbl_evento.ToList();

            var lista = "";
            int cant_eventos = eventos.Count();
            for (int i = 0; i < cant_eventos; i++)
            {
                //foreach(var x in eventos)
                //{
                //Format
                //{ "Id": 1, "PlaceName": "Wonderland", "Fecha": "9-5, M-F", "GeoLat": "15.473692", "GeoLong": "-88.004896" },
                lista += "{\"Id\":" + /*eventos[i].tbl_evento_id*/ (i + 1) + ",";
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

        public ActionResult ProximosEventos()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            return View();
        }

        [HttpGet]
        public ActionResult Eventos()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");

            var lista = "";
            var db = new hackprodb_1Entities();
            var listado = db.tbl_equipo_evento.ToList();
            foreach (var x in listado)
            {
                var evento = db.tbl_evento.Find(x.tbl_evento_id);

                int user_id = Convert.ToInt16(Session["UserId"]);
                var exist_user = db.tbl_equipo_usuario.Where(a => a.tbl_usaurio_id == user_id &&
                    a.tbl_equipo_id == x.tbl_equipo_id);

                if (!evento.tbl_evento_activo ||
                    !x.tbl_equipo_evento_activo || !exist_user.Any())
                    continue;

                lista += "<div class=\"col-lg-3 col-xs-6\">";
                lista += "<div class=\"small-box bg-green\">";
                lista += "<div class=\"inner\">";
                lista += "<h3>";
                lista += evento.tbl_evento_nombre + "</h3>";
                lista += "<p>" + evento.tbl_evento_lugar;
                lista += getMonthInWords(evento.tbl_evento_fecha_inicio.Month);
                lista += " " + evento.tbl_evento_fecha_inicio.Day;

                lista += "</p> </div> <a href=\"#\" class=\"small-box-footer\">" +
                    "details <i class=\"fa fa-arrow-circle-right\"></i> </a> </div></div>";
            }
            @ViewBag.HtmlStr = lista;

            return View();
        }

        [HttpGet]
        public ActionResult Proyectos()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");

            var lista = "";
            var db = new hackprodb_1Entities();
            var listado = db.tbl_proyecto.ToList();
            foreach (var x in listado)
            {
                var equipo = db.tbl_equipo.Find(x.tbl_equipo_id);

                int user_id = Convert.ToInt16(Session["UserId"]);
                var exist_user = db.tbl_equipo_usuario.Where(a => a.tbl_usaurio_id == user_id &&
                    a.tbl_equipo_id == x.tbl_equipo_id);

                if (!equipo.tbl_equipo_activo || !x.tbl_proyecto_activo || !exist_user.Any())
                    continue;

                lista += "<div class=\"col-lg-3 col-xs-6\">";
                lista += "<div class=\"small-box bg-red\">";
                lista += "<div class=\"inner\">";
                lista += "<h3>";
                lista += x.tbl_proyecto_nombre + "</h3>";
                lista += "<p>" + equipo.tbl_equipo_nombre;
                lista += "</p> </div> <a href=\"#\" class=\"small-box-footer\">" +
                    "details <i class=\"fa fa-arrow-circle-right\"></i> </a> </div></div>";
            }
            @ViewBag.HtmlStr = lista;
            return View();
        }

        [HttpGet]
        public ActionResult Equipos()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            var lista = "";
            var db = new hackprodb_1Entities();
            var listado = db.tbl_equipo_usuario.ToList();
            foreach (var x in listado)
            {
                var team = db.tbl_equipo.Find(x.tbl_equipo_id);
                if (!team.tbl_equipo_activo ||
                    !x.tbl_usaurio_id.ToString().Equals(Session["UserId"].ToString()))
                    continue;

                lista += "<div class=\"col-lg-3 col-xs-6\">";
                lista += "<div class=\"small-box bg-aqua\">";
                lista += "<div class=\"inner\">";
                lista += "<h3>";
                lista += team.tbl_equipo_nombre + "</h3>";
                lista += "<p>Since ";
                lista += getMonthInWords(team.tbl_equipo_fecha_creacion.Month);
                lista += " " + team.tbl_equipo_fecha_creacion.Year;

                lista += "</p> </div> <a href=\"#\" class=\"small-box-footer\">" +
                    "details <i class=\"fa fa-arrow-circle-right\"></i> </a> </div></div>";
            }
            @ViewBag.HtmlStr = lista;
            return View();
        }

        [HttpGet]
        public ActionResult Patrocinios()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            return View();
        }

        //Creates

        //EQUIPO
        [HttpGet]
        public ActionResult CrearEquipo()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");

            return View();
        }

        [HttpPost]
        public ActionResult CrearEquipo(Equipo equipo)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");

            if (ModelState.IsValid)
            {
                var db = new hackprodb_1Entities();
                var equipos = new tbl_equipo();

                equipos.tbl_equipo_nombre = equipo.tbl_equipo_nombre;
                equipos.tbl_equipo_activo = true;
                equipos.tbl_equipo_fecha_creacion = DateTime.Today;
                equipos.tbl_equipo_usuario_admin = int.Parse(Session["UserId"].ToString());

                db.tbl_equipo.Add(equipos);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Logged");
        }

        //PROYECTO
        [HttpGet]
        public ActionResult CrearProyecto()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");

            var model = new Proyectos();
            var db = new hackprodb_1Entities();
            model.eventos = db.tbl_evento.ToList().Select(x => new SelectListItem
            {
                Value = x.tbl_evento_id.ToString(),
                Text = x.tbl_evento_nombre
            });

            model.equipos = db.tbl_equipo.ToList().Select(x => new SelectListItem
            {
                Value = x.tbl_equipo_id.ToString(),
                Text = x.tbl_equipo_nombre
            });

            return View(model);
        }

        [HttpPost]
        public ActionResult CrearProyecto(Proyectos pro)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");

            if (ModelState.IsValid)
            {
                var db = new hackprodb_1Entities();
                var project = new tbl_proyecto();

                project.tbl_equipo_id = pro.tbl_equipo_id;
                project.tbl_evento_id = pro.tbl_evento_id;
                project.tbl_proyecto_activo = true;
                project.tbl_proyecto_nombre = pro.tbl_proyecto_nombre;
                project.tbl_proyecto_estatus = 0;
                project.tbl_proyecto_git = pro.tbl_proyecto_git;
                project.tbl_proyecto_url = pro.tbl_proyecto_url;


                db.tbl_proyecto.Add(project);
                db.SaveChanges();
            }
            return RedirectToAction("CrearProyecto");
        }

        //EVENTO
        [HttpGet]
        public ActionResult CrearEvento(string latitud, string longitud)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");

            var model = new Evento();
            var db = new hackprodb_1Entities();
            model.cat_evento = db.tbl_cat_evento.ToList().Select(x => new SelectListItem
            {
                Value = x.tbl_cat_evento_id.ToString(),
                Text = x.tbl_cat_evento_desc
            });
            if (String.IsNullOrEmpty(latitud) || String.IsNullOrEmpty(longitud))
            {
                latitud = "0";
                longitud = "0";
            }

            model.tbl_evento_lugar_x = Convert.ToDecimal(latitud);
            model.tbl_evento_lugar_y = Convert.ToDecimal(longitud);
            return View(model);
        }

        [HttpPost]
        public ActionResult CrearEvento(Evento even)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");

            if (ModelState.IsValid)
            {
                var db = new hackprodb_1Entities();
                var evento = new tbl_evento();

                evento.tbl_evento_duracion = even.tbl_evento_duracion;
                evento.tbl_evento_precio = even.tbl_evento_precio;
                evento.tbl_usuario_id = int.Parse(Session["UserId"].ToString());
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
            return RedirectToAction("CrearEvento");
        }

    }
}