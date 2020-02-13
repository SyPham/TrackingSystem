using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

            //check languages for users can not, edit page example same
            var teamids = db.Teams;
            var departments = db.Departments;

            var model = from team in teamids
                        join dep in departments on team.DepartmentID equals dep.DepartmentID
                        select new TeamVM
                        {
                            TeamID = team.ID,
                            TeamName = team.Name,
                            DepartmentID = dep.DepartmentID,
                            Department = dep.Name,
                        };
            var data = model.OrderBy(x=>x.TeamID).ToList().ToPagedList(page, 10);
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
        public async Task<ActionResult> Create()
        {
            ViewBag.Department =await db.Departments.ToListAsync();
            return View();
        }

        // POST: Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Team team, string nameVI, string nameEN, string NameTW)
        {
            try
            {
                team.Name = nameVI + " - " + nameEN + " - " + NameTW; ;
                db.Teams.Add(team);
                await db.SaveChangesAsync();
                TeamLang vn = new TeamLang();
                vn.Name = nameVI;
                vn.LanguageID = "vi";
                vn.TeamID = team.ID;
                db.TeamLangs.Add(vn);

                TeamLang en = new TeamLang();
                en.Name = nameEN;
                en.LanguageID = "en";
                en.TeamID = team.ID;
                db.TeamLangs.Add(en);

                TeamLang tw = new TeamLang();
                tw.Name = NameTW;
                tw.LanguageID = "en";
                tw.TeamID = team.ID;
                db.TeamLangs.Add(tw);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return View(team);
            }
        }


        public async Task<ActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Department = await db.Departments.ToListAsync();
            var team = await db.Teams.FindAsync(id);
            var itemVi = await db.TeamLangs.FirstOrDefaultAsync(x => x.TeamID == id && x.LanguageID == "vi");
            var itemEn = await db.TeamLangs.FirstOrDefaultAsync(x => x.TeamID == id && x.LanguageID == "en");
            var itemTw = await db.TeamLangs.FirstOrDefaultAsync(x => x.TeamID == id && x.LanguageID == "tw");
            if (itemVi == null) ViewBag.VI = team.Name; else ViewBag.VI = itemVi.Name;
            if (itemEn == null) ViewBag.EN = team.Name; else ViewBag.EN = itemEn.Name;
            if (itemTw == null) ViewBag.TW = team.Name; else ViewBag.TW = itemTw.Name;
            if (team == null)
            {
                return HttpNotFound();
            }

            var user = (DemoDoan.ViewModel.UserVM)Session["ACCOUNT"];
            //query lay tat ca record theo ngon ngu hien ma user chon

            var LangID = Session[UserVM.CurrentCulture].ToString();
            ViewBag.Languages = db.Language.ToList();
            return View(team);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Team team, string nameVI, string nameEN, string nameTW)
        {
            try
            {
                var id = team.ID;
                var itemOri = await db.Teams.FindAsync(id);
                var itemVi = await db.TeamLangs.FirstOrDefaultAsync(x => x.TeamID == id && x.LanguageID == "vi");
                var itemEn = await db.TeamLangs.FirstOrDefaultAsync(x => x.TeamID == id && x.LanguageID == "en");
                var itemTw = await db.TeamLangs.FirstOrDefaultAsync(x => x.TeamID == id && x.LanguageID == "tw");

                await db.SaveChangesAsync();
                if (itemVi == null)
                {
                    TeamLang vn = new TeamLang();
                    vn.Name = nameVI;
                    vn.LanguageID = "vi";
                    vn.TeamID = team.ID;
                    db.TeamLangs.Add(vn);
                }
                else
                {
                    itemVi.Name = nameVI;
                }
                if (itemTw == null)
                {
                    TeamLang tw = new TeamLang();
                    tw.Name = nameTW;
                    tw.LanguageID = "tw";
                    tw.TeamID = team.ID;
                    db.TeamLangs.Add(tw);
                }
                else
                {
                    itemTw.Name = nameTW;
                }
                if (itemEn == null)
                {
                    TeamLang en = new TeamLang();
                    en.Name = nameEN;
                    en.LanguageID = "en";
                    en.TeamID = team.ID;
                    db.TeamLangs.Add(en);
                }
                else
                {
                    itemEn.Name = nameEN;
                }
                itemOri.Name = nameVI + " - " + nameEN + " - " +nameTW;
                itemOri.DepartmentID = team.DepartmentID;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return View(team);

            }
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
