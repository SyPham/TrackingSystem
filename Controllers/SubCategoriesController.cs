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
    public class SubCategoriesController : BaseController
    {
        private OurDbContext db = new OurDbContext();

        // GET: SubSubCategories
        public ActionResult Index(int page = 1)
        {
            var model = db.SubCategories.Select(x => new SubcategoryVM
            {
                SubCategoryName = x.Name,
                CategoryName = x.Name,
                SubCategoryID = x.SubCategoryID,
                CreateTime = x.CreateTime
            }).OrderBy(x=>x.CreateTime).ToList().ToPagedList(page, 10);
          
            return View(model);
        }


        // GET: SubCategories/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.Categories =await db.Categories.ToListAsync();
            return View();
        }

        // POST: SubCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SubCategory subCategory, string nameVI, string nameEN, string NameTW)
        {
            try
            {
                subCategory.Name = nameVI + " - " + nameEN + " - " + NameTW; ;
                db.SubCategories.Add(subCategory);
                await db.SaveChangesAsync();
                SubCategoryLang vn = new SubCategoryLang();
                vn.Name = nameVI;
                vn.LanguageID = "vi";
                vn.SubCategoryID = subCategory.SubCategoryID;
                db.SubCategoryLangs.Add(vn);

                SubCategoryLang en = new SubCategoryLang();
                en.Name = nameEN;
                en.LanguageID = "en";
                en.SubCategoryID = subCategory.SubCategoryID;
                db.SubCategoryLangs.Add(en);

                SubCategoryLang tw = new SubCategoryLang();
                tw.Name = NameTW;
                tw.LanguageID = "en";
                tw.SubCategoryID = subCategory.SubCategoryID;
                db.SubCategoryLangs.Add(tw);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return View(subCategory);
            }
        }

        public async Task<ActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var subCategory = await db.SubCategories.FindAsync(id);
            var itemVi = await db.SubCategoryLangs.FirstOrDefaultAsync(x => x.SubCategoryID == id && x.LanguageID == "vi");
            var itemEn = await db.SubCategoryLangs.FirstOrDefaultAsync(x => x.SubCategoryID == id && x.LanguageID == "en");
            var itemTw = await db.SubCategoryLangs.FirstOrDefaultAsync(x => x.SubCategoryID == id && x.LanguageID == "tw");
            if (itemVi == null) ViewBag.VI = subCategory.Name; else ViewBag.VI = itemVi.Name;
            if (itemEn == null) ViewBag.EN = subCategory.Name; else ViewBag.EN = itemEn.Name;
            if (itemTw == null) ViewBag.TW = subCategory.Name; else ViewBag.TW = itemTw.Name;
            if (subCategory == null)
            {
                return HttpNotFound();
            }

            var user = (DemoDoan.ViewModel.UserVM)Session["ACCOUNT"];
            //query lay tat ca record theo ngon ngu hien ma user chon

            var LangID = Session[UserVM.CurrentCulture].ToString();
            ViewBag.Languages = db.Language.ToList();
            return View(subCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SubCategory subCategory, string nameVI, string nameEN, string nameTW)
        {
            try
            {
                var id = subCategory.SubCategoryID;
                var itemOri = await db.SubCategories.FindAsync(id);
                var itemVi = await db.SubCategoryLangs.FirstOrDefaultAsync(x => x.SubCategoryID == id && x.LanguageID == "vi");
                var itemEn = await db.SubCategoryLangs.FirstOrDefaultAsync(x => x.SubCategoryID == id && x.LanguageID == "en");
                var itemTw = await db.SubCategoryLangs.FirstOrDefaultAsync(x => x.SubCategoryID == id && x.LanguageID == "tw");

                await db.SaveChangesAsync();
                if (itemVi == null)
                {
                    SubCategoryLang vn = new SubCategoryLang();
                    vn.Name = nameVI;
                    vn.LanguageID = "vi";
                    vn.SubCategoryID = subCategory.SubCategoryID;
                    db.SubCategoryLangs.Add(vn);
                }
                else
                {
                    itemVi.Name = nameVI;
                }
                if (itemTw == null)
                {
                    SubCategoryLang tw = new SubCategoryLang();
                    tw.Name = nameTW;
                    tw.LanguageID = "tw";
                    tw.SubCategoryID = subCategory.SubCategoryID;
                    db.SubCategoryLangs.Add(tw);
                }
                else
                {
                    itemTw.Name = nameTW;
                }
                if (itemEn == null)
                {
                    SubCategoryLang en = new SubCategoryLang();
                    en.Name = nameEN;
                    en.LanguageID = "en";
                    en.SubCategoryID = subCategory.SubCategoryID;
                    db.SubCategoryLangs.Add(en);
                }
                else
                {
                    itemEn.Name = nameEN;
                }
                itemOri.CategoryID = subCategory.CategoryID;
                itemOri.Name = nameVI + " - " + nameEN + " - " + nameTW;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return View(subCategory);

            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            bool status;
            var subSubCategory = db.SubCategories.Find(id);
            db.SubCategories.Remove(subSubCategory);
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
                url = "/SubSubCategories"
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
