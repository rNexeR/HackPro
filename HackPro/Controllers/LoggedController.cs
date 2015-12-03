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

        // GET: Logged
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
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
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            if (ModelState.IsValid)
            {
                var db = new hackprodb_1Entities();
                var evento = new tbl_evento();

                evento.tbl_evento_duracion = even.tbl_evento_duracion;
                evento.tbl_evento_precio = even.tbl_evento_precio;
                evento.tbl_usuario_id = int.Parse(Session["ÜserId"].ToString());
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
            if(Session["UserId"] == null)
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
                equipos.tbl_equipo_usuario_admin = int.Parse(Session["ÜserId"].ToString());

                db.tbl_equipo.Add(equipos);
                db.SaveChanges();
            }
            return View();
        }

        [HttpGet]
        public ActionResult CrearProyecto()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            return View();
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

                db.tbl_proyecto.Add(project);
                db.SaveChanges();
            }
            return View();
        }
    }
}