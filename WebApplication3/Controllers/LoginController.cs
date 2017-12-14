using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using System.Data.SqlClient;
using System.Web.Security;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;

namespace WebApplication3.Controllers
{
    public class LoginController : Controller
    {
        private LogDB db = new LogDB();

        // GET: Login
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [ActionName("Login")]
        public ActionResult Post_Login(Models.LogClass1 user1)
        {
            try
            {           

            string un = user1.UserName;
            string pass = user1.Password;

            if (isvalidx(un, pass))
            {
                string opt = System.Web.HttpContext.Current.Session["rol"].ToString();
                if (opt == "Secretary")
                {
                    return Redirect("Secretary/Index");
                }
                if (opt == "Nurse")
                {
                    return Redirect("Nurse/Index");
                }
                if (opt == "Consultant")
                {
                    return Redirect("Consultant/Index");
                }
            }
            else
            {
                ViewBag.err = "Login Incorrect";
            }
            }
            catch (Exception e1) { }
            return View();
            
        }
        public ActionResult sout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session["uns"] = "";

            return RedirectToAction("Login", "Login");

        }
        public bool isvalidx(string user, string pass)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=aspnet-WebApplication3-20171014042442;Integrated Security=True");

            string str = "select * from LogClass1 where UserName=@UserName and Password=@Password";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@UserName", user);
            cmd.Parameters.AddWithValue("@Password", pass);

            con.Open();
            SqlDataReader sr = cmd.ExecuteReader();
            if (sr.Read() == true)
            {
                if (sr["UserName"].ToString() == user)
                {

                    string role = sr["Role"].ToString();
                    if (role == "0") { role = "Secretary"; }
                    if (role == "1") { role = "Nurse"; }
                    if (role == "2") { role = "Consultant"; }
                    System.Web.HttpContext.Current.Session["rol"] = role;
                    System.Web.HttpContext.Current.Session["uns"] = user;
                }

                return true;
            }
            else
            {
                return false;
            }
            con.Close();
        }


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public ActionResult Index()
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Nurse")|| (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View(db.LogClass1s.ToList());
            }

        }

        // GET: Login/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Nurse") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LogClass1 logClass1 = db.LogClass1s.Find(id);
                if (logClass1 == null)
                {
                    return HttpNotFound();
                }
                return View(logClass1);
            }
        }

        // GET: Login/Create
        public ActionResult Create()
        {           
                return View();           
        }

        // POST: Login/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Password,Confirm_Password,Role,Mobile")] LogClass1 logClass1)
        {
            
                if (ModelState.IsValid)
                {
                    db.LogClass1s.Add(logClass1);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(logClass1);
            
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Nurse") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LogClass1 logClass1 = db.LogClass1s.Find(id);
                if (logClass1 == null)
                {
                    return HttpNotFound();
                }
                return View(logClass1);
            }
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Password,Confirm_Password,Role,Mobile")] LogClass1 logClass1)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Nurse") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {

                if (ModelState.IsValid)
                {
                    db.Entry(logClass1).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(logClass1);
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Nurse") || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LogClass1 logClass1 = db.LogClass1s.Find(id);
                if (logClass1 == null)
                {
                    return HttpNotFound();
                }
                return View(logClass1);
            }
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LogClass1 logClass1 = db.LogClass1s.Find(id);
            db.LogClass1s.Remove(logClass1);
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
