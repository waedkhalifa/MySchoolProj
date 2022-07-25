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
    [Authorize]
    public class ReservationsController : Controller
    {
        private MySchool11Entities db = new MySchool11Entities();
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.AspNetUser).Include(r => r.Cours);
            return View("myCourses",reservations.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservations reservations = db.Reservations.Find(id);
            if (reservations == null)
            {
                return HttpNotFound();
            }
            return View(reservations);
        }

        // GET: Reservations/Create
        [CustomAuthorize(Roles = "Admin,Teacher")]
        public ActionResult Create()
        {
            var usersWithRoles = (from user in context.Users
                                  from userRole in user.Roles
                                  join role in context.Roles on userRole.RoleId equals
                                  role.Id
                                  where role.Name == "Student"
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
            ViewBag.CoursesId = new SelectList(db.Courses, "Id", "Name");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin,Teacher")]

        public ActionResult Create([Bind(Include = "Id,AspNetUserId,CoursesId")] Reservations reservations)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", reservations.AspNetUserId);
            ViewBag.CoursesId = new SelectList(db.Courses, "Id", "Name", reservations.CoursesId);
            return View(reservations);
        }

        // GET: Reservations/Edit/5
        [CustomAuthorize(Roles = "Admin,Teacher")]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservations reservations = db.Reservations.Find(id);
            if (reservations == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", reservations.AspNetUserId);
            ViewBag.CoursesId = new SelectList(db.Courses, "Id", "Name", reservations.CoursesId);
            return View(reservations);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin,Teacher")]

        public ActionResult Edit([Bind(Include = "Id,AspNetUserId,CoursesId")] Reservations reservations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", reservations.AspNetUserId);
            ViewBag.CoursesId = new SelectList(db.Courses, "Id", "Name", reservations.CoursesId);
            return View(reservations);
        }

        // GET: Reservations/Delete/5
        [CustomAuthorize(Roles = "Admin,Teacher")]

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservations reservations = db.Reservations.Find(id);
            if (reservations == null)
            {
                return HttpNotFound();
            }
            return View(reservations);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin,Teacher")]

        public ActionResult DeleteConfirmed(int id)
        {
            Reservations reservations = db.Reservations.Find(id);
            db.Reservations.Remove(reservations);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


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
