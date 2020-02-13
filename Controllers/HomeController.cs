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

        // GET: Home  
        public ActionResult Index()
        {
            return View();
        }


    }
}