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
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login log)
        {
            if (ModelState.IsValid)
            {
                hackprodb_1Entities db = new hackprodb_1Entities();
                var login = db.tbl_usuario.Where(p => p.tbl_usuario_correo.Equals(log.email)  && p.tbl_usuario_password.Equals(log.password));
                if (login.Count() == 1)
                {
                    Session["username"] = "Nexer";
                    return View("Index");
                }
                else
                {
                    ModelState.AddModelError("Password", "Email or Password not valid");
                }
            }
                     
            return View();            
        }
    }
}