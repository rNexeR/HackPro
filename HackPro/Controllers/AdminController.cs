﻿using System;
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
            ViewBag.Title = "Pagina no encontrada";
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
            var model = new Usuarios();
            if (exist != null)
            {
                model.id = id;
                model.celular = exist.tbl_usuario_celular;
                model.correo = exist.tbl_usuario_correo;
                model.ocupacion = exist.tbl_usuario_ocupacion;
                model.username = exist.tbl_usuario_username;
                model.edad = DateTime.Now.Year - exist.tbl_usuario_fecha_nac.Year;
                model.nombre = exist.tbl_usuario_p_nombre + " " + exist.tbl_usuario_p_apellido;

                string jurado = "", administrador = "", events = "", groups = "", projects = "";

                var jurados = db.tbl_jurado.Where(p => p.tbl_jurado_id == id).ToList();
                var administra = db.tbl_evento.Where(p => p.tbl_usuario_id == id).ToList();
                var equipos = db.tbl_equipo_usuario.Where(p => p.tbl_usaurio_id == id).ToList();
                var proyectos = new List<tbl_proyecto>();
                var eventos = new List<tbl_evento>();
                foreach (var n in equipos)
                {
                    var equipo = db.tbl_equipo.Find(n.tbl_equipo_id);
                    groups += "<div class=\"col-md-3 col-sm-6 col-xs-12\"><div class=\"info-box\">";
                    groups += "<span class=\"info-box-icon bg-aqua\"><i class=\"fa fa-envelope-o\"></i></span>";
                    groups += "<div class=\"info-box-content\"><span class=\"info-box-text\">" + equipo.tbl_equipo_evento + "</span>";
                    groups += "<span class=\"info-box-number\">" + equipo.tbl_equipo_nombre + "</span></div></div></div>";
                    var tempProjects = db.tbl_proyecto.Where(p => p.tbl_equipo_id == equipo.tbl_equipo_id).ToList();
                    var tempEvents = db.tbl_equipo_evento.Where(p => p.tbl_equipo_id == equipo.tbl_equipo_id).ToList();
                    if (tempProjects.Any())
                        proyectos.AddRange(tempProjects);
                    if (tempEvents.Any())
                        eventos.AddRange(tempEvents.Select(m => db.tbl_evento.Find(m.tbl_evento_id)));

                }

                foreach (var n in jurados)
                {
                    var eventoActual = db.tbl_evento.Find(n.tbl_evento_id);
                    jurado += "<div class=\"col-md-3 col-sm-6 col-xs-12\"><div class=\"info-box\">";
                    jurado += "<span class=\"info-box-icon bg-aqua\"><i class=\"fa fa-envelope-o\"></i></span>";
                    jurado += "<div class=\"info-box-content\"><span class=\"info-box-text\">" + eventoActual.tbl_evento_lugar + "</span>";
                    jurado += "<span class=\"info-box-number\">" + eventoActual.tbl_evento_nombre + "</span></div></div></div>";
                }

                foreach (var n in administra)
                {
                    var evento = db.tbl_evento.Find(n.tbl_evento_id);
                    administrador += "<div class=\"col-md-3 col-sm-6 col-xs-12\"><div class=\"info-box\">";
                    administrador += "<span class=\"info-box-icon bg-aqua\"><i class=\"fa fa-envelope-o\"></i></span>";
                    administrador += "<div class=\"info-box-content\"><span class=\"info-box-text\">" + evento.tbl_evento_nombre + "</span>";
                    administrador += "<span class=\"info-box-number\">" + evento.tbl_evento_lugar + "</span></div></div></div>";
                }

                foreach (var n in eventos)
                {
                    events += "<div class=\"col-md-3 col-sm-6 col-xs-12\"><div class=\"info-box\">";
                    events += "<span class=\"info-box-icon bg-aqua\"><i class=\"fa fa-envelope-o\"></i></span>";
                    events += "<div class=\"info-box-content\"><span class=\"info-box-text\">" + n.tbl_evento_lugar + "</span>";
                    events += "<span class=\"info-box-number\">" + n.tbl_evento_nombre + "</span></div></div></div>";
                }

                foreach (var n in proyectos)
                {
                    projects += "<div class=\"col-md-3 col-sm-6 col-xs-12\"><div class=\"info-box\">";
                    projects += "<span class=\"info-box-icon bg-aqua\"><i class=\"fa fa-envelope-o\"></i></span>";
                    projects += "<div class=\"info-box-content\"><span class=\"info-box-text\">" + n.tbl_proyecto_git + "</span>";
                    projects += "<span class=\"info-box-number\">" + n.tbl_proyecto_nombre + "</span></div></div></div>";
                }

                ViewBag.Proyectos = projects;
                ViewBag.Administrador = administrador;
                ViewBag.Eventos = events;
                ViewBag.Equipos = groups;
                ViewBag.Jurado = jurado;

                return View(model);
            }
            return RedirectToAction("Error404");
        }

        public ActionResult Index()
        {
            /*if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");
            return View();
            */
            return RedirectToAction("Index", "Logged");
        }

        public ActionResult ListarEventos()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");
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

                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-pencil\" onclick=\"editar(" +
                                    x.tbl_evento_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw  fa-eye\" onclick=\"mostrar(" +
                                    x.tbl_evento_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-trash\" onclick=\"eliminar(" +
                                    x.tbl_evento_id + ")\"></i></button>";
                lista += "</td>";

                lista += "</tr>";
            }
            ViewBag.HtmlStr = lista;
            return View();
        }
        /**
            Función general para listar cualquier sección en un solo View
        **/
        public ActionResult Listar(int? section)
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Home");
            if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");

            switch (section)
            {
                case 0: //Usuarios 
                    return Usuarios();
                case 1: //Eventos
                    break;
                case 2: //Equipos
                    break;
                case 3: //Categoría de Eventos
                    break;
                case 4: //Proyectos
                    break;
                case 5: //TipoAporte;
                    break;
                default:
                    return RedirectToAction("Index", "Admin");
            }

            return View();
        }

        public ActionResult Usuarios()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");
            var lista = "";
            var db = new hackprodb_1Entities();
            var listado = db.tbl_usuario.ToList();
            foreach (var x in listado)
            {
                if (!x.tbl_usuario_activo)
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
                lista += x.tbl_usuario_activo ? "Si" : "No";
                lista += "</td>";
                lista += "<td>";
                lista += x.tbl_usuario_admin ? "Si" : "No";
                lista += "</td>";

                lista += "<td>";

                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-pencil\" onclick=\"editPerfil(" +
                                    x.tbl_usuario_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw  fa-eye\" onclick=\"showPerfil(" +
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
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");
            hackprodb_1Entities db = new hackprodb_1Entities();
            var exist = db.tbl_usuario.Where(a => a.tbl_usuario_id == id);
            if (exist.Any())
            {
                var user = db.tbl_usuario.Find(id);
                user.tbl_usuario_activo = false;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Usuarios", "Admin");

        }

        [HttpGet]
        public ActionResult MakeAdmin(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");
            hackprodb_1Entities db = new hackprodb_1Entities();
            var exist = db.tbl_usuario.Where(a => a.tbl_usuario_id == id);
            if (exist.Any())
            {
                var user = db.tbl_usuario.Find(id);
                user.tbl_usuario_admin = !user.tbl_usuario_admin;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Usuarios", "Admin");
        }

        [HttpGet]
        public ActionResult DeleteEvento(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");
            hackprodb_1Entities db = new hackprodb_1Entities();
            var exist = db.tbl_evento.Where(a => a.tbl_evento_id == id);
            if (exist.Any())
            {
                var mody = db.tbl_evento.Find(id);
                mody.tbl_evento_activo = false;
                db.Entry(mody).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("ListarEventos");

        }

        [HttpGet]
        public ActionResult DeleteEquipo(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");
            hackprodb_1Entities db = new hackprodb_1Entities();
            var exist = db.tbl_equipo.Where(a => a.tbl_equipo_id == id);
            if (exist.Any())
            {
                var mody = db.tbl_equipo.Find(id);
                mody.tbl_equipo_activo = false;
                db.Entry(mody).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("ListarEquipos");

        }

        [HttpGet]
        public ActionResult DeleteCatEvento(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");
            hackprodb_1Entities db = new hackprodb_1Entities();
            var exist = db.tbl_cat_evento.Where(a => a.tbl_cat_evento_id == id);
            if (exist.Any())
            {
                var mody = db.tbl_cat_evento.Find(id);
                mody.tbl_cat_evento_activo = false;
                db.Entry(mody).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("ListarCatEventos");

        }

        [HttpGet]
        public ActionResult CrearCatEvento()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");
            return View();
        }

        [HttpPost]
        public ActionResult CrearCatEvento(Cat_Evento categoria)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");
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
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");
            var lista = "";
            var db = new hackprodb_1Entities();
            var listado = db.tbl_equipo.ToList();
            foreach (var x in listado)
            {
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

                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-pencil\" onclick=\"editar(" +
                                    x.tbl_equipo_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw  fa-eye\" onclick=\"mostrar(" +
                                    x.tbl_equipo_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-trash\" onclick=\"eliminar(" +
                                    x.tbl_equipo_id + ")\"></i></button>";
                lista += "</td>";

                lista += "</tr>";

            }
            @ViewBag.HtmlStr = lista;
            return View();
        }

        public ActionResult ListarCatEventos()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");
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
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-pencil\" onclick=\"editar(" +
                                    x.tbl_cat_evento_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw  fa-eye\" onclick=\"mostrar(" +
                                    x.tbl_cat_evento_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-trash\" onclick=\"eliminar(" +
                                    x.tbl_cat_evento_id + ")\"></i></button>";
                lista += "</td>";

                lista += "</tr>";

            }
            @ViewBag.HtmlStr = lista;
            return View();
        }

        public ActionResult ListarProyectos()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");
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
                lista += db.tbl_evento.Find(x.tbl_evento_id).tbl_evento_desc;
                lista += "</td>";

                lista += "<td>";
                lista += x.tbl_proyecto_git;
                lista += "</td>";

                lista += "<td>";
                lista += x.tbl_proyecto_estatus;
                lista += "</td>";

                lista += "<td>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-pencil\" onclick=\"editar(" +
                                    x.tbl_proyecto_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw  fa-eye\" onclick=\"mostrar(" +
                                    x.tbl_proyecto_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-trash\" onclick=\"eliminar(" +
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
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");
            return View();
        }

        [HttpPost]
        public ActionResult CrearTipoAporte(TipoAporte tp)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");
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

        public ActionResult ListarTipoAporte()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");
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
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-pencil\" onclick=\"editar(" +
                                    x.tbl_tipo_aporte_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw  fa-eye\" onclick=\"mostrar(" +
                                    x.tbl_tipo_aporte_id + ")\"></i></button>";
                lista += "<button class=\"btn\"><i class=\"fa fa-fw fa-trash\" onclick=\"eliminar(" +
                                    x.tbl_tipo_aporte_id + ")\"></i></button>";
                lista += "</td>";

                lista += "</tr>";

            }
            @ViewBag.HtmlStr = lista;
            return View();
        }

        public ActionResult DeleteProyecto(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");
            hackprodb_1Entities db = new hackprodb_1Entities();
            var exist = db.tbl_proyecto.Where(a => a.tbl_proyecto_id == id);
            if (exist.Any())
            {
                var mody = db.tbl_proyecto.Find(id);
                mody.tbl_proyecto_activo = false;
                db.Entry(mody).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("ListarProyectos");
        }

        public ActionResult DeleteTipoAporte(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");
            hackprodb_1Entities db = new hackprodb_1Entities();
            var exist = db.tbl_tipo_aporte.Where(a => a.tbl_tipo_aporte_id == id);
            if (exist.Any())
            {
                var mody = db.tbl_tipo_aporte.Find(id);
                mody.tbl_tipo_aporte_activo = false;
                db.Entry(mody).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("ListarProyectos");
        }
              
        //Cuando se haya elegido desde el mapa (x, y)
        [HttpGet]
       // [ActionName("CreateEvento")]
        public ActionResult CrearEvento(string latitud, string longitud)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");

            return RedirectToAction("CrearEvento", "Logged");
        }

        [HttpPost]
        public ActionResult CrearEvento(Evento even)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
           
            return RedirectToAction("CrearEvento", "Logged");
        }

        [HttpGet]
        public ActionResult CrearEquipo()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");

            return RedirectToAction("CrearEquipo", "Logged");
        }

        [HttpPost]
        public ActionResult CrearEquipo(Equipo equipo)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");

            return RedirectToAction("CrearEquipo", "Logged");
        }

        [HttpGet]
        public ActionResult CrearProyecto()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");

            return RedirectToAction("CrearProyecto", "Logged");
        }

        [HttpPost]
        public ActionResult CrearProyecto(Proyectos pro)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");

            return RedirectToAction("CrearProyecto", "Logged");
        }

        [HttpGet]
        public ActionResult EditarUsuario(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            else if (Session["Admin"].Equals(false) && int.Parse(Session["UserId"].ToString()) != id)
                return RedirectToAction("PermissionError");

            var model = new Usuarios();
            var db = new hackprodb_1Entities();

            var user = db.tbl_usuario.Find(id);

            model.celular = user.tbl_usuario_celular;
            model.correo = user.tbl_usuario_correo;
            model.fecha_nac = user.tbl_usuario_fecha_nac;
            model.genero = user.tbl_usuario_genero;
            model.id = user.tbl_usuario_id;
            model.ocupacion = user.tbl_usuario_ocupacion;
            model.p_apellido = user.tbl_usuario_p_apellido;
            model.s_apellido = user.tbl_usuario_s_apellido;
            model.p_nombre = user.tbl_usuario_p_nombre;
            model.s_nombre = user.tbl_usuario_s_nombre;
            model.username = user.tbl_usuario_username;
            model.admin = user.tbl_usuario_admin;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditarUsuario(Usuarios user)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            else if (Session["Admin"].Equals(false) && int.Parse(Session["UserId"].ToString()) != user.id)
                return RedirectToAction("PermissionError");
            hackprodb_1Entities db = new hackprodb_1Entities();
            var userActual = db.tbl_usuario.Find(user.id);
            if (userActual != null)
            {
                userActual.tbl_usuario_activo = true;
                userActual.tbl_usuario_admin = user.admin;
                userActual.tbl_usuario_fecha_nac = user.fecha_nac;
                userActual.tbl_usuario_genero = user.genero;
                userActual.tbl_usuario_ocupacion = user.ocupacion;
                userActual.tbl_usuario_p_apellido = user.p_apellido;
                userActual.tbl_usuario_s_apellido = user.s_apellido;
                userActual.tbl_usuario_p_nombre = user.p_nombre;
                userActual.tbl_usuario_s_nombre = user.s_nombre;
                userActual.tbl_usuario_username = user.username;
                userActual.tbl_usuario_correo = user.correo;

                db.Entry(userActual).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Usuarios", "Admin");
        }

        [HttpGet]
        public ActionResult EditarEvento(int id)
        {

            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            var model = new Evento();
            var db = new hackprodb_1Entities();

            var ev = db.tbl_evento.Find(id);
            if (ev == null)
                return RedirectToAction("Error404");
            else if (Session["Admin"].Equals(false) && ev.tbl_usuario_id != int.Parse(Session["UserId"].ToString()))
                return RedirectToAction("PermissionError");

            model.cat_evento = db.tbl_cat_evento.ToList().Select(x => new SelectListItem
            {
                Value = x.tbl_cat_evento_id.ToString(),
                Text = x.tbl_cat_evento_desc
            });

            model.id = ev.tbl_evento_id;
            model.tbl_cat_evento = ev.tbl_cat_evento_id;
            model.tbl_evento_cal_jurado = ev.tbl_evento_cal_jurado;
            model.tbl_evento_cal_pueblo = ev.tbl_evento_cal_pueblo;
            model.tbl_evento_desc = ev.tbl_evento_desc;
            model.tbl_evento_duracion = ev.tbl_evento_duracion;
            model.tbl_evento_fecha_fin = ev.tbl_evento_fecha_fin;
            model.tbl_evento_fecha_inicio = ev.tbl_evento_fecha_inicio;
            model.tbl_evento_lugar = ev.tbl_evento_lugar;
            model.tbl_evento_lugar_x = ev.tbl_evento_lugar_x;
            model.tbl_evento_lugar_y = ev.tbl_evento_lugar_y;
            model.tbl_evento_nombre = ev.tbl_evento_nombre;
            model.tbl_evento_precio = ev.tbl_evento_precio;
            model.tbl_evento_presupuesto = ev.tbl_evento_presupuesto;
            model.tbl_evento_url = ev.tbl_evento_url;

            var equipos = db.tbl_equipo_evento.Where(p => p.tbl_evento_id == id).ToList();
            var jurados = db.tbl_jurado.Where(p => p.tbl_evento_id == id).ToList();
            string groups = "", jurado = "";
            foreach (var n in equipos)
            {
                if (n.tbl_equipo_evento_activo == false)
                    continue;
                var equipoActual = db.tbl_equipo.Find(n.tbl_equipo_id);
                groups += "<tr>";

                groups += "<td>";
                groups += equipoActual.tbl_equipo_id;
                groups += "</td>";

                groups += "<td>";
                groups += equipoActual.tbl_equipo_nombre;
                groups += "</td>";

                groups += "<td>";
                groups += equipoActual.tbl_equipo_usuario_admin;
                groups += "</td>";

                groups += "<td>";
                groups += "<button class=\"btn\"><i class=\"fa fa-fw  fa-eye\" onclick=\"mostrarEquipo(" +
                                    equipoActual.tbl_equipo_id + ")\"></i></button>";
                groups += "<button class=\"btn\"><i class=\"fa fa-fw fa-trash\" onclick=\"eliminarEquipo(" +
                                    equipoActual.tbl_equipo_id + ")\"></i></button>";
                groups += "</td>";

                groups += "</tr>";
            }

            foreach (var n in jurados)
            {
                if (n.tbl_jurado_activo == false)
                    continue;
                var user = db.tbl_usuario.Find(n.tbl_jurado_id);
                jurado += "<tr>";

                jurado += "<td>";
                jurado += user.tbl_usuario_id;
                jurado += "</td>";

                jurado += "<td>";
                jurado += user.tbl_usuario_username;
                jurado += "</td>";

                jurado += "<td>";
                jurado += user.tbl_usuario_p_nombre + " " + user.tbl_usuario_p_apellido;
                jurado += "</td>";

                jurado += "<td>";
                jurado += user.tbl_usuario_ocupacion;
                jurado += "</td>";

                jurado += "<td>";
                jurado += "<button class=\"btn\"><i class=\"fa fa-fw  fa-eye\" onclick=\"mostrarJurado(" +
                                    user.tbl_usuario_id + ")\"></i></button>";
                jurado += "<button class=\"btn\"><i class=\"fa fa-fw fa-trash\" onclick=\"eliminarJurado(" +
                                    user.tbl_usuario_id + "," + id + ")\"></i></button>";
                jurado += "</td>";

                jurado += "</tr>";
            }

            ViewBag.Equipos = groups;
            ViewBag.Jurados = jurado;

            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteJurado(int juradoid, int eventoid)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");
            var db = new hackprodb_1Entities();
            var fila = db.tbl_jurado.Where(p => p.tbl_evento_id == eventoid && p.tbl_jurado_id == juradoid);
            if (fila.Any())
            {
                fila.First().tbl_jurado_activo = false;
                db.Entry(fila.First()).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                return RedirectToAction("Error404");
            }
            return RedirectToAction("EditarEvento", new { id = eventoid });
        }

        [HttpGet]
        public ActionResult DeleteEquipoEvento(int equipoid, int eventoid)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");
            var db = new hackprodb_1Entities();
            var fila = db.tbl_equipo_evento.Where(p => p.tbl_evento_id == eventoid && p.tbl_equipo_id == equipoid);
            if (fila.Any())
            {
                fila.First().tbl_equipo_evento_activo = false;
                db.Entry(fila.First()).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                return RedirectToAction("Error404");
            }
            return RedirectToAction("EditarEvento", new { id = eventoid });
        }

        [HttpPost]
        public ActionResult EditarEvento(Evento evento)
        {

            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");

            var db = new hackprodb_1Entities();

            var ev = db.tbl_evento.Find(evento.id);
            if (ev == null)
                return RedirectToAction("Error404");
            else if (Session["Admin"].Equals(false) && ev.tbl_usuario_id != int.Parse(Session["UserId"].ToString()))
                return RedirectToAction("PermissionError");

            ev.tbl_cat_evento_id = evento.tbl_cat_evento;
            ev.tbl_evento_cal_jurado = evento.tbl_evento_cal_jurado;
            ev.tbl_evento_cal_pueblo = evento.tbl_evento_cal_pueblo;
            ev.tbl_evento_desc = evento.tbl_evento_desc;
            ev.tbl_evento_duracion = evento.tbl_evento_duracion;
            ev.tbl_evento_fecha_fin = evento.tbl_evento_fecha_fin;
            ev.tbl_evento_fecha_inicio = evento.tbl_evento_fecha_inicio;
            ev.tbl_evento_lugar = evento.tbl_evento_lugar;
            ev.tbl_evento_lugar_x = evento.tbl_evento_lugar_x;
            ev.tbl_evento_lugar_y = evento.tbl_evento_lugar_y;
            ev.tbl_evento_nombre = evento.tbl_evento_nombre;
            ev.tbl_evento_precio = evento.tbl_evento_precio;
            ev.tbl_evento_presupuesto = evento.tbl_evento_presupuesto;
            ev.tbl_evento_url = evento.tbl_evento_url;

            db.Entry(ev).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ListarEventos");
        }

        [HttpGet]
        public ActionResult EditarEquipo(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            var model = new Equipo();
            var db = new hackprodb_1Entities();

            var equipo = db.tbl_equipo.Find(id);
            if (equipo == null)
                return RedirectToAction("Error404");
            else if (Session["Admin"].Equals(false) && equipo.tbl_equipo_usuario_admin != int.Parse(Session["UserId"].ToString()))
                return RedirectToAction("PermissionError");

            model.tbl_equipo_nombre = equipo.tbl_equipo_nombre;
            model.id = equipo.tbl_equipo_id;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditarEquipo(Equipo model)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            var db = new hackprodb_1Entities();

            var equipo = db.tbl_equipo.Find(model.id);
            if (equipo == null)
                return RedirectToAction("Error404");
            else if (Session["Admin"].Equals(false) && equipo.tbl_equipo_usuario_admin != int.Parse(Session["UserId"].ToString()))
                return RedirectToAction("PermissionError");

            equipo.tbl_equipo_nombre = model.tbl_equipo_nombre;
            equipo.tbl_equipo_id = model.id;

            db.Entry(equipo).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ListarEquipos");
        }

        [HttpGet]
        public ActionResult EditarCatEvento(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            var model = new Cat_Evento();
            var db = new hackprodb_1Entities();

            var categoria = db.tbl_cat_evento.Find(id);
            if (categoria == null)
                return RedirectToAction("Error404");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");

            model.tbl_cat_evento_desc = categoria.tbl_cat_evento_desc;
            model.id = categoria.tbl_cat_evento_id;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditarCatEvento(Cat_Evento model)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            var db = new hackprodb_1Entities();

            var categoria = db.tbl_cat_evento.Find(model.id);
            if (categoria == null)
                return RedirectToAction("Error404");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");

            categoria.tbl_cat_evento_desc = model.tbl_cat_evento_desc;
            categoria.tbl_cat_evento_id = model.id;

            db.Entry(categoria).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ListarCatEventos");
        }

        [HttpGet]
        public ActionResult EditarProyecto(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            var model = new Proyectos();
            var db = new hackprodb_1Entities();

            var project = db.tbl_proyecto.Find(id);
            if (project == null)
                return RedirectToAction("Error404");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");

            model.id = project.tbl_proyecto_id;
            model.tbl_equipo_id = project.tbl_equipo_id;
            model.tbl_evento_id = project.tbl_evento_id;
            model.tbl_proyecto_git = project.tbl_proyecto_git;
            model.tbl_proyecto_url = project.tbl_proyecto_url;
            model.estado = project.tbl_proyecto_estatus;
            model.tbl_proyecto_nombre = project.tbl_proyecto_nombre;

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
        public ActionResult EditarProyecto(Proyectos model)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            var db = new hackprodb_1Entities();

            var project = db.tbl_proyecto.Find(model.id);
            if (project == null)
                return RedirectToAction("Error404");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");

            project.tbl_equipo_id = model.tbl_equipo_id;
            project.tbl_evento_id = model.tbl_evento_id;
            project.tbl_proyecto_estatus = model.estado;
            project.tbl_proyecto_git = model.tbl_proyecto_git;
            project.tbl_proyecto_nombre = model.tbl_proyecto_nombre;
            project.tbl_proyecto_url = model.tbl_proyecto_url;
            project.tbl_proyecto_activo = true;

            db.Entry(project).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ListarProyectos");
        }

        [HttpGet]
        public ActionResult EditarTipoAporte(int id)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            var model = new TipoAporte();
            var db = new hackprodb_1Entities();

            var aporte = db.tbl_tipo_aporte.Find(id);
            if (aporte == null)
                return RedirectToAction("Error404");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");

            model.tbl_tipo_aporte_desc = aporte.tbl_tipo_aporte_desc;
            model.id = aporte.tbl_tipo_aporte_id;

            return View(model);
        }

        [HttpPost]
        public ActionResult EditarTipoAporte(TipoAporte model)
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login", "Home");
            var db = new hackprodb_1Entities();

            var aporte = db.tbl_tipo_aporte.Find(model.id);
            if (aporte == null)
                return RedirectToAction("Error404");
            else if (Session["Admin"].Equals(false))
                return RedirectToAction("PermissionError");

            aporte.tbl_tipo_aporte_desc = model.tbl_tipo_aporte_desc;
            aporte.tbl_tipo_aporte_id = model.id;

            db.Entry(aporte).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("ListarTipoAporte");
        }
    }
}