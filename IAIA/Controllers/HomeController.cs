using IAIA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAIA.Controllers
{
    public class HomeController : Controller
    {


        private testEntities db = new testEntities();
        public ActionResult Index()
        { 
            
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        // GET: Users/login
        public ActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string UserEmail, string UserPassword, string UserRole)
        {
            User user = db.users.SingleOrDefault(u => u.UserEmail == UserEmail && u.UserPassword == UserPassword && u.UserRole == UserRole);
            Session["userId"] = user.UserId;
            UserRole = user.UserRole;
            Session["userFirstName"] = user.UserFirstName;
            Session["userRole"] = user.UserRole;
            Session["photo"] = user.UserPhoto;
            if (UserRole == "cm")
            {

                return RedirectToAction("CustomerProfile", "Users", new { id = Session["userId"] });
            }


            if (UserRole == "pm")
            {
                return RedirectToAction("PMHome", "Users", new { id = Session["userId"] });
            }
            if (UserRole == "tl")
            {
                return RedirectToAction("TeamLeaderProfile", "Users", new { id = Session["userId"] });
            }
            if (UserRole == "admin")
            {
                return RedirectToAction("AdminProfile", "Users", new { id = Session["userId"] });
            }
            else if (UserRole == "jd")
            {
                return RedirectToAction("JonurDeveloperProfile", "Users", new { id = Session["userId"] });
            }

            else return View("Index");
        }


        public ActionResult regester()
        {
             return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult regester(string UserEmail,string UserFirstName, string UserLasttName,string UserPhone, string UserRole,string UserPassword)
        {
            User user = new User();
            user.UserEmail = UserEmail;
            user.UserPassword = UserPassword;
            user.UserRole = UserRole;
            user.UserLasttName = UserLasttName;
            user.UserFirstName = UserFirstName;
            user.NumOFProject = 0;
            user.Qualifications = " ";
            user.UserExpirienceYears = 0;
            user.UserJob_description = "";
            user.UserPhoto = "~/ Content / 4.jpg";
            user.UserPhone = UserPhone;
            db.users.Add(user);
            db.SaveChanges();
            User user1 = db.users.SingleOrDefault(u => u.UserEmail == UserEmail && u.UserPassword == UserPassword && u.UserRole == UserRole);
            Session["userId"] = user1.UserId;
            if (UserRole == "cm")
            {

                return RedirectToAction("CustomerProfile", "Users", new { id = Session["userId"] });
            }


            if (UserRole == "pm")
            {
                return RedirectToAction("PMHome", "Users", new { id = Session["userId"] });
            }
            if (UserRole == "tl")
            {
                return RedirectToAction("TeamLeaderProfile", "Users", new { id = Session["userId"] });
            }
            if (UserRole == "admin")
            {
                return RedirectToAction("AdminProfile", "Users", new { id = Session["userId"] });
            }
            else if (UserRole == "jd")
            {
                return RedirectToAction("JonurDeveloperProfile", "Users", new { id = Session["userId"] });
            }

            else return View("Index");
        }



        public ActionResult Logout()
        {
            Session["userId"] = null;

            return RedirectToAction("Index");
        }

        // GET: Projects/Create
        public ActionResult CreateProject()
        {
            string role =(string)Session["userRole"];


            return role == "cm" ? View() : (ActionResult)RedirectToAction("Login");

        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProject([Bind(Include = "ProjectID,ProjectName,ProjectDescription,Projectstatus,StartDate,endData,price")] Project project)
        {
            project.Projectstatus = 0;
            project.StartDate = DateTime.Now.ToString();
            project.endData = "0 - 0 - 0";
            project.price = 0;
            if (ModelState.IsValid)
            {
                db.projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);


        }




    }
}