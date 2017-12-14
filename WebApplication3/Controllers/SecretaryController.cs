using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using System.Web.Security;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;

namespace WebApplication3.Controllers
{
    public class SecretaryController : Controller
    {
        private SecDB1 db = new SecDB1();

        // GET: Secretary
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View(db.Secs.ToList());
            }
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post_Index()
        {
            List<Sec> list1 = new List<Sec>();
            string pid = Request["pid"];
            if (pid == "")
            {
                IQueryable<Sec> q = from s in db.Secs  select s;
                foreach (var i in q)
                {
                    list1.Add(i);
                }
                ViewBag.pid = pid;
            }
            else
            {
                IQueryable<Sec> q = from s in db.Secs where s.PatientId == pid select s;
                foreach (var i in q)
                {
                    list1.Add(i);
                }
                ViewBag.pid = pid;
            }

            return View(list1);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult sout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session["uns"] = "";

            return RedirectToAction("Login", "Login");

        }

        // GET: Secretary/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Sec sec = db.Secs.Find(id);
                if (sec == null)
                {
                    return HttpNotFound();
                }
                return View(sec);
            }
        }

        // GET: Secretary/Create
        public ActionResult Create()
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View();
            }
        }

        // POST: Secretary/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PatientId,FirstName,SurName,DOB,Address,Gender,ContactNumber,Appointment,AppointmentTime,NextOfKin,MedicalCard,EthnicOrigin,Religion,GPContactDetails,Occupation,Maritalstatus")] Sec sec)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {

                if (ModelState.IsValid)
                {
                    db.Secs.Add(sec);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(sec);
            }
        }

        // GET: Secretary/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Sec sec = db.Secs.Find(id);
                if (sec == null)
                {
                    return HttpNotFound();
                }
                return View(sec);
            }
        }

        // POST: Secretary/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PatientId,FirstName,SurName,DOB,Address,Gender,ContactNumber,Appointment,AppointmentTime,NextOfKin,MedicalCard,EthnicOrigin,Religion,GPContactDetails,Occupation,Maritalstatus")] Sec sec)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {

                if (ModelState.IsValid)
                {
                    db.Entry(sec).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(sec);
            }
        }

        // GET: Secretary/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Sec sec = db.Secs.Find(id);
                if (sec == null)
                {
                    return HttpNotFound();
                }
                return View(sec);
            }
        }

        // POST: Secretary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Consultant"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {

                Sec sec = db.Secs.Find(id);
                db.Secs.Remove(sec);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult signout()
        {
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            FormsAuthentication.SignOut();
            Session.Abandon();
            //Session["uns"] = "";
            return RedirectToAction("Login", "Login");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}
