using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DemoDoan.Models;
using DemoDoan.ViewModel;
using PagedList;

namespace DemoDoan.Controllers
{
    public class DepartmentsController : BaseController
    {
        private OurDbContext db;

        public DepartmentsController()
        {
            db = new OurDbContext();
        }

        // GET: Departments
        public async Task<ActionResult> Index(int page = 1)
        {
            return View((await db.Departments.OrderBy(x=>x.CreateTime).Include(x=>x.DepartmentLangs).ToListAsync()).ToPagedList(page, 10));
        }


        // GET: Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Department department, string nameVI, string nameEN, string NameTW)
        {
            try
            {
                department.Name = nameVI + " - " + nameEN + " - " +NameTW;
                db.Departments.Add(department);
                await db.SaveChangesAsync();
                DepartmentLang vn = new DepartmentLang();
                vn.Name = nameVI;
                vn.LanguageID = "vi";
                vn.DepartmentID = department.DepartmentID;
                db.DepartmentLangs.Add(vn);

                DepartmentLang en = new DepartmentLang();
                en.Name = nameEN;
                en.LanguageID = "en";
                en.DepartmentID = department.DepartmentID;
                db.DepartmentLangs.Add(en);

                DepartmentLang tw = new DepartmentLang();
                tw.Name = NameTW;
                tw.LanguageID = "en";
                tw.DepartmentID = department.DepartmentID;
                db.DepartmentLangs.Add(tw);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return View(department);
            }
        }

        // GET: Departments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            var itemVi =await db.DepartmentLangs.FirstOrDefaultAsync(x => x.DepartmentID == id && x.LanguageID == "vi");
            var itemEn =await db.DepartmentLangs.FirstOrDefaultAsync(x => x.DepartmentID == id && x.LanguageID == "en");
            var itemTw =await db.DepartmentLangs.FirstOrDefaultAsync(x => x.DepartmentID == id && x.LanguageID == "tw");
            if (itemVi == null)ViewBag.VI = department.Name;else ViewBag.VI = itemVi.Name;
            if (itemEn == null) ViewBag.EN = department.Name; else ViewBag.EN = itemEn.Name;
            if (itemTw == null) ViewBag.TW = department.Name; else ViewBag.TW = itemTw.Name;
            if (department == null)
            {
                return HttpNotFound();
            }

            var user = (DemoDoan.ViewModel.UserVM)Session["ACCOUNT"];
            //query lay tat ca record theo ngon ngu hien ma user chon

            var LangID = Session[UserVM.CurrentCulture].ToString();
            ViewBag.Languages = db.Language.ToList();
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Department department,string nameVI, string nameEN, string nameTW)
        {
            try
            {
                var id = department.DepartmentID;
                Department itemDepartment = await db.Departments.FindAsync(id);
                var itemVi = await db.DepartmentLangs.FirstOrDefaultAsync(x => x.DepartmentID == id && x.LanguageID == "vi");
                var itemEn = await db.DepartmentLangs.FirstOrDefaultAsync(x => x.DepartmentID == id && x.LanguageID == "en");
                var itemTw = await db.DepartmentLangs.FirstOrDefaultAsync(x => x.DepartmentID == id && x.LanguageID == "tw");

                await db.SaveChangesAsync();
                if (itemVi == null)
                {
                    DepartmentLang vn = new DepartmentLang();
                    vn.Name = nameVI;
                    vn.LanguageID = "vi";
                    vn.DepartmentID = department.DepartmentID;
                    db.DepartmentLangs.Add(vn);
                }
                else
                {
                    itemVi.Name = nameVI;
                }
                if (itemTw == null)
                {
                    DepartmentLang tw = new DepartmentLang();
                    tw.Name = nameTW;
                    tw.LanguageID = "tw";
                    tw.DepartmentID = department.DepartmentID;
                    db.DepartmentLangs.Add(tw);
                }
                else
                {
                    itemTw.Name = nameTW;
                }
                if (itemEn == null)
                {
                    DepartmentLang en = new DepartmentLang();
                    en.Name = nameEN;
                    en.LanguageID = "en";
                    en.DepartmentID = department.DepartmentID;
                    db.DepartmentLangs.Add(en);
                }
                else
                {
                    itemEn.Name = nameEN;
                }
                itemDepartment.Name = nameVI + " - " + nameEN + " - " + nameTW;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return View(department);

            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            bool status;
            Department department = await db.Departments.FindAsync(id);
            db.Departments.Remove(department);
            try
            {
                await db.SaveChangesAsync();
                status = true;
            }
            catch
            {
                status = false;
            }
            return Json(new
            {
                status,
                url = "/Departments"
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
