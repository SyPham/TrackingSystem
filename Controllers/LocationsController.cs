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
    public class LocationsController : BaseController
    {
        private OurDbContext db = new OurDbContext();

        // GET: Locations
        public ActionResult Index(int page = 1)
        {
            ViewBag.Teams = db.Locations;

            return View(db.Locations.OrderBy(x=>x.Number).ToPagedList(page,10));
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

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Location location, string nameVI, string nameEN, string NameTW)
        {
            try
            {
                location.Content = nameVI + " - " + nameEN + " - " + NameTW; ;
                db.Locations.Add(location);
                await db.SaveChangesAsync();
                LocationLang vn = new LocationLang();
                vn.Name = nameVI;
                vn.LanguageID = "vi";
                vn.LocationID = location.Number;
                db.LocationLangs.Add(vn);

                LocationLang en = new LocationLang();
                en.Name = nameEN;
                en.LanguageID = "en";
                en.LocationID = location.Number;
                db.LocationLangs.Add(en);

                LocationLang tw = new LocationLang();
                tw.Name = NameTW;
                tw.LanguageID = "en";
                tw.LocationID = location.Number;
                db.LocationLangs.Add(tw);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return View(location);
            }
        }

        public async Task<ActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var location = await db.Locations.FindAsync(id);
            var itemVi = await db.LocationLangs.FirstOrDefaultAsync(x => x.LocationID == id && x.LanguageID == "vi");
            var itemEn = await db.LocationLangs.FirstOrDefaultAsync(x => x.LocationID == id && x.LanguageID == "en");
            var itemTw = await db.LocationLangs.FirstOrDefaultAsync(x => x.LocationID == id && x.LanguageID == "tw");
            if (itemVi == null) ViewBag.VI = location.Content; else ViewBag.VI = itemVi.Name;
            if (itemEn == null) ViewBag.EN = location.Content; else ViewBag.EN = itemEn.Name;
            if (itemTw == null) ViewBag.TW = location.Content; else ViewBag.TW = itemTw.Name;
            if (location == null)
            {
                return HttpNotFound();
            }

            var user = (DemoDoan.ViewModel.UserVM)Session["ACCOUNT"];
            //query lay tat ca record theo ngon ngu hien ma user chon

            var LangID = Session[UserVM.CurrentCulture].ToString();
            ViewBag.Languages = db.Language.ToList();
            return View(location);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Location location, string nameVI, string nameEN, string nameTW)
        {
            try
            {
                var id = location.Number;
                var itemOri = await db.Locations.FindAsync(id);
                var itemVi = await db.LocationLangs.FirstOrDefaultAsync(x => x.LocationID == id && x.LanguageID == "vi");
                var itemEn = await db.LocationLangs.FirstOrDefaultAsync(x => x.LocationID == id && x.LanguageID == "en");
                var itemTw = await db.LocationLangs.FirstOrDefaultAsync(x => x.LocationID == id && x.LanguageID == "tw");

                await db.SaveChangesAsync();
                if (itemVi == null)
                {
                    LocationLang vn = new LocationLang();
                    vn.Name = nameVI;
                    vn.LanguageID = "vi";
                    vn.LocationID = location.Number;
                    db.LocationLangs.Add(vn);
                }
                else
                {
                    itemVi.Name = nameVI;
                }
                if (itemTw == null)
                {
                    LocationLang tw = new LocationLang();
                    tw.Name = nameTW;
                    tw.LanguageID = "tw";
                    tw.LocationID = location.Number;
                    db.LocationLangs.Add(tw);
                }
                else
                {
                    itemTw.Name = nameTW;
                }
                if (itemEn == null)
                {
                    LocationLang en = new LocationLang();
                    en.Name = nameEN;
                    en.LanguageID = "en";
                    en.LocationID = location.Number;
                    db.LocationLangs.Add(en);
                }
                else
                {
                    itemEn.Name = nameEN;
                }
                itemOri.Content = nameVI + " - " + nameEN + " - " + nameTW;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return View(location);

            }
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
