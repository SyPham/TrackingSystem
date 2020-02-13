using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DemoDoan.Dto;
using DemoDoan.helpers;
using DemoDoan.Models;
using DemoDoan.ViewModel;
using KPI.Model.helpers;
using PagedList;
using static DemoDoan.MvcApplication;

namespace DemoDoan.Controllers
{
    public class UserAccountsController : BaseController
    {
        private OurDbContext db = new OurDbContext();

        // GET: UserAccounts
        public ActionResult Index(int page = 1)
        {
            //Join do do lieu tu UserAccountVM len giao dien
            var LangID = Session[UserVM.CurrentCulture].ToString();

            //check languages for users can not, edit page example same
            //var teamids = db.Teams.Where(x => x.LanguageID == LangID);
            //var departments = db.Departments.Where(x => x.LanguageID == LangID);
            //var userlanguages = db.UserLanguages.Where(x => x.LanguageID == LangID);
            //var UserAccounts = db.UserAccounts.Where(x => x.LanguageID == LangID);

            var model = from useraccount in db.UserAccounts
                        join dep in db.Departments on useraccount.DepartmentID equals dep.DepartmentID
                        join tea in db.Teams on useraccount.TeamID equals tea.ID
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
            var data = model.ToList().ToPagedList(page, 10);
            return View(data);
            //var LangID = Session[UserVM.CurrentCulture].ToString();
            //return View(db.UserAccounts.ToList());
        }


        // GET: UserAccounts/Create
        public ActionResult Create()
        {
            var user = (DemoDoan.ViewModel.UserVM)Session["ACCOUNT"];
            var LangID = Session[UserVM.CurrentCulture].ToString();
            //query lay tat ca record theo ngon ngu hien ma user chon
            ViewBag.Team = db.Teams.Select(x => new TeamDto { ID = x.ID, DepartmentID = x.DepartmentID, Name = x.TeamLangs.FirstOrDefault(a => a.LanguageID == LangID).Name }).ToList();
            ViewBag.Department = db.Departments.Select(x => new DepartmentDto { DepartmentID = x.DepartmentID, Name = x.DepartmentLangs.FirstOrDefault(a => a.LanguageID == LangID).Name }).ToList();
            ViewBag.RoleID = db.Roles.ToList();
            ViewBag.Location = db.Locations.Select(x => new LocationDto { Number = x.Number, Content = x.LocationLangs.FirstOrDefault(a => a.LanguageID == LangID).Name }).ToList();
            return View();
        }

        public JsonResult GetbyDepartment(int departmentID)
        {
            //var LangID = Session[UserVM.CurrentCulture].ToString();
            //var teamName = db.Teams.Where(x => x.TeamID == teamName).ToList();
            //var teamID = db.Departments.Where(x => x.LanguageID == LangID).ToList();
            var LangID = Session[UserVM.CurrentCulture].ToString();

            //cau dieu kien lay team theo department
            var listdepartment = db.Teams.Where(x => x.DepartmentID == departmentID).Select(
                x => new TeamDto
                {
                    ID = x.ID,
                    DepartmentID = x.DepartmentID,
                    Name = x.TeamLangs.FirstOrDefault(a => a.LanguageID == LangID).Name
                }).ToList();

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
        public ActionResult Create(UserAccount UserAccounts)
        {
            var user = (DemoDoan.ViewModel.UserVM)Session["ACCOUNT"];
            UserAccounts.Password = UserAccounts.Password.SHA256Hash();
           
                if (user.RoleCode == Commons.ROLE.ADM.ToString())
                {
                    db.UserAccounts.Add(UserAccounts);
                    db.SaveChanges();

                }
                if (user.RoleCode == Commons.ROLE.DEPTHEAD.ToString())
                {
                    UserAccounts.DepartmentID = user.DepartmentID;
                    UserAccounts.TeamID = UserAccounts.TeamID;
                    db.UserAccounts.Add(UserAccounts);
                    db.SaveChanges();
                }
                if (user.RoleCode == Commons.ROLE.SUP.ToString())
                {
                    UserAccounts.DepartmentID = user.DepartmentID;
                    UserAccounts.TeamID = user.TeamID;
                    db.UserAccounts.Add(UserAccounts);
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            //}
            //catch (Exception)
            //{

            //    return View(UserAccounts);
            //}
        }

        // GET: UserAccounts/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount UserAccounts = db.UserAccounts.Find(id);

            var model = MappingConfig.Mapper.Map<UserAccount, UserAccountDto>(UserAccounts);
            if (UserAccounts == null)
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
            ViewBag.Role = db.Roles.ToList();

            return View(model);
        }

        // POST: UserAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(UserAccountDto UserAccounts)
        {
            if (ModelState.IsValid)
            {
                var item = db.UserAccounts.Find(UserAccounts.UserID);
                item.Email = UserAccounts.Email;
                item.RoleID = UserAccounts.RoleID;
                item.Status = UserAccounts.Status;
                item.DepartmentID = UserAccounts.DepartmentID;
                item.TeamID = UserAccounts.TeamID;
                item.IDcardNumber = UserAccounts.IDcardNumber;

                //db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(UserAccounts);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            bool status;
            UserAccount UserAccounts = db.UserAccounts.Find(id);
            db.UserAccounts.Remove(UserAccounts);
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
                url = "/UserAccounts"
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
