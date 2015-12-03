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
            ViewBag.Title = "Usuario no encontrado";
            return View();
        }

        [HttpGet]
        public ActionResult PermissionError()
        {
            ViewBag.Title = "Privilegios Limitados";
            return View();
        }

        [HttpGet]
        public ActionResult Profile(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
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
            if (Session["UserId"] != null || Session["Admin"].Equals(true))
                return View();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult ListarEventos()
        {
            if (Session["UserId"] == null || int.Parse(Session["UserId"].ToString()) != 22)
                return RedirectToAction("Login", "Home");
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
            if (Session["UserId"] == null || int.Parse(Session["UserId"].ToString()) != 22)
                return RedirectToAction("Login", "Home");
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
                lista += "<button class=\"btn\"><i class=\"fa fa-black-tie\" onclick=\"makeAdmin(" +
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
            if (Session["UserId"] == null || int.Parse(Session["UserId"].ToString()) != 22)
                return RedirectToAction("Login", "Home");
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
            if (Session["UserId"] == null || int.Parse(Session["UserId"].ToString()) != 22)
                return RedirectToAction("Login", "Home");
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
            if (Session["UserId"] == null || int.Parse(Session["UserId"].ToString()) != 22)
                return RedirectToAction("Login", "Home");
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
            if (Session["UserId"] == null || int.Parse(Session["UserId"].ToString()) != 22)
                return RedirectToAction("Login", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult CrearTipoAporte(TipoAporte tp)
        {
            if (Session["UserId"] == null || int.Parse(Session["UserId"].ToString()) != 22)
                return RedirectToAction("Login", "Home");
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
            if (Session["UserId"] == null || int.Parse(Session["UserId"].ToString()) != 22)
                return RedirectToAction("Login", "Home");
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

        [HttpGet]
        public ActionResult CrearEvento()
        {
            var model = new Evento();
            var db = new hackprodb_1Entities();
            model.cat_evento = db.tbl_cat_evento.ToList().Select(x => new SelectListItem
            {
                Value = x.tbl_cat_evento_id.ToString(),
                Text = x.tbl_cat_evento_desc
            });
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
                int id = int.Parse(Session["ÜserId"].ToString());
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

                db.tbl_proyecto.Add(project);
                db.SaveChanges();
            }
            return View();
        }
    }
}