using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HackPro.Models;

namespace HackPro.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListarEventos()
        {
            var lista = "";
            var db = new hackprodb_1Entities();
            var listado = db.tbl_evento.ToList();
            
            foreach (var x in listado)
            {
                if (!x.tbl_evento_activo)
                    continue;
                lista += "<tr>";

                lista += "<td>";
                lista += x.tbl_evento_id;
                lista += "</td>";

                lista += "<td>";
                lista += x.tbl_evento_nombre;
                lista += "</td>";

                lista += "<td>";
                lista += db.tbl_cat_evento.Find(x.tbl_cat_evento_id).tbl_cat_evento_desc;
                lista += "</td>";

                lista += "<td>";
                lista += x.tbl_evento_lugar;
                lista += "</td>";

                lista += "<td>";
                lista += db.tbl_usuario.Find(x.tbl_usuario_id).tbl_usuario_username;
                lista += "</td>";

                lista += "<td>";
                lista += x.tbl_evento_duracion;
                lista += "</td>";

                lista += "<td>";
                lista += x.tbl_evento_precio;
                lista += "</td>";

                lista += "<td>";

                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-pencil\" onclick=\"editPerfil(" +
                                    x.tbl_usuario_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-user\" onclick=\"showPerfil(" +
                                    x.tbl_usuario_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-trash\" onclick=\"deleteuser(" +
                                    x.tbl_usuario_id + ")\"></i></button>";
                lista += "</td>";

                lista += "</tr>";
            }
            ViewBag.HtmlStr = lista;
            return View();
        }

        public ActionResult Usuarios()
        {
            var lista = "";
            var db = new hackprodb_1Entities();
            var listado = db.tbl_usuario.ToList();
            foreach (var x in listado)
            {
                if(!x.tbl_usuario_activo)
                    continue;
                lista += "<tr>";
                lista += "<td>";
                lista += x.tbl_usuario_id;
                lista += "</td>";
                lista += "<td>";
                lista += x.tbl_usuario_username;
                lista += "</td>";
                lista += "<td>";
                lista += x.tbl_usuario_correo;
                lista += "</td>";
                lista += "<td>";
                lista += x.tbl_usuario_p_nombre;
                lista += "</td>";
                lista += "<td>";
                lista += x.tbl_usuario_p_apellido;
                lista += "</td>";
                lista += "<td>";
                lista += x.tbl_usuario_ocupacion;
                lista += "</td>";
                lista += "<td>";
                lista += x.tbl_usuario_activo? "Si" + ";" : "No";
                lista += "</td>";
                lista += "<td>";
                lista += x.tbl_usuario_admin? "Si" + ";" : "No";
                lista += "</td>";

                lista += "<td>";

                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-pencil\" onclick=\"editPerfil(" +
                                    x.tbl_usuario_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-user\" onclick=\"showPerfil(" +
                                    x.tbl_usuario_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-trash\" onclick=\"deleteuser(" +
                                    x.tbl_usuario_id + ")\"></i></button>";
                lista += "</td>";

                lista += "</tr>";
            }
            ViewBag.HtmlStr = lista;

            return View();
        }

        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            hackprodb_1Entities db = new hackprodb_1Entities();
            var exist = db.tbl_usuario.Where(a => a.tbl_usuario_id == id);
            if (exist.Any())
            {
                var user = db.tbl_usuario.Find(id);
                user.tbl_usuario_activo = false;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View("Usuarios");

        }

        [HttpGet]
        public ActionResult DeleteEvento(int id)
        {
            hackprodb_1Entities db = new hackprodb_1Entities();
            var exist = db.tbl_evento.Where(a => a.tbl_evento_id == id);
            if (exist.Any())
            {
                var mody = db.tbl_evento.Find(id);
                mody.tbl_evento_activo = false;
                db.Entry(mody).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View("ListarEventos");

        }

        [HttpGet]
        public ActionResult DeleteEquipo(int id)
        {
            hackprodb_1Entities db = new hackprodb_1Entities();
            var exist = db.tbl_equipo.Where(a => a.tbl_equipo_id == id);
            if (exist.Any())
            {
                var mody = db.tbl_equipo.Find(id);
                mody.tbl_equipo_activo = false;
                db.Entry(mody).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View("ListarEquipos");

        }

        [HttpGet]
        public ActionResult DeleteCatEvento(int id)
        {
            hackprodb_1Entities db = new hackprodb_1Entities();
            var exist = db.tbl_cat_evento.Where(a => a.tbl_cat_evento_id == id);
            if (exist.Any())
            {
                var mody = db.tbl_cat_evento.Find(id);
                mody.tbl_cat_evento_activo = false;
                db.Entry(mody).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View("ListarCatEventos");

        }

        [HttpGet]
        public ActionResult CrearCatEvento()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearCatEvento(Cat_Evento categoria)
        {
            if (ModelState.IsValid)
            {
                var db = new hackprodb_1Entities();
                var cat = new tbl_cat_evento();

                cat.tbl_cat_evento_desc = categoria.tbl_cat_evento_desc;
                cat.tbl_cat_evento_activo = true;

                db.tbl_cat_evento.Add(cat);
                db.SaveChanges();
            }
            return View();
        }

        [HttpGet]
        public ActionResult ListarEquipos()
        {
            var lista = "";
            var db = new hackprodb_1Entities();
            var listado = db.tbl_equipo.ToList();
            foreach(var x in listado)
            {
                ///*
                if (!x.tbl_equipo_activo)
                    continue;

                lista += "<tr>";

                lista += "<td>";
                lista += x.tbl_equipo_id;
                lista += "</td>";

                lista += "<td>";
                lista += x.tbl_equipo_nombre;
                lista += "</td>";

                lista += "<td>";
                lista += db.tbl_usuario.Find(x.tbl_equipo_usuario_admin).tbl_usuario_username;
                lista += "</td>";

                lista += "<td>";

                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-pencil\" onclick=\"editPerfil(" +
                                    x.tbl_equipo_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-user\" onclick=\"showPerfil(" +
                                    x.tbl_equipo_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-trash\" onclick=\"deleteuser(" +
                                    x.tbl_equipo_id + ")\"></i></button>";
                lista += "</td>";

                lista += "</tr>";

            }
            @ViewBag.HtmlStr = lista;
            return View();
        }

        public ActionResult ListarCatEventos()
        {
            var lista = "";
            var db = new hackprodb_1Entities();
            var listado = db.tbl_cat_evento.ToList();
            foreach (var x in listado)
            {
                ///*
                if (!x.tbl_cat_evento_activo)
                    continue;

                lista += "<tr>";

                lista += "<td>";
                lista += x.tbl_cat_evento_id;
                lista += "</td>";

                lista += "<td>";
                lista += x.tbl_cat_evento_desc;
                lista += "</td>";

                lista += "<td>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-pencil\" onclick=\"editPerfil(" +
                                    x.tbl_cat_evento_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-user\" onclick=\"showPerfil(" +
                                    x.tbl_cat_evento_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-trash\" onclick=\"deleteuser(" +
                                    x.tbl_cat_evento_id + ")\"></i></button>";
                lista += "</td>";

                lista += "</tr>";

            }
            @ViewBag.HtmlStr = lista;
            return View();
        }
    }
}