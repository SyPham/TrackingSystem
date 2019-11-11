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
    public class TeamsController : BaseController
    {
        private OurDbContext db = new OurDbContext();

        // GET: Teams
        public ActionResult Index(int page = 1)
        {
            //Join do do lieu tu UserAccountVM len giao dien
            var LangID = Session[UserVM.CurrentCulture].ToString();

            //check languages for users can not, edit page example same
            var teamids = db.Teams.Where(x => x.LanguageID == LangID);
            var departments = db.Departments.Where(x => x.LanguageID == LangID);

            var model = from team in teamids
                        join dep in departments on team.DepartmentID equals dep.DepartmentID
                        join lang in db.Language on team.LanguageID equals lang.LanguageID
                        select new TeamVM
                        {
                            TeamID = team.TeamID,
                            TeamName = team.Name,
                            DepartmentID = dep.DepartmentID,
                            Department = dep.Name,
                            LanguageID = team.LanguageID,
                        };
            var data = model.ToList().ToPagedList(page,10);
            return View(data);
        }

        // GET: Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // GET: Teams/Create
        public ActionResult Create()
        {
            var user = (DemoDoan.ViewModel.UserVM)Session["ACCOUNT"];
            //query lay tat ca record theo ngon ngu hien ma user chon

            var LangID = Session[UserVM.CurrentCulture].ToString();
            ViewBag.Department = db.Departments.Where(x => x.LanguageID == LangID);
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Team team)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 1000);
                team.TeamID = randomNumber;
                db.Teams.Add(team);
                //kiem tra languages trong session va lay ra languages
                team.LanguageID = Session[UserVM.CurrentCulture].ToString();
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(team);
        }

        // GET: Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Department = db.Departments.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            var user = (DemoDoan.ViewModel.UserVM)Session["ACCOUNT"];
            //query lay tat ca record theo ngon ngu hien ma user chon

            var LangID = Session[UserVM.CurrentCulture].ToString();
            ViewBag.Department = db.Departments.Where(x => x.LanguageID == LangID);
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Team team)
        {
            if (ModelState.IsValid)
            {
                team.LanguageID = Session[UserVM.CurrentCulture].ToString();
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            bool status;
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
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
                url = "/Teams"
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
