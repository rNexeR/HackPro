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
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            return View();
        }

        public ActionResult ProximosEventos()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            return View();
        }

        public ActionResult Eventos()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            return View();
        }

        public ActionResult Proyectos()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
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
        
        public ActionResult Patrocinios()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            return View();
        }

    }
}