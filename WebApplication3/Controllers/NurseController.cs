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
    public class NurseController : Controller
    {
        private NurseDB db = new NurseDB();

        // GET: Nurse
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Secretary"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View(db.Nurses.ToList());
            }
        }
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post_Index()
        {
            List<Nurse> list1 = new List<Nurse>();
            string pid = Request["pid"];
            if (pid == "")
            {
                IQueryable<Nurse> q = from s in db.Nurses select s;
                foreach (var i in q)
                {
                    list1.Add(i);
                }
                ViewBag.pid = pid;
            }
            else
            {
                IQueryable<Nurse> q = from s in db.Nurses where s.PatientId == pid select s;
                foreach (var i in q)
                {
                    list1.Add(i);
                }
                ViewBag.pid = pid;
            }

            return View(list1);
        }
        public ActionResult sout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session["uns"] = "";

            return RedirectToAction("Login", "Login");

        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        // GET: Nurse/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Secretary"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Nurse nurse = db.Nurses.Find(id);
                if (nurse == null)
                {
                    return HttpNotFound();
                }
                return View(nurse);
            }
        }

        // GET: Nurse/Create
        public ActionResult Create()
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Secretary"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View();
            }
        }

        // POST: Nurse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PatientId,FirstName,PastMedicalHistory,FamilyHistory,MentalStatusExamination,CollateralHistory,NursingCarePlan,AlergicSpecificMedication")] Nurse nurse)
        {
            if (ModelState.IsValid)
            {
                db.Nurses.Add(nurse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nurse);
        }

        // GET: Nurse/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Secretary"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Nurse nurse = db.Nurses.Find(id);
                if (nurse == null)
                {
                    return HttpNotFound();
                }
                return View(nurse);
            }
        }

        // POST: Nurse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PatientId,FirstName,PastMedicalHistory,FamilyHistory,MentalStatusExamination,CollateralHistory,NursingCarePlan,AlergicSpecificMedication")] Nurse nurse)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Secretary"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {

                if (ModelState.IsValid)
                {
                    db.Entry(nurse).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(nurse);
            }
        }

        // GET: Nurse/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Secretary"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Nurse nurse = db.Nurses.Find(id);
                if (nurse == null)
                {
                    return HttpNotFound();
                }
                return View(nurse);
            }
        }

        // POST: Nurse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Nurse nurse = db.Nurses.Find(id);
            db.Nurses.Remove(nurse);
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
