using DemoDoan.Models;
using DemoDoan.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoDoan.Controllers
{
    public class LoginController : Controller
    {

        OurDbContext _dbContext = null;
        public LoginController()
        {
            this._dbContext = new OurDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }
        // GET: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string username,string password)
        {
            if (ModelState.IsValid)
            {
                //Kiem tra trong db co chua
                var result = _dbContext.userAccount.SingleOrDefault(x => x.Username == username && x.Password == password);
                var login = new UserVM();
                login.RoleID = result.RoleID;
                login.UserID = result.UserID;
                login.Username = result.Username;
                login.DepartmentID = result.DepartmentID;
                login.TeamID = result.TeamID;
                login.LocationID = result.LocationID;
                login.LanguageID = result.LanguageID;
                Session["ACCOUNT"] = login;
                Session.Timeout = 525600;
                //Neu chua co thong bao loi
                if (result == null)
                {
                    return Redirect("/");
                }
                else
                {
                    if (result.RoleID == 1)
                    {
                        //Neu la admin thi chuyen den trang admin
                        return RedirectToAction("Index", "Records");
                    }
                    else
                    {
                        //nguoc lai la user thi chuyen den trang user
                        return RedirectToAction("Index", "Records");
                    }
                }
            }
            return View();
            
        }


        public ActionResult Logout()
        {
            Session.Abandon(); //xoa session va logout
            return RedirectToAction("Index", "Login");

        }
    }
}
