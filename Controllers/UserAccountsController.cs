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
    public class UserAccountsController : BaseController
    {
        private OurDbContext db = new OurDbContext();

        // GET: UserAccounts
        public ActionResult Index(int page=1)
        {
            //Join do do lieu tu UserAccountVM len giao dien
            var LangID = Session[UserVM.CurrentCulture].ToString();

            //check languages for users can not, edit page example same
            //var teamids = db.Teams.Where(x => x.LanguageID == LangID);
            //var departments = db.Departments.Where(x => x.LanguageID == LangID);
            //var userlanguages = db.UserLanguages.Where(x => x.LanguageID == LangID);
            //var userAccount = db.userAccount.Where(x => x.LanguageID == LangID);

            var model = from useraccount in db.userAccount
                        join dep in db.Departments on useraccount.DepartmentID equals dep.DepartmentID
                        join tea in db.Teams on useraccount.TeamID equals tea.TeamID
                        join role in db.Roles on useraccount.RoleID equals role.RoleID 
                        select new UserAccountVM
                        {
                            UserID = useraccount.UserID, //UserID khong co userID thi se khong nhan dien 'id' duoc trang edit va delete
                            Username = useraccount.Username,
                            Password = useraccount.Password,
                            Email = useraccount.Email,
                            Description = useraccount.Description,
                            Status = useraccount.Status,
                            IDcardNumber = useraccount.IDcardNumber,
                            RoleID = role.Name,
                            Department = dep.Name,
                            Team = tea.Name,
                        };
            var data = model.ToList().ToPagedList(page,10);
            return View(data);
            //var LangID = Session[UserVM.CurrentCulture].ToString();
            //return View(db.userAccount.ToList());
        }


        // GET: UserAccounts/Create
        public ActionResult Create()
        {
            var user = (DemoDoan.ViewModel.UserVM)Session["ACCOUNT"];
            //query lay tat ca record theo ngon ngu hien ma user chon

            var LangID = Session[UserVM.CurrentCulture].ToString();
            ViewBag.RoleID = db.Roles.ToList();
            ViewBag.Team = db.Teams.Where(x => x.LanguageID == LangID);
            ViewBag.Department = db.Departments.Where(x => x.LanguageID == LangID);
            ViewBag.Location = db.Locations.Where(x => x.LanguageID == LangID);
           
            return View();
        }

        public JsonResult GetbyDepartment(int departmentID)
        {
            //var LangID = Session[UserVM.CurrentCulture].ToString();
            //var teamName = db.Teams.Where(x => x.TeamID == teamName).ToList();
            //var teamID = db.Departments.Where(x => x.LanguageID == LangID).ToList();
            var LangID = Session[UserVM.CurrentCulture].ToString();

            //cau dieu kien lay team theo department
            var listdepartment = db.Teams.Where(x => x.LanguageID == LangID && x.DepartmentID == departmentID).ToList();

            return Json(new
            {
                //Tra ve
                status = true,
                //tra ve du lieu data = listdepartment
                data = listdepartment,
            }, JsonRequestBehavior.AllowGet);
        }

        // POST: UserAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                db.userAccount.Add(userAccount);
                var userLanguages = new UserLanguages();
                userLanguages.UserID = userAccount.UserID;
                userLanguages.LanguageID = Session[UserVM.CurrentCulture].ToString();
                userLanguages.TeamID = userAccount.TeamID;
                //
                db.UserLanguages.Add(userLanguages);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userAccount);
        }

        // GET: UserAccounts/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount userAccount = db.userAccount.Find(id);
            if (userAccount == null)
            {
                return HttpNotFound();
            }

            var user = (DemoDoan.ViewModel.UserVM)Session["ACCOUNT"];
            //query lay tat ca record theo ngon ngu hien ma user chon

            var LangID = Session[UserVM.CurrentCulture].ToString();
            //ViewBag.RoleID = db.Roles.Where(x => x.LanguageID == LangID);
            ViewBag.Team = db.Teams.Where(x => x.LanguageID == LangID);
            ViewBag.Department = db.Departments.Where(x => x.LanguageID == LangID);
            ViewBag.Location = db.Locations.Where(x => x.LanguageID == LangID);
            ViewBag.RoleID = db.Roles.ToList();

            return View(userAccount);
        }

        // POST: UserAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserAccount userAccount)
        {

            if (ModelState.IsValid)
            {
                userAccount.LanguageID = Session[UserVM.CurrentCulture].ToString();

                db.Entry(userAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userAccount);
        }

       
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            bool status;
            UserAccount userAccount = db.userAccount.Find(id);
            db.userAccount.Remove(userAccount);
            try
            {
                db.SaveChanges();
                status = true;
            }
            catch 
            {
                status = false;
            }
            return Json( new
            {
                status,
                url ="/UserAccounts"
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
