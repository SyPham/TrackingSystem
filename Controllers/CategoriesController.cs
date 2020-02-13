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
    public class CategoriesController : BaseController
    {
        private OurDbContext db = new OurDbContext();

        // GET: Categories
        public ActionResult Index(int page = 1)
        {
            return View(db.Categories.OrderBy(x => x.LanguageID).ToPagedList(page, 10));
        }


        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category category, string nameVI, string nameEN, string NameTW)
        {
            try
            {
                category.Name = nameVI + " - " + nameEN + " - " + NameTW; ;
                db.Categories.Add(category);
                await db.SaveChangesAsync();
                CategoryLang vn = new CategoryLang();
                vn.Name = nameVI;
                vn.LanguageID = "vi";
                vn.CategoryID = category.CategoryID;
                db.CategoryLangs.Add(vn);

                CategoryLang en = new CategoryLang();
                en.Name = nameEN;
                en.LanguageID = "en";
                en.CategoryID = category.CategoryID;
                db.CategoryLangs.Add(en);

                CategoryLang tw = new CategoryLang();
                tw.Name = NameTW;
                tw.LanguageID = "en";
                tw.CategoryID = category.CategoryID;
                db.CategoryLangs.Add(tw);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return View(category);
            }
        }

        public async Task<ActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = await db.Categories.FindAsync(id);
            var itemVi = await db.CategoryLangs.FirstOrDefaultAsync(x => x.CategoryID == id && x.LanguageID == "vi");
            var itemEn = await db.CategoryLangs.FirstOrDefaultAsync(x => x.CategoryID == id && x.LanguageID == "en");
            var itemTw = await db.CategoryLangs.FirstOrDefaultAsync(x => x.CategoryID == id && x.LanguageID == "tw");
            if (itemVi == null) ViewBag.VI = category.Name; else ViewBag.VI = itemVi.Name;
            if (itemEn == null) ViewBag.EN = category.Name; else ViewBag.EN = itemEn.Name;
            if (itemTw == null) ViewBag.TW = category.Name; else ViewBag.TW = itemTw.Name;
            if (category == null)
            {
                return HttpNotFound();
            }

            var user = (DemoDoan.ViewModel.UserVM)Session["ACCOUNT"];
            //query lay tat ca record theo ngon ngu hien ma user chon

            var LangID = Session[UserVM.CurrentCulture].ToString();
            ViewBag.Languages = db.Language.ToList();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Category category, string nameVI, string nameEN, string nameTW)
        {
            try
            {
                var id = category.CategoryID;
                var itemOri = await db.Categories.FindAsync(id);
                var itemVi = await db.CategoryLangs.FirstOrDefaultAsync(x => x.CategoryID == id && x.LanguageID == "vi");
                var itemEn = await db.CategoryLangs.FirstOrDefaultAsync(x => x.CategoryID == id && x.LanguageID == "en");
                var itemTw = await db.CategoryLangs.FirstOrDefaultAsync(x => x.CategoryID == id && x.LanguageID == "tw");

                await db.SaveChangesAsync();
                if (itemVi == null)
                {
                    CategoryLang vn = new CategoryLang();
                    vn.Name = nameVI;
                    vn.LanguageID = "vi";
                    vn.CategoryID = category.CategoryID;
                    db.CategoryLangs.Add(vn);
                }
                else
                {
                    itemVi.Name = nameVI;
                }
                if (itemTw == null)
                {
                    CategoryLang tw = new CategoryLang();
                    tw.Name = nameTW;
                    tw.LanguageID = "tw";
                    tw.CategoryID = category.CategoryID;
                    db.CategoryLangs.Add(tw);
                }
                else
                {
                    itemTw.Name = nameTW;
                }
                if (itemEn == null)
                {
                    CategoryLang en = new CategoryLang();
                    en.Name = nameEN;
                    en.LanguageID = "en";
                    en.CategoryID = category.CategoryID;
                    db.CategoryLangs.Add(en);
                }
                else
                {
                    itemEn.Name = nameEN;
                }
                itemOri.Name = nameVI + " - " + nameEN + " - " + nameTW;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return View(category);

            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            bool status;
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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
                url = "/Categories"
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
