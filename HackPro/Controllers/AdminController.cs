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
        [HttpGet]
        public ActionResult Error404()
        {
            ViewBag.Title = "User not found";
            return View();
        }

        [HttpGet]
        public ActionResult Profile(int id)
        {
            var db = new hackprodb_1Entities();
            var exist = db.tbl_usuario.Find(id);
            if (exist != null)
            {

                return View();
            }
            return View("Error404");
        }

        public ActionResult Index()
        {
            if (Session["UserId"] != null)
                return View();
            return RedirectToAction("Login", "Home");
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
            if (Session["UserId"] == null || int.Parse(Session["UserId"].ToString()) != 22)
                return RedirectToAction("Login", "Home");
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
            if (Session["UserId"] == null || int.Parse(Session["UserId"].ToString()) != 22)
                return RedirectToAction("Login", "Home");
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
            if (Session["UserId"] == null || int.Parse(Session["UserId"].ToString()) != 22)
                return RedirectToAction("Login", "Home");
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
            if (Session["UserId"] == null || int.Parse(Session["UserId"].ToString()) != 22)
                return RedirectToAction("Login", "Home");
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
            if (Session["UserId"]==null || int.Parse(Session["UserId"].ToString()) != 22)
                return RedirectToAction("Login", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult CrearCatEvento(Cat_Evento categoria)
        {
            if (Session["UserId"] == null || int.Parse(Session["UserId"].ToString()) != 22)
                return RedirectToAction("Login", "Home");
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

        public ActionResult ListarProyectos()
        {
            var lista = "";
            var db = new hackprodb_1Entities();
            var listado = db.tbl_proyecto.ToList();
            foreach (var x in listado)
            {
                ///*
                if (!x.tbl_proyecto_activo)
                    continue;

                lista += "<tr>";

                lista += "<td>";
                lista += x.tbl_proyecto_id;
                lista += "</td>";

                lista += "<td>";
                lista += x.tbl_proyecto_nombre;
                lista += "</td>";

                lista += "<td>";
                lista += db.tbl_equipo.Find(x.tbl_equipo_id).tbl_equipo_nombre;
                lista += "</td>";

                lista += "<td>";
                lista += db.tbl_evento.Find(x.tbl_equipo_id).tbl_evento_desc;
                lista += "</td>";

                lista += "<td>";
                lista += x.tbl_proyecto_git;
                lista += "</td>";

                lista += "<td>";
                lista += x.tbl_proyecto_estatus;
                lista += "</td>";

                lista += "<td>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-pencil\" onclick=\"editPerfil(" +
                                    x.tbl_proyecto_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-user\" onclick=\"showPerfil(" +
                                    x.tbl_proyecto_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-trash\" onclick=\"deleteuser(" +
                                    x.tbl_proyecto_id + ")\"></i></button>";
                lista += "</td>";

                lista += "</tr>";

            }
            @ViewBag.HtmlStr = lista;
            return View();
        }

        [HttpGet]
        public ActionResult CrearTipoAporte()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CrearTipoAporte(TipoAporte tp)
        {
            if (ModelState.IsValid)
            {
                var db = new hackprodb_1Entities();
                var tipo = new tbl_tipo_aporte();

                tipo.tbl_tipo_aporte_desc = tp.tbl_tipo_aporte_desc;
                tipo.tbl_tipo_aporte_activo = true;

                db.tbl_tipo_aporte.Add(tipo);
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult ListarTIpoAporte()
        {
            var lista = "";
            var db = new hackprodb_1Entities();
            var listado = db.tbl_tipo_aporte.ToList();
            foreach (var x in listado)
            {
                ///*
                if (!x.tbl_tipo_aporte_activo)
                    continue;

                lista += "<tr>";

                lista += "<td>";
                lista += x.tbl_tipo_aporte_id;
                lista += "</td>";

                lista += "<td>";
                lista += x.tbl_tipo_aporte_desc;
                lista += "</td>";

                lista += "<td>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-pencil\" onclick=\"editPerfil(" +
                                    x.tbl_tipo_aporte_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-user\" onclick=\"showPerfil(" +
                                    x.tbl_tipo_aporte_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-trash\" onclick=\"deleteuser(" +
                                    x.tbl_tipo_aporte_id + ")\"></i></button>";
                lista += "</td>";

                lista += "</tr>";

            }
            @ViewBag.HtmlStr = lista;
            return View();
        }

        public ActionResult DeleteProyecto(int id)
        {
            if (Session["UserId"] == null || int.Parse(Session["UserId"].ToString()) != 22)
                return RedirectToAction("Login", "Home");
            hackprodb_1Entities db = new hackprodb_1Entities();
            var exist = db.tbl_proyecto.Where(a => a.tbl_proyecto_id == id);
            if (exist.Any())
            {
                var mody = db.tbl_proyecto.Find(id);
                mody.tbl_proyecto_activo = false;
                db.Entry(mody).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View("ListarProyectos");
        }

        public ActionResult DeleteTipoAporte(int id)
        {
            if (Session["UserId"] == null || int.Parse(Session["UserId"].ToString()) != 22)
                return RedirectToAction("Login", "Home");
            hackprodb_1Entities db = new hackprodb_1Entities();
            var exist = db.tbl_tipo_aporte.Where(a => a.tbl_tipo_aporte_id == id);
            if (exist.Any())
            {
                var mody = db.tbl_tipo_aporte.Find(id);
                mody.tbl_tipo_aporte_activo = false;
                db.Entry(mody).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View("ListarProyectos");
        }
    }
}