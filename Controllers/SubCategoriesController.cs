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
    public class SubCategoriesController : BaseController
    {
        private OurDbContext db = new OurDbContext();

        // GET: SubCategories
        public ActionResult Index(int page = 1)
        {
            var LangID = Session[UserVM.CurrentCulture].ToString();
            var model = db.SubCategories.Select(x => new SubcategoryVM
            {
                CategoryName = db.Categories.FirstOrDefault(a=>a.CategoryID == x.CategoryID).Name,
                LanguageID = x.LanguageID,
                SubCategoryName = x.Name,
                SubCategoryID = x.SubCategoryID,
                Language = db.Language.FirstOrDefault(a => a.LanguageID == x.LanguageID).Name
            }).Where(x => x.LanguageID == LangID).ToList().ToPagedList(page, 10);
          
            return View(model);
        }

        // GET: SubCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            return View(subCategory);
        }

        // GET: SubCategories/Create
        public ActionResult Create()
        {
            var user = (DemoDoan.ViewModel.UserVM)Session["ACCOUNT"];
            //query lay tat ca record theo ngon ngu hien ma user chon

            var LangID = Session[UserVM.CurrentCulture].ToString();
            ViewBag.SubCategories = db.SubCategories.Where(x => x.LanguageID == LangID);
            ViewBag.Categories = db.Categories.Where(x => x.LanguageID == LangID);
            ViewBag.Languages = db.Language;
            return View();
        }

        // POST: SubCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubCategoryID,Name,CreateTime,CategoryID,LanguageID")] SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                subCategory.CreateTime = DateTime.Now;
                db.SubCategories.Add(subCategory);
                subCategory.LanguageID = Session[UserVM.CurrentCulture].ToString();
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(subCategory);
        }

        // GET: SubCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subCategory = db.SubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }
            var LangID = Session[UserVM.CurrentCulture].ToString();
            ViewBag.Languages = db.Language;
            ViewBag.Category = db.Categories.Where(x => x.LanguageID == LangID);
            return View(subCategory);
        }

        // POST: SubCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubCategoryID,Name,CreateTime,CategoryID,LanguageID")] SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                subCategory.CreateTime = DateTime.Now;
                db.Entry(subCategory).State = EntityState.Modified;
                subCategory.LanguageID = Session[UserVM.CurrentCulture].ToString();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(subCategory);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            bool status;
            SubCategory subCategory = db.SubCategories.Find(id);
            db.SubCategories.Remove(subCategory);
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
                url = "/SubCategories"
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
