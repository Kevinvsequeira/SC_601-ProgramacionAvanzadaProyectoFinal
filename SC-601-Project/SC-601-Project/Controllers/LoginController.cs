using SC_601_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SC_601_Project.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(SC_601_Project.Models.UserCred userModel)
        {
            using (masterEntities db = new masterEntities())
            {
                var userDetails = db.UserCreds.Where(x => x.username == userModel.username &&
                x.password == userModel.password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Credenciales incorrectas";
                    return View("Index", userModel);
                }
                else
                {
                    Session["userID"] = userDetails.idNum;
                    Session["username"] = userDetails.username;
                    Session["role"] = userDetails.role;
                    return Content("<script language='javascript' type='text/javascript'>alert('Bienvenido');" +
                    "window.location.href = '../Home/Index/'; </script>");
                    //return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
}