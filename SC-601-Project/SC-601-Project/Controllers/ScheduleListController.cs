using SC_601_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using PagedList.Mvc;
using PagedList;

namespace SC_601_Project.Controllers
{
    public class ScheduleListController : Controller
    {
        // GET: ScheduleList
        public ActionResult Index(string search, int? i)
        {

            //////////////////////////
            List<ScheduleListViewModel> lst = null;
            using (masterEntities db2 = new masterEntities())
            {
                lst =
                    (from d in db2.Schedules
                     select new ScheduleListViewModel
                     {
                         time = d.time
                     }).ToList();
            }

            List<SelectListItem> times = lst.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.time.ToString(),
                    Value = d.time.ToString(),
                    Selected = false
                };
            });
            ViewBag.times = times;
            //////////////////////////

            masterEntities DB = new masterEntities();
            IList<ScheduleList> list = DB.ScheduleLists.ToList();
            ViewBag.TotalSch = DB.ScheduleLists.Where(x => x.time == search || search == null).Count();
            return View(DB.ScheduleLists.Where(x => x.time == search || search == null).ToList().ToPagedList(i ?? 1, 5));
            //return View(list);
        }



        public ActionResult Delete(int id)
        {
            masterEntities DB = new masterEntities();
            IList<ScheduleList> schList = DB.ScheduleLists.ToList();
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
                    var oTabla = db.ScheduleLists.Find(id);
                    if (oTabla != null)
                    {
                        db.ScheduleLists.Remove(oTabla);
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
            IList<ScheduleList> schList = DB.ScheduleLists.ToList();
            var schs = schList.Where(b => b.id == id).FirstOrDefault();

            return View(schs);
        }


        public ActionResult Create()
        {
            /////////////// Start to fill dropdown list /////////////// 
            List<ScheduleListViewModel> lst = null;
            using (masterEntities db2 = new masterEntities())
            {
                lst =
                    (from d in db2.Schedules
                     select new ScheduleListViewModel
                     {
                         time = d.time
                     }).ToList();
            }

            List<SelectListItem> times = lst.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.time.ToString(),
                    Value = d.time.ToString(),
                    Selected = false
                };
            });
            ViewBag.times = times;

            //////////
            List<ScheduleListViewModel> lst2 = null;
            using (masterEntities db2 = new masterEntities())
            {
                lst2 =
                    //(from d in db2.UserCreds
                    // select new ScheduleListViewModel
                    // {
                    //     username = d.username
                    // })
                    ( from d in db2.UserCreds where d.role == "U"
                     select new ScheduleListViewModel
                     {
                         username = d.username
                     })
                     .ToList();
            }

            //var innerJoinQuery =
            //from category in categories
            //join prod in products on category.ID equals prod.CategoryID
            //select new { ProductName = prod.Name, Category = category.Name }

            List<SelectListItem> users = lst2.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.username.ToString(),
                    Value = d.username.ToString(),
                    Selected = false
                };
            });
            ViewBag.users = users;
            /////////////// End of filling dropdown list /////////////// 
            return View();
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreateConfirmed(ScheduleList schLst)
        {
            //Start to fill dropdown list
            masterEntities db2 = new masterEntities();
            var schLstExist = db2.ScheduleLists.Where(x => (x.username == schLst.username) && (x.time == schLst.time)).ToList();

            if (schLstExist.Count() == 0)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        using (masterEntities db = new masterEntities())
                        {
                            var oTabla = new ScheduleList();
                            oTabla.username = schLst.username;
                            oTabla.time= schLst.time;
                            db.ScheduleLists.Add(oTabla);
                            db.SaveChanges();
                        }
                        return Content("<script language='javascript' type='text/javascript'>alert('Se ha agendado correctamente');" +
                        "window.location.href = 'Index/'; </script>");
                    }

                    return View(schLst);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                ViewBag.Message = "Ya se encuentra registrado en el horario";
                //return RedirectToAction("Index");
                //return View(creds);
                return Content("<script language='javascript' type='text/javascript'>alert('Ya se encuentra registrado en el horario');" +
                    "window.location.href = 'Create/'; </script>");
            }

        }
    }
}