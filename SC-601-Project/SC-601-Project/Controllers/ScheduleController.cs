using SC_601_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace SC_601_Project.Controllers
{
    public class ScheduleController : Controller
    {
        // GET: Schedule
        public ActionResult Index(string search, int? i)
        {
            masterEntities DB = new masterEntities();
    
            return View(DB.Schedules.ToList().ToPagedList(i ?? 1, 5));
            //return View(credsList);
        }

        public ActionResult Edit(int id)
        {
            ScheduleViewModel model = new ScheduleViewModel();
            using (masterEntities db = new masterEntities())
            {
                var oTabla = db.Schedules.Find(id);
                model.time = oTabla.time;
                model.duration = oTabla.duration;
                model.capacity = oTabla.capacity;
            }
            masterEntities DB = new masterEntities();
            IList<Schedule> schList = DB.Schedules.ToList();

            var sch = schList.Where(a => a.id == id).FirstOrDefault();

            //Start to fill dropdown list
            //Start to fill dropdown list
            List<SelectListItem> durations = new List<SelectListItem>();
            durations.Add(new SelectListItem() { Text = "30", Value = "30" });
            durations.Add(new SelectListItem() { Text = "60", Value = "60" });
            durations.Add(new SelectListItem() { Text = "75", Value = "75" });
            durations.Add(new SelectListItem() { Text = "90", Value = "90" });
            durations.Add(new SelectListItem() { Text = "105", Value = "105" });
            durations.Add(new SelectListItem() { Text = "120", Value = "120" });
            ViewBag.durations = durations;

            List<SelectListItem> capacities = new List<SelectListItem>();
            capacities.Add(new SelectListItem() { Text = "1", Value = "1" });
            capacities.Add(new SelectListItem() { Text = "3", Value = "3" });
            capacities.Add(new SelectListItem() { Text = "5", Value = "5" });
            capacities.Add(new SelectListItem() { Text = "10", Value = "10" });
            capacities.Add(new SelectListItem() { Text = "15", Value = "15" });
            ViewBag.capacities = capacities;
            //End to fill dropdown list

            return View(sch);
        }

        [HandleError]
        [HttpPost]
        public ActionResult Edit(Schedule sch)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (masterEntities db = new masterEntities())
                    {
                        var oTabla = db.Schedules.Find(sch.id);
                        oTabla.time = sch.time;
                        oTabla.duration = sch.duration;
                        oTabla.capacity = sch.capacity;
                        db.Entry(oTabla).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Content("<script language='javascript' type='text/javascript'>alert('Cambios realizados correctamente');" +
                    "window.location.href = '../Index/'; </script>");
                }
                return View(sch);
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
            IList<Schedule> schList = DB.Schedules.ToList();
            var schDel = schList.Where(b => b.id == id).FirstOrDefault();

            return View(schDel);
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
                    var oTabla = db.Schedules.Find(id);
                    if (oTabla != null)
                    {
                        db.Schedules.Remove(oTabla);
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
            IList<Schedule> schList = DB.Schedules.ToList();
            var schedule = schList.Where(b => b.id == id).FirstOrDefault();

            return View(schedule);
        }

        public ActionResult Create()
        {
            //Start to fill dropdown list
            List<SelectListItem> durations = new List<SelectListItem>();
            durations.Add(new SelectListItem() { Text = "30", Value = "30" });
            durations.Add(new SelectListItem() { Text = "60", Value = "60" });
            durations.Add(new SelectListItem() { Text = "75", Value = "75" });
            durations.Add(new SelectListItem() { Text = "90", Value = "90" });
            durations.Add(new SelectListItem() { Text = "105", Value = "105" });
            durations.Add(new SelectListItem() { Text = "120", Value = "120" });
            ViewBag.durations = durations;

            List<SelectListItem> capacities = new List<SelectListItem>();
            capacities.Add(new SelectListItem() { Text = "1", Value = "1" });
            capacities.Add(new SelectListItem() { Text = "3", Value = "3" });
            capacities.Add(new SelectListItem() { Text = "5", Value = "5" });
            capacities.Add(new SelectListItem() { Text = "10", Value = "10" });
            capacities.Add(new SelectListItem() { Text = "15", Value = "15" });
            ViewBag.capacities = capacities;
            //End to fill dropdown list
            return View();
        }

        [HandleError]
        [HttpPost, ActionName("Create")]
        public ActionResult CreateConfirmed(Schedule sch)
        {
            //Start to fill dropdown list
            masterEntities db2 = new masterEntities();
            var schExist = db2.Schedules.Find(sch.time);

            if (schExist == null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        using (masterEntities db = new masterEntities())
                        {
                            var oTabla = new Schedule();
                            oTabla.time = sch.time;
                            oTabla.duration = sch.duration;
                            oTabla.capacity = sch.capacity;
                            db.Schedules.Add(oTabla);
                            db.SaveChanges();
                        }
                        return Content("<script language='javascript' type='text/javascript'>alert('El horario fue creado correctamente');" +
                        "window.location.href = 'Index/'; </script>");
                    }

                    return View(sch);
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
                return Content("<script language='javascript' type='text/javascript'>alert('Ya existe el horario ingresado');" +
                    "window.location.href = 'Create/'; </script>");
            }
        }
    }
}