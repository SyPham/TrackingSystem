using DemoDoan.Models;
using DemoDoan.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DemoDoan.Controllers
{
    public class BaseController: Controller
    {
        private OurDbContext db = new OurDbContext();
        //inilizing culture on controller initialization
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if(Session[UserVM.CurrentCulture] != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session[UserVM.CurrentCulture].ToString());
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session[UserVM.CurrentCulture].ToString());
            }
            else
            {
                Session[UserVM.CurrentCulture] = "en";
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");

            }
        }

        //changing culture
        public ActionResult ChangeCulture(string ddCulture, string returnUrl )
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(ddCulture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(ddCulture);

            Session[UserVM.CurrentCulture] = ddCulture;

            //update languageID trong bang user
            var sss = (DemoDoan.ViewModel.UserVM)Session["ACCOUNT"];
            var item = db.userAccount.Find(sss.UserID);
            item.LanguageID = ddCulture;
            ////Luu db
            db.SaveChanges();
            //update lai session Language
            sss.LanguageID = ddCulture;


            return Redirect(returnUrl);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userprofile = Session["ACCOUNT"] as UserVM;
            //var username = Session["UserName"].ToSafetyString();
            if (userprofile == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}