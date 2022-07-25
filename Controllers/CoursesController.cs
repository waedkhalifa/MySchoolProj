using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MySchoolProj.Models;

namespace MySchoolProj.Controllers
{
    public class CoursesController : Controller
    {
        private MySchool11Entities db = new MySchool11Entities();
        ApplicationDbContext context = new ApplicationDbContext();

        // GET: Courses
        [CustomAuthorize(Roles = "Admin,Teacher")]
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.AspNetUser);
            return View(courses.ToList());
        }

        // GET: Courses/Details/5
        [CustomAuthorize(Roles = "Admin,Teacher")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = db.Courses.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }
            return View(courses);
        }

        // GET: Courses/Create
        [CustomAuthorize(Roles = "Admin,Teacher")]
        public ActionResult Create()
        {
            var usersWithRoles = (from user in context.Users
                                  from userRole in user.Roles
                                  join role in context.Roles on userRole.RoleId equals
                                  role.Id
                                  where role.Name == "Teacher"
                                  select new UserViewModel()
                                  {
                                      Id = user.Id,
                                      Email = user.Email,
                                      firstName = user.firstName,
                                      lastName = user.lastName,
                                      age = user.age,
                                      Role = role.Name
                                  }).ToList();
            ViewBag.AspNetUserId = new SelectList(usersWithRoles, "Id", "Email");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin,Teacher")]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Length,AspNetUserId")] Courses courses)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(courses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", courses.AspNetUserId);
            return View(courses);
        }

        // GET: Courses/Edit/5
        [CustomAuthorize(Roles = "Admin,Teacher")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = db.Courses.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", courses.AspNetUserId);
            return View(courses);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin,Teacher")]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Length,AspNetUserId")] Courses courses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", courses.AspNetUserId);
            return View(courses);
        }

        // GET: Courses/Delete/5
        [CustomAuthorize(Roles = "Admin,Teacher")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = db.Courses.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }
            return View(courses);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin,Teacher")]
        public ActionResult DeleteConfirmed(int id)
        {
            Courses courses = db.Courses.Find(id);
            db.Courses.Remove(courses);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [CustomAuthorize(Roles = "Admin,Teacher")]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
