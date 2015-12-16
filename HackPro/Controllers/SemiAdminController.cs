using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HackPro.Models;

namespace HackPro.Controllers
{
    public class SemiAdminController : Controller
    {
        // GET: SemiAdmin
        public ActionResult Index()
        {
            return View();
        }

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
    }
}