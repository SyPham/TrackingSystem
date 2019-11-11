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
using PagedList;

namespace DemoDoan.Controllers
{
    public class ColorCodesController : BaseController
    {
        private OurDbContext db = new OurDbContext();

        // GET: ColorCodes
        public async Task<ActionResult> Index(int page =1)
        {
            return View((await db.ColorCodes.ToListAsync()).ToPagedList(page, 10));
        }

        // GET: ColorCodes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColorCode colorCode = await db.ColorCodes.FindAsync(id);
            if (colorCode == null)
            {
                return HttpNotFound();
            }
            return View(colorCode);
        }

        // GET: ColorCodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ColorCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Code,Name")] ColorCode colorCode)
        {
            if (ModelState.IsValid)
            {
                db.ColorCodes.Add(colorCode);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(colorCode);
        }

        // GET: ColorCodes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ColorCode colorCode = await db.ColorCodes.FindAsync(id);
            if (colorCode == null)
            {
                return HttpNotFound();
            }
            return View(colorCode);
        }

        // POST: ColorCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Code,Name")] ColorCode colorCode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(colorCode).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(colorCode);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            bool status;
            ColorCode colorCode = await db.ColorCodes.FindAsync(id);
            db.ColorCodes.Remove(colorCode);
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
                url = "/ColorCodes"
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
