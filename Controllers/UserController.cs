using MySchoolProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySchoolProj.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        // GET: User
        [CustomAuthorize(Roles = "Admin,Teacher")]
        public ActionResult Index()
        {
            var usersWithRoles = (from user in context.Users
                                  from userRole in user.Roles
                                  join role in context.Roles on userRole.RoleId equals
                                  role.Id
                                  where role.Name == "Teacher"
                                  select new UserViewModel()
                                  {
                                      Id=user.Id,
                                      Email = user.Email,
                                      firstName=user.firstName,
                                      lastName=user.lastName,
                                      age=user.age,
                                      Role = role.Name
                                  }).ToList();

            return View(usersWithRoles);
        }
        [CustomAuthorize(Roles = "Admin,Teacher")]
        public ActionResult Student()
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

            return View(usersWithRoles);
        }
    }
}