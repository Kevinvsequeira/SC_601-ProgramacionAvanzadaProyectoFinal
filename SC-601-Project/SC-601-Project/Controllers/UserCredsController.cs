using SC_601_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace SC_601_Project.Controllers
{
    public class UserCredsController : Controller
    {
        public ActionResult Index(string search, int? i)
        {
            masterEntities DB = new masterEntities();
            IList<UserCred> credsList = DB.UserCreds.ToList();
            ViewBag.TotalCreds = DB.UserCreds.Where(x => x.username.StartsWith(search) || search == null).Count() ;
            return View(DB.UserCreds.Where(x => x.username.StartsWith(search) || search==null).ToList().ToPagedList(i ?? 1, 5));
            //return View(credsList);
        }

        public ActionResult Edit(int id)
        {
            UserCredViewModel model = new UserCredViewModel();
            using (masterEntities db = new masterEntities())
            {
                var oTabla = db.UserCreds.Find(id);
                model.idNum = (int)oTabla.idNum;
                model.username = oTabla.username;
                model.password = oTabla.password;
                model.status = oTabla.status;
                model.role = oTabla.role;
            }
            masterEntities DB = new masterEntities();
            IList<UserCred> credsList = DB.UserCreds.ToList();

            var creds = credsList.Where(a => a.idNum == id).FirstOrDefault();

            //Start to fill dropdown list
            List<SelectListItem> Status = new List<SelectListItem>();
            Status.Add(new SelectListItem() { Text = "Habilitado", Value = "Enabled" });
            Status.Add(new SelectListItem() { Text = "Deshabilitado", Value = "Disabled" });
            ViewBag.Status = Status;

            List<SelectListItem> Roles = new List<SelectListItem>();
            Roles.Add(new SelectListItem() { Text = "Usuario", Value = "U" });
            Roles.Add(new SelectListItem() { Text = "Empleado", Value = "E" });
            Roles.Add(new SelectListItem() { Text = "Admin", Value = "A" });
            ViewBag.Roles = Roles;
            //End to fill dropdown list


            return View(creds);
        }

        [HandleError]
        [HttpPost]
        public ActionResult Edit(UserCred creds)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (masterEntities db = new masterEntities())
                    {
                        var oTabla = db.UserCreds.Find(creds.idNum);
                        oTabla.idNum = creds.idNum;
                        oTabla.username = creds.username;
                        oTabla.password = creds.password;
                        oTabla.status = creds.status;
                        oTabla.role = creds.role;
                        db.Entry(oTabla).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Content("<script language='javascript' type='text/javascript'>alert('Cambios realizados correctamente');" +
                    "window.location.href = '../Index/'; </script>");
                }
                return View(creds);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //masterEntities DB = new masterEntities();
            //IList<Person> personaList = DB.People.ToList();

            //var editPersona = personaList.Where(a => a.idNum == persona.idNum).FirstOrDefault();
            //DB.People.Remove(editPersona);
            //DB.People.Add(persona);
            //DB.SaveChanges();
            //return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            masterEntities DB = new masterEntities();
            IList<UserCred> credsList = DB.UserCreds.ToList();
            var credsDel = credsList.Where(b => b.idNum == id).FirstOrDefault();

            return View(credsDel);
        }

        [HandleError]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                //return Content("<script language='javascript' type='text/javascript'>alert('AAA: " + id + "');</script>");
                using (masterEntities db = new masterEntities())
                {
                    var oTabla = db.UserCreds.Find(id);
                    if (oTabla != null)
                    {
                        db.UserCreds.Remove(oTabla);
                        db.SaveChanges();
                        return Content("<script language='javascript' type='text/javascript'>alert('Registro eliminado correctamente');" +
                        "window.location.href = '../Index/'; </script>");
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Details(int id)
        {
            masterEntities DB = new masterEntities();
            IList<UserCred> credsList = DB.UserCreds.ToList();
            var creds = credsList.Where(b => b.idNum == id).FirstOrDefault();

            return View(creds);
        }

        public ActionResult Create()
        {
            //Start to fill dropdown list
            List<PersonViewModel> lst = null;
            using (masterEntities db2 = new masterEntities())
            {
                lst =
                    (from d in db2.People
                     select new PersonViewModel
                     {
                         idNum = d.idNum,
                         first_name = d.first_name
                     }).ToList();
            }

            List<SelectListItem> ids = lst.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.idNum.ToString(),
                    Value = d.idNum.ToString(),
                    Selected = false
                };
            });
            ViewBag.ids = ids;
            List<SelectListItem> Status = new List<SelectListItem>();
            Status.Add(new SelectListItem() { Text = "Habilitado", Value = "Enabled" });
            Status.Add(new SelectListItem() { Text = "Deshabilitado", Value = "Disabled" });
            ViewBag.Status = Status;

            List<SelectListItem> Roles = new List<SelectListItem>();
            Roles.Add(new SelectListItem() { Text = "Usuario", Value = "U" });
            Roles.Add(new SelectListItem() { Text = "Empleado", Value = "E" });
            Roles.Add(new SelectListItem() { Text = "Admin", Value = "A" });
            ViewBag.Roles = Roles;
            //End to fill dropdown list
            return View();
        }

        [HandleError]
        [HttpPost, ActionName("Create")]
        public ActionResult CreateConfirmed(UserCred creds)
        {
            //Start to fill dropdown list
            masterEntities db2 = new masterEntities();
            var credExist = db2.UserCreds.Find(creds.idNum);

            if (credExist == null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        using (masterEntities db = new masterEntities())
                        {
                            var oTabla = new UserCred();
                            oTabla.idNum = creds.idNum;
                            oTabla.username = creds.username;
                            oTabla.password = creds.password;
                            oTabla.status = creds.status;
                            oTabla.role = creds.role;
                            db.UserCreds.Add(oTabla);
                            db.SaveChanges();
                        }
                        return Content("<script language='javascript' type='text/javascript'>alert('El usuario fue creado correctamente');" +
                        "window.location.href = 'Index/'; </script>");
                    }

                    return View(creds);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                ViewBag.Message = "Ya existen credenciales vinculadas a ese ID";
                //return RedirectToAction("Index");
                //return View(creds);
                return Content("<script language='javascript' type='text/javascript'>alert('Ya existen credenciales vinculadas a ese ID');" +
                    "window.location.href = 'Create/'; </script>");
            }

        }
    }
}