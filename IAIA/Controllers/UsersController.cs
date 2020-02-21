using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IAIA.Models;

namespace IAIA.Controllers
{
    public class UsersController : Controller
    {
        private testEntities db = new testEntities();

        // GET: Users
        public ActionResult Index(int ?id)
        {

            if (Session["user"] != null)
            {

                List<User> users = db.users.Where(x => x.UserId != id).ToList();
                ViewBag.users = users;
                return View(users);
            }
            else
            {
               return (ActionResult)RedirectToAction("../Home/Login");
            }
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        
        {

            if (Session["user"] == null)
            {
                return (ActionResult)RedirectToAction("../Home/Login");
            }
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return (ActionResult)RedirectToAction("../Home/Login");
            }
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       
            public ActionResult Create(string UserEmail, string UserPassword, string UserRole)
            {
            return View();
        }
            

         

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["user"] == null)
            {
                return (ActionResult)RedirectToAction("../Home/Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserPhoto,UserExpirienceYears,UserEmail,UserJob_description,UserFirstName,UserLasttName,UserPhone,UserRole,UserPassword,NumOFProject,Qualifications")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["user"] == null)
            {
                return (ActionResult)RedirectToAction("../Home/Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.users.Find(id);
            db.users.Remove(user);
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

//---------------------------------------------------Customer information------------------------------------------------------------------------//
        public ActionResult CustomerProfile(int id)
        {

            if (Session["user"] == null)
            {
                return (ActionResult)RedirectToAction("../Home/Login");
            }

            User user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }


        public ActionResult CustomernotdeliveredProjects()
        {

            if (Session["user"] == null)
            {
                return (ActionResult)RedirectToAction("../Home/Login");
            }
            int id = (int)Session["userId"];

            
            List<Project> projects = db.projects.Where(x => x.Projectstatus == 0 || x.Projectstatus==1 ||x.Projectstatus==2).ToList();
            return View(projects);
            
        }


        public ActionResult CustomerdeliveredProjects()
        {

            if (Session["user"] == null)
            {
                return (ActionResult)RedirectToAction("../Home/Login");
            }
            int id = (int)Session["userId"];


            List<Project> projects = db.projects.Where(x => x.Projectstatus == 3).ToList();
            return View(projects);

        }

        public ActionResult Asignpm(int id,int state)
        {

            if (Session["user"] == null)
            {
                return (ActionResult)RedirectToAction("../Home/Login");
            }
            if (state != 0)
            {
                return RedirectToAction("Index", "projects");
            }
            else
            {
                ViewBag.state = state;
                ViewBag.pid= id;
                List<User> users = db.users.Where(x => x.UserRole == "pm").ToList();
                return View(users);
            }
            
        }


        public ActionResult Asign(int pid, int pmid, int state) 
        {
            Project project = db.projects.Find(pid);
            project.Projectstatus = 1;
            db.Entry(project).State = EntityState.Modified;
            db.SaveChanges();
            UserProject userProjects = db.userProjects.SingleOrDefault(x => x.userId == pmid && x.ProjectId == pid);
            if (userProjects != null) { }
            else
            {
                UserProject userProject = new UserProject();

                userProject.userId = pmid;
                userProject.ProjectId = pid;

                db.userProjects.Add(userProject);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Projects");

        }

        //----------------------------------------------end customer info-----------------------------------------------------------//

        //-------------------------------------------------admin information---------------------------------------------------------//

        public ActionResult AdminProfile(int id)
        {
            List<User> users = db.users.Where(x => x.UserId != id).ToList();
            ViewBag.users = users;

            User user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //---------------------------------------------------end admin infornation el7 :D-------------------------------------------------------------//
        //----------------------------------------start project manader information :(-------------------------------------------------------//


        public ActionResult ProjectManagerProfile(int? id)
        {
            ViewBag.pmid = id;
            User user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        public ActionResult ListNotAssign()
        {
            List<Project> projects = db.projects.Where(x => x.Projectstatus != 1).ToList();
            return View(projects);
        }



        public ActionResult PMHome(int id)
        {
            ViewBag.pmid = id;
            List<UserProject> userProjects = db.userProjects.Where(x => x.userId == id).ToList();
            List<Project> projects = new List<Project>();
            foreach (var item in userProjects)
            {
                int pid = item.ProjectId;
                Project project = db.projects.Find(pid);
                if (project.Projectstatus == 1||project.Projectstatus==2)
                {
                    bool alreadyExist = projects.Contains(project);
                    if (alreadyExist) {}
                    else
                    {    projects.Add(project); }
                }
               
            } return View(projects);
        }


        public ActionResult AssignTL(int pid,int pmid,string search)
        {
            ViewBag.pid = pid;
            ViewBag.pmid = pmid;

            if (search == null)
            {
                List<User> users = db.users.Where(x => x.UserRole == "tl").ToList();
                return View(users);
            }
            else {
                List<User> users = db.users.Where(x => x.UserFirstName.StartsWith(search)  && x.UserRole=="tl").ToList();
           return View(users);
            }
            
        }

        public ActionResult AssignJD(int? pid, int pmid, string search)
        {
            ViewBag.pid = pid;
            ViewBag.pmid = pmid;

            if (search == null)
            {
                List<User> users = db.users.Where(x => x.UserRole == "jd").ToList();
                return View(users);
            }
            else
            {
                List<User> users = db.users.Where(x => x.UserFirstName.StartsWith(search) && x.UserRole == "jd").ToList();
                return View(users);
            }

        }

        // GET
        public ActionResult SendRequst(int pid, int pmid, int toid)
        {
            Requst request = new Requst();
            request.ToUser = toid;
            request.FromUser = pmid;
            request.projectid = pid;
            request.isAccepted = 0;
            request.sentAt = DateTime.Now.ToString();
            Project project = db.projects.Find(pid);
            project.Projectstatus = 2;
            UserProject userProject = db.userProjects.SingleOrDefault(x => x.userId == toid && x.ProjectId == pid);
            if (userProject != null) { }
            else
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();

                List<UserProject> userProjects = db.userProjects.Where(x => x.userId == toid && x.ProjectId == pid).ToList();


                db.requsts.Add(request);
                db.SaveChanges();
            }
            return View();
        }

        /*
            [HttpPost]

            public ActionResult SendRequst(int pid ,int pmid,int toid)
            {
                Requst request = new Requst();

                request.ToUser = toid;
                request.FromUser = pmid;
                request.projectid = pid;

                db.requsts.Add(request);
                db.SaveChanges();
                 return RedirectToAction("PMHome",new { id=pmid});

            }*/

        public ActionResult ManagingPRojectBypm(int? pmid)
        {
            if (pmid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<UserProject> userProjects = db.userProjects.Where(x=>x.userId==pmid).ToList();
            List<Project> projects = new List<Project> ();
            foreach (var item in userProjects)
            {
                int pid = item.ProjectId;
                Project project = db.projects.Find(pid);
                if (project.Projectstatus == 1 ||project.Projectstatus==2)
                {
                        bool alreadyExist = projects.Contains(project);
                         if (alreadyExist)
                             {

                             }
                         else
                         { projects.Add(project); }
                         }
                
            }
            
            return View(projects);
        }



        public ActionResult DeleveredPRojectsByPm(int? pmid)
        {

            if (pmid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<UserProject> userProjects = db.userProjects.Where(x => x.userId == pmid).ToList();
            List<Project> projects = new List<Project>();
            foreach (var item in userProjects)
            {
                int pid = item.ProjectId;
                Project project = db.projects.Find(pid);
                if (project.Projectstatus == 3)
                {
                    bool alreadyExist = projects.Contains(project);
                    if (alreadyExist)
                    {

                    }
                    else
                    { projects.Add(project); }
                }

            }

            return View(projects);
        }

        public ActionResult SearchForTeamLeader(int? pmid,  string search)
        {
            ViewBag.pmid = pmid;
            List<User> users = db.users.Where(x => x.UserRole == "tl").ToList();

            return View(users);
            
        }


        // GET: Users/Edit/5
        public ActionResult Set_Schedule_for_project(int? id,int pmid)
        {
            ViewBag.pmid = pmid;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Set_Schedule_for_project([Bind(Include = "ProjectID,ProjectName,ProjectDescription,Projectstatus,StartDate,endData,price")]Project project,int pmid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PMHome",new { id=pmid});
            }
            return View(project);
        }



        public ActionResult isDelivered(int pid)
        {
            Project project = db.projects.Find(pid);
            return View(project);
        }

        public ActionResult Delivered(int pid)
        {
            Project project = db.projects.Find(pid);
            project.Projectstatus = 3;
            db.Entry(project).State = EntityState.Modified;
            db.SaveChanges();
            return View (project);

        }

        public ActionResult on_progress(int pid)
        {
            Project project = db.projects.Find(pid);
            project.Projectstatus = 2;
            db.Entry(project).State = EntityState.Modified;
            db.SaveChanges();
            return View(project);


        }
        public ActionResult comment(int pid,int ?pmid)
        {
            ViewBag.pid = pid;
            ViewBag.pmid = pmid;
            List<Comment> comments = db.comments.Where(x => x.projectid == pid ).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult comment(int pid,int pmid,string content)
        {
            Comment comment = new Comment();
            comment.sentAt = DateTime.Now.ToString();
            comment.projectid = pid;
            comment.pmid = pmid;
            comment.content = content;

                db.comments.Add(comment);
                db.SaveChanges();
            
            return RedirectToAction("PMHome",new { id=pmid});
        }



        public ActionResult SetPrice(int? id, int pmid)
        {
            ViewBag.pmid = pmid;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetPrice([Bind(Include = "ProjectID,ProjectName,ProjectDescription,Projectstatus,StartDate,endData,price")]Project project, int pmid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PMHome", new { id = pmid });
            }
            return View(project);
        }


        public ActionResult listAllMember(int pid,int pmid)
        {
            ViewBag.pid = pid;
            ViewBag.pmid = pmid;


            List<UserProject> userProjects = db.userProjects.Where(x => x.ProjectId==pid).ToList();
            List<User> users = new List<User>();
            foreach (var item in userProjects)
            {
                
                int uid = item.userId;
                 User user= db.users.Find(uid);
                  bool alreadyExist = users.Contains(user);
                    if (alreadyExist)
                    {
                    }
                    else
                    { users.Add(user); }
                
              
            }

            return View(users);
        }

        public ActionResult Remove(int id, int pid,int pmid)
        {

            UserProject userProject = db.userProjects.FirstOrDefault(x => x.userId == id && x.ProjectId == pid);
            if (db.users.Find(pmid).UserRole == "pm")
            {
                db.userProjects.Remove(userProject);
                db.SaveChanges();
                return RedirectToAction("PMHome", new { id = pmid });
            }
            else
            {
                Requst requsttl = new Requst();
                requsttl.projectid = pid;
                requsttl.ToUser = pmid;
                requsttl.FromUser = id;
                requsttl.isAccepted = 0;
                requsttl.sentAt = DateTime.Now.ToString();
                db.requsts.Add(requsttl);
                db.SaveChanges();
                return RedirectToAction("TeamLeaderProfile", new { id = pmid });
            
        }
        }

        public ActionResult leave(int id,int pmid)
        {

            UserProject userProject = db.userProjects.SingleOrDefault(x => x.userId == pmid && x.ProjectId == id);
          
            User user = db.users.Find(pmid);
            Requst requst = db.requsts.Where(x => x.projectid == id && x.isAccepted==1 && x.ToUser==pmid).Single();
            if (user.UserRole == "pm")
            {
                db.userProjects.Remove(userProject);
                db.SaveChanges();
                
                return RedirectToAction("PMHome", new { id = pmid });
            }
           
            else if (user.UserRole == "tl") {
                Requst requsttl = new Requst();
                requsttl.projectid = id;
                requsttl.ToUser = requst.FromUser;
                requsttl.FromUser = pmid;
                requsttl.isAccepted = 0;
                requsttl.sentAt = DateTime.Now.ToString();
                db.requsts.Add(requsttl);
                db.SaveChanges();
                db.userProjects.Remove(userProject);
                db.SaveChanges();
                return RedirectToAction("TeamLeaderProfile", new { id = pmid }); }
            else
            {
                Requst requsttl = new Requst();
                requsttl.projectid = id;
                requsttl.ToUser = requst.FromUser;
                requsttl.FromUser = pmid;
                requsttl.isAccepted = 0;
                requsttl.sentAt = DateTime.Now.ToString();
                db.requsts.Add(requsttl);
                db.SaveChanges();
                db.userProjects.Remove(userProject);
                db.SaveChanges();
                return RedirectToAction("JonurDeveloperProfile", new { id = pmid });
            }
            
        }

        public ActionResult feedbacks()
        {
            List<Feedback> feedbacks = db.feedbacks.ToList();
            List<string> con = new List<string>();
            foreach(var i in feedbacks)
            {
                User user = db.users.Find(i.ToUser);
                User user1 = db.users.Find(i.FromUser);
                string a = "  this   a feed back  from:   ";
                string pname = user1.UserFirstName;
                string conc1 = string.Concat(a, pname);
                string b = "  To evaluate  :  ";
                string name = user.UserFirstName;      
           
                string conc2 = string.Concat(b, name);
                string content = i.feedbackContent;
                string conc3 = string.Concat( conc1,conc2);
                string conc4 = string.Concat(conc3, content);
                con.Add(conc4);

            }
            ViewBag.list = con;
            return View(con);
        }
     
        //--------------------------------end project manager zft information :(-----------------------------------------------------------------//


        //------------------------------------- Team Leader---------------------------------------------------------------------------//

        public ActionResult TeamLeaderProfile(int id)
        {
            ViewBag.tlid = id;
            User user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult listRequstes(int tlid)
        {
            ViewBag.tlid = tlid;
           
            List<Requst> requsts = db.requsts.Where(x=>x.ToUser==tlid &&x.isAccepted==0).ToList();
            List<string> con = new List<string> ();
            foreach (var item in requsts)
            {
                int pid = item.projectid;
                int uid = item.FromUser;
                User user = db.users.Find(uid);
                Project project = db.projects.Find(pid);
                if (project != null)
                {
                    string pname = project.ProjectName;
                    string a = "  this requst is from:   ";
                    string b = "  To goin project:        ";
                    string conc1 = string.Concat(b,pname);

                    string uname = user.UserFirstName;
                    string conc2 = string.Concat(a, uname);

                    string conc3 = string.Concat(conc2, conc1);
                    con.Add(conc3);
                }
                else { 
                  
                    User user1 = db.users.Find(uid);
                    if (user1.UserRole == "tl") { 
                    return RedirectToAction("TeamLeaderProfile", new { id = tlid }); }
                    else if(user1.UserRole == "jd")
                    {
                        return RedirectToAction("JonurDeveloperProfile", new { id = tlid });
                    }
                }
            }
            ViewBag.list = con;
            return View(requsts);
        }


        public ActionResult Accept(int rid,int toid,int pid)
        {
            Requst requst = db.requsts.Find(rid);
            requst.isAccepted = 1;
            Project project = db.projects.Find(pid);
            project.Projectstatus = 2;
            db.Entry(requst).State = EntityState.Modified;
            db.SaveChanges();
            UserProject userProject = new UserProject();
            userProject.ProjectId = project.ProjectID;
            userProject.userId = toid;
            db.userProjects.Add(userProject);
            db.SaveChanges();
            User user1 = db.users.Find(toid);
            if (user1.UserRole == "tl")
            {
                return RedirectToAction("TeamLeaderProfile", new { id = toid });
            }
            else if (user1.UserRole == "jd")
            {
                return RedirectToAction("JonurDeveloperProfile", new { id = toid });
            }
            else
            {
                Requst requst1 = db.requsts.Find(rid);
                requst.isAccepted = 1;
                Project project1 = db.projects.Find(pid);
                project.Projectstatus = 2;
                db.Entry(requst).State = EntityState.Modified;
                db.SaveChanges();
                UserProject userProject1 = new UserProject();
                userProject.ProjectId = project.ProjectID;
                userProject.userId = toid;
                db.userProjects.Remove(userProject1);
                db.SaveChanges();
                return RedirectToAction("PMHome", new { id = toid });
            }

        }

        public ActionResult reject(int rid, int toid)
        {
            Requst requst = db.requsts.Find(rid);
            
            User user1 = db.users.Find(toid);
            if (user1.UserRole == "tl")
            {
                db.requsts.Remove(requst);
            db.SaveChanges();
                return RedirectToAction("TeamLeaderProfile", new { id = toid });
            }
            else if (user1.UserRole == "jd")
            {
                db.requsts.Remove(requst);
                db.SaveChanges();
                return RedirectToAction("JonurDeveloperProfile", new { id = toid });
            }
            else
            {
                db.requsts.Add(requst);
                db.SaveChanges();
                return RedirectToAction("PMHome", new { id = toid });
            }

        }


        public ActionResult CurrentProjects(int tlid)
        {
            ViewBag.tlid = tlid;
            List<UserProject> userProjects = db.userProjects.Where(x => x.userId == tlid).ToList();
            List<Project> projects = new List<Project>();
            foreach (var item in userProjects)
            {
                int pid = item.ProjectId;
               Project project = db.projects.FirstOrDefault(x=>x.ProjectID==pid &&x.Projectstatus==2);
                bool alreadyExist = projects.Contains(project);
                if (alreadyExist){ }
                else
                {  projects.Add(project); } }
            
            return View(projects); }

        public ActionResult Delevered(int tlid)
        {
            ViewBag.tlid = tlid;
            List<UserProject> userProjects = db.userProjects.Where(x => x.userId == tlid).ToList();
            List<Project> projects = new List<Project>();
            foreach (var item in userProjects)
            {
                int pid = item.ProjectId;
                Project project = db.projects.FirstOrDefault(x => x.ProjectID == pid && x.Projectstatus == 3);
                bool alreadyExist = projects.Contains(project);
                if (alreadyExist) { }
                else
                { projects.Add(project); }
            }

            
            return View(projects);
        }


        public ActionResult AssignJDd(int id)
        {
            ViewBag.tlid = id;
            List<UserProject> userProjects = db.userProjects.Where(x => x.userId == id).ToList();
            List<Project> projects = new List<Project>();
            foreach (var item in userProjects)
            {
                int pid = item.ProjectId;
                Project project = db.projects.Find(pid);
                if ( project.Projectstatus == 2)
                {
                    bool alreadyExist = projects.Contains(project);
                    if (alreadyExist) { }
                    else
                    { projects.Add(project); }
                }

            }
            return View(projects);
        }

        public ActionResult listAlljd( int ?tlid)
        {
            
            ViewBag.tlid = tlid;

            List<User> users = db.users.Where(x => x.UserRole == "jd").ToList(); 
             return View(users);
        }

        public ActionResult feedback(int uid,int ? tlid)
        {
            ViewBag.jdid = uid;
            ViewBag.tlid = tlid;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult feedback(string feedbackContent, int Evaluate,int ? FromUser,int ToUser)
        {
            Feedback feedback = new Feedback();
            feedback.FromUser = (int)Session["userId"];
            feedback.feedbackContent = feedbackContent;
            feedback.ToUser = ToUser;
            feedback.Evaluate = Evaluate;
            db.feedbacks.Add(feedback);
            db.SaveChanges();
            return RedirectToAction("TeamLeaderProfile",new { id=feedback.FromUser});
            

           
        }


        //------------------------------------------------not end team leader---------------------------------------------------------//

        //-------------------------------------------------- Junior Developers-------------------------------------------------------//


        public ActionResult JonurDeveloperProfile(int id)
        {
            ViewBag.pmid = id;
            User user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        public ActionResult current(int id)
        {
            ViewBag.pmid = id;
            List<UserProject> userProjects = db.userProjects.Where(x => x.userId == id ).ToList();
            List<Project> projects = new List<Project>();
            foreach(var i in userProjects)
            {
                int pid = i.ProjectId;
                Project project = db.projects.Find(pid);
                if (project.Projectstatus != 3)
                {
                    bool exict = projects.Contains(project);
                    if (exict) { }
                    else
                    {
                        projects.Add(project);
                    }
                }

            }
            return View(projects);
        }
        public ActionResult getdata()
        {

            User UserExpirienceYears = db.users.Where(x => x.UserId == 2).Single();

            List<string> obj = new List<string>();

            int UserExpirienceYear = UserExpirienceYears.UserExpirienceYears;
            int nump = UserExpirienceYears.NumOFProject;

            ratio r = new ratio();
            r.UserExpirienceYears = UserExpirienceYear;
            // r.Qualifications = UserExpirienceYears.Qualifications;
            r.numOfProjects = UserExpirienceYears.NumOFProject;
            ViewBag.data = r;
            ViewBag.num = UserExpirienceYears.NumOFProject;
            ViewBag.ex = UserExpirienceYears.UserExpirienceYears;
            return Json(r, JsonRequestBehavior.AllowGet);
        }
        public class ratio
        {
            //public  string Qualifications { get; set; }
            public int UserExpirienceYears { get; set; }
            public int numOfProjects { get; set; }

        }

    }


}

   
    