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

    }
}