using DemoDoan.helpers;
using DemoDoan.Models;
using DemoDoan.ViewModel;
using KPI.Model.helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
        public ActionResult Index(string username, string password)
        {
            if (ModelState.IsValid)
            {
                password = password.SHA256Hash();
                //Kiem tra trong db co chua
                var result = _dbContext.UserAccounts
                            .FirstOrDefault(x => x.Username == username && x.Password == password);
                if (result == null)
                    return View();
                var login = new UserVM();
                login.RoleID = result.RoleID;
                login.UserID = result.UserID;
                login.Username = result.Username;
                login.Alias = result.Username.ToTitleCase();

                login.DepartmentID = result.DepartmentID;
                login.TeamID = result.TeamID;
                login.RoleCode = _dbContext.Roles.Find(result.RoleID).Code;
                login.LocationID = result.LocationID;
                login.LanguageID = result.LanguageID;
                login.Menus = result.Role.Menus;
                Session["ACCOUNT"] = login;
                Session.Timeout = 525600;
                //Neu chua co thong bao loi
                if (result == null)
                {
                    return Redirect("/");
                }
                else
                {
                    if (login.RoleCode == Commons.ROLE.USER.ToSafetyString())
                    {
                        //Neu la admin thi chuyen den trang admin
                        return RedirectToAction("Index", "Records");
                    }
                    else
                    {
                        //nguoc lai la user thi chuyen den trang user
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View();

        }

        public JsonResult ForGotPassword(string email, string idcard)
        {
            try
            {

                if (!email.IsEmailFormat())
                {
                    return Json(new
                    {
                        status = false,
                        message = "Email or ID Card Number incorrect!"
                    }, JsonRequestBehavior.AllowGet);
                }
                var item = _dbContext.UserAccounts.FirstOrDefault(x => x.Email == email && x.IDcardNumber == idcard);
                if (item != null)
                {
                    var password = CodeUtility.RandomString(3);
                    string from = ConfigurationManager.AppSettings["FromEmailAddress"].ToSafetyString();
                    string host = ConfigurationManager.AppSettings["Host"].ToSafetyString();

                    string content = "[KPI System] Your password is '<strong style='color:red'>" + password + "</strong>' . Please you login with this password and change the password. Link: " + host;
                    string to = email.ToSafetyString();
                    string subject = "Reset password";
                    item.Password = password.SHA256Hash();
                    _dbContext.SaveChanges();

                    if (Commons.SendMail(from, to, subject, content))
                    {
                        return Json(new
                        {
                            status = true,
                            message = "A new password has already sent to your company email!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            status = false,
                            message = "Send mail failed!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new
                    {
                        message = "Email or ID Card Number incorrect!"
                    }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                return Json(new
                {
                    status = false,
                    message = "Error!"
                }, JsonRequestBehavior.AllowGet);

            }
        }
        public JsonResult ChangePassword(string password, string passwordNew)
        {
            try
            {
                var user = Session["ACCOUNT"] as UserVM;
                password = password.SHA256Hash();
                passwordNew = passwordNew.SHA256Hash();

                var item = _dbContext.UserAccounts.FirstOrDefault(x => x.Username == user.Username && x.Password == password);
                item.Password = passwordNew;
                _dbContext.SaveChanges();
                return Json(new
                {
                    status = true,
                    message = "Change password successfully!"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new
                {
                    status = false,
                    message = "Error!"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon(); //xoa session va logout
            return RedirectToAction("Index", "Login");

        }
    }
}
