using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DemoDoan.Models;
using DemoDoan.ViewModel;
using PagedList;

namespace DemoDoan.Controllers
{
    public class LocationsController : BaseController
    {
        private OurDbContext db = new OurDbContext();

        // GET: Locations
        public ActionResult Index(int page = 1)
        {
            var user = (DemoDoan.ViewModel.UserVM)Session["ACCOUNT"];
            //query lay tat ca record theo ngon ngu hien ma user chon

            var LangID = Session[UserVM.CurrentCulture].ToString();
            ViewBag.Teams = db.Locations.Where(x => x.LanguageID == LangID);

            return View(db.Locations.OrderBy(x=>x.LanguageID).ToPagedList(page,10));
        }

        // GET: Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            var user = (DemoDoan.ViewModel.UserVM)Session["ACCOUNT"];
            //query lay tat ca record theo ngon ngu hien ma user chon

            var LangID = Session[UserVM.CurrentCulture].ToString();
            ViewBag.Locations = db.Locations.Where(x => x.LanguageID == LangID);
            ViewBag.Department = db.Departments.Where(x => x.LanguageID == LangID);
            ViewBag.Team = db.Teams.Where(x => x.LanguageID == LangID);
            ViewBag.Language = db.Language;
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Number,Content,Remark,DepartmentID,TeamID,LanguageID")] Location location)
        {
            if (ModelState.IsValid)
            {
                db.Locations.Add(location);
                location.LanguageID = Session[UserVM.CurrentCulture].ToString();
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(location);
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Location location = db.Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            var user = (DemoDoan.ViewModel.UserVM)Session["ACCOUNT"];
            //query lay tat ca record theo ngon ngu hien ma user chon

            var LangID = Session[UserVM.CurrentCulture].ToString();
            ViewBag.Locations = db.Locations.Where(x => x.LanguageID == LangID);
            ViewBag.Department = db.Departments.Where(x => x.LanguageID == LangID);
            ViewBag.Team = db.Teams.Where(x => x.LanguageID == LangID);
            ViewBag.Language = db.Language;
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Number,Content,Remark,DepartmentID,TeamID,LanguageID")] Location location)
        {
            if (ModelState.IsValid)
            {
                location.LanguageID = Session[UserVM.CurrentCulture].ToString();
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(location);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            bool status;
            Location location = db.Locations.Find(id);
            db.Locations.Remove(location);
            try
            {
                db.SaveChanges();
                status = true;
            }
            catch
            {
                status = false;
            }
            return Json(new
            {
                status,
                url = "/Locations"
            }, JsonRequestBehavior.AllowGet);
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
