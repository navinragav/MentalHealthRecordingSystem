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
    public class ConsultantController : Controller
    {
        private ConsDB db = new ConsDB();

        // GET: Consultant
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Secretary") || (Session["rol"].ToString() == "Nurse"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View(db.Conss.ToList());
            }
        }
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post_Index()
        {
            List<Cons> list1 = new List<Cons>();
            string pid = Request["pid"];
            if (pid == "")
            {
                IQueryable<Cons> q = from s in db.Conss select s;
                foreach (var i in q)
                {
                    list1.Add(i);
                }
                ViewBag.pid = pid;
            }
            else
            {
                IQueryable<Cons> q = from s in db.Conss where s.PatientId == pid select s;
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
        // GET: Consultant/Details/5

        public ActionResult Details(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Secretary") || (Session["rol"].ToString() == "Nurse"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cons cons = db.Conss.Find(id);
                if (cons == null)
                {
                    return HttpNotFound();
                }
                return View(cons);
            }
        }

        // GET: Consultant/Create
        public ActionResult Create()
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Secretary") || (Session["rol"].ToString() == "Nurse"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {

                return View();
            }
        }

        // POST: Consultant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PatientId,FirstName,MedicalHistory,UCD10,TreatmentPlan,Prescription,RaiseBillAmount")] Cons cons)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Secretary") || (Session["rol"].ToString() == "Nurse"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {

                if (ModelState.IsValid)
                {
                    db.Conss.Add(cons);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(cons);
            }
        }

        // GET: Consultant/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Secretary") || (Session["rol"].ToString() == "Nurse"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cons cons = db.Conss.Find(id);
                if (cons == null)
                {
                    return HttpNotFound();
                }
                return View(cons);
            }
        }

        // POST: Consultant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PatientId,FirstName,MedicalHistory,UCD10,TreatmentPlan,Prescription,RaiseBillAmount")] Cons cons)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Secretary") || (Session["rol"].ToString() == "Nurse"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {

                if (ModelState.IsValid)
                {
                    db.Entry(cons).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(cons);
            }
        }

        // GET: Consultant/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewBag.user = Session["uns"];
            ViewBag.role = Session["rol"];
            if ((Session["uns"] == null) || (Session["rol"].ToString() == "Secretary") || (Session["rol"].ToString() == "Nurse"))
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cons cons = db.Conss.Find(id);
                if (cons == null)
                {
                    return HttpNotFound();
                }
                return View(cons);
            }
        }

        // POST: Consultant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cons cons = db.Conss.Find(id);
            db.Conss.Remove(cons);
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
