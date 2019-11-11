using DemoDoan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoDoan.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        OurDbContext db = new OurDbContext();
        // GET: Home  
        public ActionResult Index()
        {
            return View();
        }


    }
}