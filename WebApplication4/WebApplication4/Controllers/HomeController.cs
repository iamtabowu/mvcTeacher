using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DBCon dbcon = new DBCon();
            List<Teacher> teachers = dbcon.GetTeacher();
            ViewBag.teacher = teachers;
            return View();
        }
        public ActionResult CreateTeacher()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTeacher(Teacher teachers)
        {
            DBCon dbcon = new DBCon();
            try
            {
                dbcon.NewTeacher(teachers);
            }
            catch(Exception e) {
                Console.WriteLine(e.ToString());
            }
            return RedirectToAction("Index");
        }
        public ActionResult EditTeacher(string id)
        {
            DBCon dbcon = new DBCon();
            Teacher teacher = dbcon.GetTeacherByID(id);
            return View(teacher);
        }
        [HttpPost]
        public ActionResult EditTeacher(Teacher teacher)
        {
            DBCon dbcon = new DBCon();
            dbcon.UpdateTeacherByID(teacher);
            return RedirectToAction("Index");
        }
    }
}