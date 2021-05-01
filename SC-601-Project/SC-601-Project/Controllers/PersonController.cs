using System;
using SC_601_Project.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using PagedList.Mvc;
using PagedList;

namespace SC_601_Project.Controllers
{
    public class PersonController : Controller
    {
        // GET: Employee
        public ActionResult Index(string search, int? i)
        {
            masterEntities DB = new masterEntities();
            IList<Person> personaList = DB.People.ToList();
            ViewBag.TotalPersonas = DB.People.Where(x => x.first_name.StartsWith(search) || search == null).Count();
            return View(DB.People.Where(x => x.first_name.StartsWith(search) || search == null).ToList().ToPagedList(i ?? 1, 5));
            //return View(personaList);
        }

        public ActionResult Edit(int id)
        {
            PersonViewModel model = new PersonViewModel();
            using (masterEntities db = new masterEntities())
            {
                var oTabla = db.People.Find(id);
                model.first_name = oTabla.first_name;
                model.last_name= oTabla.last_name;
                model.registerDate = oTabla.registerDate;
                model.birthDate = oTabla.birthDate;
                model.phone = oTabla.phone;
                model.idNum = oTabla.idNum;
            }
            masterEntities DB = new masterEntities();
            IList<Person> personaList = DB.People.ToList();

            var persona = personaList.Where(a => a.idNum == id).FirstOrDefault();

            return View(persona);
        }

        [HandleError]
        [HttpPost]
        public ActionResult Edit(Person persona)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (masterEntities db = new masterEntities())
                    {
                        var oTabla = db.People.Find(persona.idNum);
                        oTabla.first_name = persona.first_name;
                        oTabla.last_name = persona.last_name;
                        oTabla.registerDate = persona.registerDate;
                        oTabla.birthDate= persona.birthDate;
                        oTabla.phone = persona.phone;
                        db.Entry(oTabla).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Content("<script language='javascript' type='text/javascript'>alert('Cambios realizados correctamente');" +
                    "window.location.href = '../Index/'; </script>");
                }
                return View(persona);
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
            IList<Person> personaList = DB.People.ToList();
            var personDel = personaList.Where(b => b.idNum == id).FirstOrDefault();

            return View(personDel);
        }

        [HandleError]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            //return Content("<script language='javascript' type='text/javascript'>alert('AAA: " + id + "');</script>");
            using (masterEntities db = new masterEntities())
            {
                var oTabla = db.UserCreds.Find(id);
                if (oTabla != null)
                {
                    db.UserCreds.Remove(oTabla);
                    db.SaveChanges();
                }
                var oTabla2 = db.People.Find(id);
                db.People.Remove(oTabla2);
                db.SaveChanges();
            }
            return Content("<script language='javascript' type='text/javascript'>alert('Registro eliminado correctamente');" +
                        "window.location.href = '../Index/'; </script>");
        }

        public ActionResult Details(int id)
        {
            masterEntities DB = new masterEntities();
            IList<Person> personaList = DB.People.ToList();
            var person = personaList.Where(b => b.idNum == id).FirstOrDefault();

            return View(person);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HandleError]
        [HttpPost, ActionName("Create")]
        public ActionResult CreateConfirmed(Person persona)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (masterEntities db = new masterEntities())
                    {
                        var oTabla = new Person();
                        oTabla.idNum = persona.idNum;
                        oTabla.first_name = persona.first_name;
                        oTabla.last_name = persona.last_name;
                        oTabla.registerDate = persona.registerDate;
                        oTabla.birthDate = persona.birthDate;
                        oTabla.phone = persona.phone;
                        db.People.Add(oTabla);
                        db.SaveChanges();
                    }
                    return Content("<script language='javascript' type='text/javascript'>alert('El usuario fue creado correctamente');" +
                    "window.location.href = 'Index/'; </script>");
                }

                return View(persona);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}