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
    public class StatusController : BaseController
    {
        private OurDbContext db = new OurDbContext();

        // GET: Status
        public ActionResult Index(int page = 1)
        {
            return View(db.Status.OrderBy(x=>x.StatusID).ToPagedList(page, 10));
        }
        public ActionResult BackToRecord()
        {
            return Redirect("/Records/Index");
        }


        // GET: Status/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Status status, string nameVI, string nameEN, string NameTW)
        {
            try
            {
                status.Name = nameVI + " - " + nameEN + " - " + NameTW; ;
                db.Status.Add(status);
                await db.SaveChangesAsync();
                StatusLang vn = new StatusLang();
                vn.Name = nameVI;
                vn.LanguageID = "vi";
                vn.StatusID = status.StatusID;
                db.StatusLangs.Add(vn);

                StatusLang en = new StatusLang();
                en.Name = nameEN;
                en.LanguageID = "en";
                en.StatusID = status.StatusID;
                db.StatusLangs.Add(en);

                StatusLang tw = new StatusLang();
                tw.Name = NameTW;
                tw.LanguageID = "en";
                tw.StatusID = status.StatusID;
                db.StatusLangs.Add(tw);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return View(status);
            }
        }

        public async Task<ActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var status = await db.Status.FindAsync(id);
            var itemVi = await db.StatusLangs.FirstOrDefaultAsync(x => x.StatusID == id && x.LanguageID == "vi");
            var itemEn = await db.StatusLangs.FirstOrDefaultAsync(x => x.StatusID == id && x.LanguageID == "en");
            var itemTw = await db.StatusLangs.FirstOrDefaultAsync(x => x.StatusID == id && x.LanguageID == "tw");
            if (itemVi == null) ViewBag.VI = status.Name; else ViewBag.VI = itemVi.Name;
            if (itemEn == null) ViewBag.EN = status.Name; else ViewBag.EN = itemEn.Name;
            if (itemTw == null) ViewBag.TW = status.Name; else ViewBag.TW = itemTw.Name;
            if (status == null)
            {
                return HttpNotFound();
            }

            var user = (DemoDoan.ViewModel.UserVM)Session["ACCOUNT"];
            //query lay tat ca record theo ngon ngu hien ma user chon

            var LangID = Session[UserVM.CurrentCulture].ToString();
            ViewBag.Languages = db.Language.ToList();
            return View(status);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Status status, string nameVI, string nameEN, string nameTW)
        {
            try
            {
                var id = status.StatusID;
                var itemOri = await db.Status.FindAsync(id);
                var itemVi = await db.StatusLangs.FirstOrDefaultAsync(x => x.StatusID == id && x.LanguageID == "vi");
                var itemEn = await db.StatusLangs.FirstOrDefaultAsync(x => x.StatusID == id && x.LanguageID == "en");
                var itemTw = await db.StatusLangs.FirstOrDefaultAsync(x => x.StatusID == id && x.LanguageID == "tw");

                await db.SaveChangesAsync();
                if (itemVi == null)
                {
                    StatusLang vn = new StatusLang();
                    vn.Name = nameVI;
                    vn.LanguageID = "vi";
                    vn.StatusID = status.StatusID;
                    db.StatusLangs.Add(vn);
                }
                else
                {
                    itemVi.Name = nameVI;
                }
                if (itemTw == null)
                {
                    StatusLang tw = new StatusLang();
                    tw.Name = nameTW;
                    tw.LanguageID = "tw";
                    tw.StatusID = status.StatusID;
                    db.StatusLangs.Add(tw);
                }
                else
                {
                    itemTw.Name = nameTW;
                }
                if (itemEn == null)
                {
                    StatusLang en = new StatusLang();
                    en.Name = nameEN;
                    en.LanguageID = "en";
                    en.StatusID = status.StatusID;
                    db.StatusLangs.Add(en);
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
                return View(status);

            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            bool status;
            Status item = db.Status.Find(id);
            db.Status.Remove(item);
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
                url = "/Status"
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
