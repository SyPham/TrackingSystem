using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DemoDoan.Dao;
using DemoDoan.helpers;
using DemoDoan.Models;
using DemoDoan.ViewModel;
using Newtonsoft.Json;
namespace DemoDoan.Controllers
{
    public class RecordsController : BaseController
    {
        private OurDbContext db = new OurDbContext();

        public ActionResult Index()
        {
            //var LangID = Session[UserVM.CurrentCulture].ToString();
            //var model = db.Records.Where(x => x.LanguageID == LangID).ToList();
            //return View(model);

            return View(db.ColorCodes.ToList());
        }

        [HttpGet]
        public JsonResult LoadData(string search,int colorCodeID, int page, int pageSize, string sort, string type) //them sort, type
        {
            var listTeamID = new List<int>();
            //Sesssion
            var user = (DemoDoan.ViewModel.UserVM)Session["ACCOUNT"];
            //query lay tat ca record theo ngon ngu hien ma user chon

            var LangID = Session[UserVM.CurrentCulture].ToString();
            var listStatus =  db.Status.Select(x => new { x.StatusID, x.StatusLangs.FirstOrDefault(a => a.LanguageID == LangID).Name }).ToList();
            var listCategories =  db.Categories.Select(x => new { x.CategoryID, x.CategoryLangs.FirstOrDefault(a => a.LanguageID == LangID).Name }).ToList();
            var listSubCategories =  db.SubCategories.Select(x => new { x.SubCategoryID, x.SubCategoryLangs.FirstOrDefault(a => a.LanguageID == LangID).Name }).ToList();
            var listLocations =  db.Locations.Select(x => new { x.Number, Content = x.LocationLangs.FirstOrDefault(a => a.LanguageID == LangID).Name }).ToList();

            //
            var records = db.Records;
            var locations = db.Locations;
            var categories = db.Categories;
            var status = db.Status;
            var teamids = db.Teams;
            var departments = db.Departments;
            var subCategories = db.SubCategories;
            var colorCodes = db.ColorCodes.Select(x=> new { x.Name, x.ID }).ToList();
            if (user != null)
            {
                if (user.RoleID == 1)
                {
                    var model = (from record in records
                                 join loc in locations on record.LocationID equals loc.Number into sr
                                 from x in sr.DefaultIfEmpty()
                                 join cate in categories on record.CategoryID equals cate.CategoryID
                                 join sta in status on record.StatusID equals sta.StatusID into aa
                                 from x2 in aa.DefaultIfEmpty()
                                 join tea in teamids on record.TeamID equals tea.ID into bb
                                 from x3 in bb.DefaultIfEmpty()
                                 join dep in departments on record.DepartmentID equals dep.DepartmentID into ab
                                 from x4 in ab.DefaultIfEmpty()
                                 join sub in subCategories on record.SubCategoryID equals sub.SubCategoryID into scate
                                 from x5 in scate.DefaultIfEmpty()
                                 join usr in db.UserAccounts on record.UserID equals usr.UserID
                                 join color in db.ColorCodes on record.ColorCodeID equals color.ID
                                 select new LoadDataRecordVM
                                 {
                                     StatusID = x2.StatusID,
                                     UserID = record.UserID,//
                                     RecordID = record.RecordID,//
                                     TrackingID = record.TrackingID,//
                                     SubCategoryID = record.SubCategoryID,//
                                     Title = record.Title,//
                                     Location = x.Content == null ? "N/A" : x.Content,//
                                     DocumentCategory = cate.Name,//
                                     CreatedDate = record.CreatedDate,//
                                     Modifieddate = record.Modifieddate,//
                                     Description = record.Description,//
                                     StatusName = x2.Name == null ? "Wait for document" : x2.Name,//
                                     TeamName = x3.Name == null ? "N/A" : x3.Name,
                                     Department = x4.Name == null ? "N/A" : x4.Name,//
                                     ApprovalSheetCategory = x5.Name == null ? "N/A" : x5.Name,//
                                     Username = usr.Username,
                                     CategoryID = cate.CategoryID,
                                     LocationID = record.LocationID,
                                     ColorCode = color.Code,
                                     ColorCodeID =record.ColorCodeID
                                     

                                 }).AsEnumerable();
                    var data = model.ToList();
                    int totalRow = model.Count();
                    if (!search.IsNullOrEmpty())
                    {
                        model = model.Where(x => x.TrackingID.Contains(search));
                    }

                    if (colorCodeID > 0)
                    {
                        model = model.Where(x => x.ColorCodeID == colorCodeID);
                    }

                    model = model.OrderByDescending(x => x.CreatedDate)
                      .Skip((page - 1) * pageSize)
                      .Take(pageSize).ToList();

                    if (!sort.IsNullOrEmpty() && !type.IsNullOrEmpty())
                    {
                        //Sort theo createddate
                        if (sort.ToUpper() == "CREATEDDATE" && type.ToUpper() == "DESC")
                            model = model.OrderByDescending(x => x.CreatedDate);
                        else if (sort.ToUpper() == "CREATEDDATE" && type.ToUpper() == "ASC")
                        {
                            model = model.OrderBy(x => x.CreatedDate);
                        }

                        //Sort theo Status
                        else if (sort.ToUpper() == "STATUS" && type.ToUpper() == "DESC")
                            model = model.OrderByDescending(x => x.StatusName);
                        else if (sort.ToUpper() == "STATUS" && type.ToUpper() == "ASC")
                        {
                            model = model.OrderBy(x => x.StatusName);
                        }
                        //Sort theo department
                        else if (sort.ToUpper() == "DEPARTMENT" && type.ToUpper() == "DESC")
                            model = model.OrderByDescending(x => x.DepartmentID);
                        else if (sort.ToUpper() == "DEPARTMENT" && type.ToUpper() == "ASC")
                        {
                            model = model.OrderBy(x => x.DepartmentID);
                        }
                    }

                    return Json(new
                    {
                        status = true,
                        total = totalRow,
                        data = model,
                        listStatus,
                        listCategories,
                        listSubCategories,
                        listLocations,
                        pageSize,
                        page,
                        role = user.RoleID
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (user.DepartmentID > 0)
                    {
                        var model = (from record in records
                                     join loc in locations on record.LocationID equals loc.Number into sr
                                     // 
                                     from x in sr.DefaultIfEmpty()
                                     join cate in categories on record.CategoryID equals cate.CategoryID
                                     join sta in status on record.StatusID equals sta.StatusID into aa
                                     from x2 in aa.DefaultIfEmpty()
                                     join tea in teamids on record.TeamID equals tea.ID into bb
                                     from x3 in bb.DefaultIfEmpty()
                                     join dep in departments on record.DepartmentID equals dep.DepartmentID into ab
                                     from x4 in ab.DefaultIfEmpty()
                                         //Left Join LinQ
                                     join sub in subCategories on record.SubCategoryID equals sub.SubCategoryID into scate
                                     from x5 in scate.DefaultIfEmpty()
                                     join usr in db.UserAccounts on record.UserID equals usr.UserID
                                     where record.DepartmentID == user.DepartmentID || listTeamID.Contains(record.TeamID)
                                     join color in db.ColorCodes on record.ColorCodeID equals color.ID

                                     //buoc nay la buoc bat buoc
                                     select new LoadDataRecordVM
                                     {
                                         StatusID = x2.StatusID,
                                         RecordID = record.RecordID,
                                         UserID = record.UserID,//
                                         TrackingID = record.TrackingID,//
                                         SubCategoryID = record.SubCategoryID,//
                                         Title = record.Title,//
                                         Location = x.Content == null ? "N/A" : x.Content,//
                                         DocumentCategory = cate.Name,//
                                         CreatedDate = record.CreatedDate,//
                                         Modifieddate = record.Modifieddate,//
                                         Description = record.Description,//
                                         StatusName = x2.Name == null ? "Wait for document" : x2.Name,//
                                         TeamName = x3.Name == null ? "N/A" : x3.Name,
                                         Department = x4.Name == null ? "N/A" : x4.Name,//
                                         ApprovalSheetCategory = x5.Name == null ? "N/A" : x5.Name,//
                                         Username = usr.Username,
                                         CategoryID = cate.CategoryID,
                                         LocationID = record.LocationID,
                                         ColorCode=color.Code,
                                         ColorCodeID = record.ColorCodeID
                                     }).AsEnumerable();

                        var data = model.ToList();

                        if (!search.IsNullOrEmpty())
                        {
                            model = model.Where(x => x.TrackingID.Contains(search));
                        }
                        if (colorCodeID > 0)
                        {
                            model = model.Where(x => x.ColorCodeID == colorCodeID);
                        }
                        int totalRow = model.Count();

                        model = model.OrderByDescending(x => x.CreatedDate)
                          .Skip((page - 1) * pageSize)
                          .Take(pageSize).ToList();

                        if (!sort.IsNullOrEmpty() && !type.IsNullOrEmpty())
                        {
                            //Sort theo createddate
                            if (sort.ToUpper() == "CREATEDDATE" && type.ToUpper() == "DESC")
                                model = model.OrderByDescending(x => x.CreatedDate);
                            else if (sort.ToUpper() == "CREATEDDATE" && type.ToUpper() == "ASC")
                            {
                                model = model.OrderBy(x => x.CreatedDate);
                            }

                            //Sort theo Status
                            else if (sort.ToUpper() == "STATUS" && type.ToUpper() == "DESC")
                                model = model.OrderByDescending(x => x.StatusName);
                            else if (sort.ToUpper() == "STATUS" && type.ToUpper() == "ASC")
                            {
                                model = model.OrderBy(x => x.StatusName);
                            }
                            //Sort theo department
                            else if (sort.ToUpper() == "DEPARTMENT" && type.ToUpper() == "DESC")
                                model = model.OrderByDescending(x => x.DepartmentID);
                            else if (sort.ToUpper() == "DEPARTMENT" && type.ToUpper() == "ASC")
                            {
                                model = model.OrderBy(x => x.DepartmentID);
                            }
                        }

                        return Json(new
                        {
                            status = true,
                            total = totalRow,
                            data = model,
                            listStatus,
                            listCategories,
                            listSubCategories,
                            listLocations,
                            pageSize,
                            page,
                            role = user.RoleID
                        }, JsonRequestBehavior.AllowGet);
                    }

                    if (user.TeamID > 0)
                    {
                        //Kiem tra user dang dang nhap thuoc department nao
                        var departmentID = db.Teams.FirstOrDefault(x => x.ID == user.TeamID).DepartmentID;
                        //Lay ra danh sach team thuoc cung department tren
                        var teams = db.Teams.Where(x => x.DepartmentID == departmentID).ToList();

                        //Lay tat ca nhung ID cua team thuoc cung department them vao danh sach vua tao o tren (var listTeamID = new List<int>();) da khai bao o tren roi
                        foreach (var item in teams)
                        {
                            listTeamID.Add(item.ID);
                        }
                        var model = (from record in records
                                     join loc in locations on record.LocationID equals loc.Number into sr
                                     // 
                                     from x in sr.DefaultIfEmpty()
                                     join cate in categories on record.CategoryID equals cate.CategoryID
                                     join sta in status on record.StatusID equals sta.StatusID into aa
                                     from x2 in aa.DefaultIfEmpty()
                                     join tea in teamids on record.TeamID equals tea.ID into bb
                                     from x3 in bb.DefaultIfEmpty()
                                     join dep in departments on record.DepartmentID equals dep.DepartmentID into ab
                                     from x4 in ab.DefaultIfEmpty()
                                         //Left Join LinQ
                                     join sub in subCategories on record.SubCategoryID equals sub.SubCategoryID into scate
                                     from x5 in scate.DefaultIfEmpty()
                                     join usr in db.UserAccounts on record.UserID equals usr.UserID
                                     where listTeamID.Contains(record.TeamID)
                                     join color in db.ColorCodes on record.ColorCodeID equals color.ID

                                     //buoc nay la buoc bat buoc
                                     select new LoadDataRecordVM
                                     {
                                         StatusID = x2.StatusID,
                                         RecordID = record.RecordID,
                                         UserID = record.UserID,//
                                         TrackingID = record.TrackingID,//
                                         SubCategoryID = record.SubCategoryID,//
                                         Title = record.Title,//
                                         Location = x.Content == null ? "N/A" : x.Content,//
                                         DocumentCategory = cate.Name,//
                                         CreatedDate = record.CreatedDate,//
                                         Modifieddate = record.Modifieddate,//
                                         Description = record.Description,//
                                         StatusName = x2.Name == null ? "Wait for document" : x2.Name,//
                                         TeamName = x3.Name == null ? "N/A" : x3.Name,
                                         Department = x4.Name == null ? "N/A" : x4.Name,//
                                         ApprovalSheetCategory = x5.Name == null ? "N/A" : x5.Name,//
                                         Username = usr.Username,
                                         CategoryID = cate.CategoryID,
                                         LocationID = record.LocationID,
                                         ColorCode=color.Code,
                                         ColorCodeID = record.ColorCodeID
                                     }).AsEnumerable();

                        var data = model.ToList();

                        if (!search.IsNullOrEmpty())
                        {
                            model = model.Where(x => x.TrackingID.Contains(search));
                        }
                        if (colorCodeID > 0)
                        {
                            model = model.Where(x => x.ColorCodeID == colorCodeID);
                        }
                        int totalRow = model.Count();

                        model = model.OrderByDescending(x => x.CreatedDate)
                          .Skip((page - 1) * pageSize)
                          .Take(pageSize).ToList();

                        if (!sort.IsNullOrEmpty() && !type.IsNullOrEmpty())
                        {
                            //Sort theo createddate
                            if (sort.ToUpper() == "CREATEDDATE" && type.ToUpper() == "DESC")
                                model = model.OrderByDescending(x => x.CreatedDate);
                            else if (sort.ToUpper() == "CREATEDDATE" && type.ToUpper() == "ASC")
                            {
                                model = model.OrderBy(x => x.CreatedDate);
                            }

                            //Sort theo Status
                            else if (sort.ToUpper() == "STATUS" && type.ToUpper() == "DESC")
                                model = model.OrderByDescending(x => x.StatusName);
                            else if (sort.ToUpper() == "STATUS" && type.ToUpper() == "ASC")
                            {
                                model = model.OrderBy(x => x.StatusName);
                            }
                            //Sort theo department
                            else if (sort.ToUpper() == "DEPARTMENT" && type.ToUpper() == "DESC")
                                model = model.OrderByDescending(x => x.DepartmentID);
                            else if (sort.ToUpper() == "DEPARTMENT" && type.ToUpper() == "ASC")
                            {
                                model = model.OrderBy(x => x.DepartmentID);
                            }
                        }

                        return Json(new
                        {
                            status = true,
                            total = totalRow,
                            data = model,
                            listStatus,
                            listCategories,
                            listSubCategories,
                            listLocations,
                            pageSize,
                            colorCodes,
                            page,
                            role = user.RoleID
                        }, JsonRequestBehavior.AllowGet);

                    }
                }
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);

            }
            return Json(new
            {
                status = false
            }, JsonRequestBehavior.AllowGet);
        }

        public object GeneralLoadData(string search, int page, int pageSize, string sort, string type)
        {
            var listTeamID = new List<int>();
            //Sesssion
            var user = (DemoDoan.ViewModel.UserVM)Session["ACCOUNT"];
            //query lay tat ca record theo ngon ngu hien ma user chon

            var LangID = Session[UserVM.CurrentCulture].ToString();
            var listStatus = db.Status.Where(x => x.LanguageID == LangID);
            var listCategories = db.Categories.Where(x => x.LanguageID == LangID);
            var listSubCategories = db.SubCategories.Where(x => x.LanguageID == LangID);
            var listLocations = db.Locations.Where(x => x.LanguageID == LangID);

            //
            var records = db.Records.Where(x => x.LanguageID == LangID);
            var locations = db.Locations.Where(x => x.LanguageID == LangID);
            var categories = db.Categories.Where(x => x.LanguageID == LangID);
            var status = db.Status.Where(x => x.LanguageID == LangID);
            var teamids = db.Teams.Where(x => x.LanguageID == LangID);
            var departments = db.Departments.Where(x => x.LanguageID == LangID);
            var subCategories = db.SubCategories.Where(x => x.LanguageID == LangID);
            var colorCodes = db.ColorCodes.ToList();
            var model = (from record in records
                             //
                             //join lang in db.Language on record.LanguageID equals lang.LanguageID
                             //where record.LanguageID == user.LanguageID
                             //trong bang location tai cot locationid , neu bang location number , do du lieu vao sr
                         join loc in locations on record.LocationID equals loc.Number into sr
                         // 
                         from x in sr.DefaultIfEmpty()
                         join cate in categories on record.CategoryID equals cate.CategoryID
                         join sta in status on record.StatusID equals sta.StatusID into aa
                         from x2 in aa.DefaultIfEmpty()
                         join tea in teamids on record.TeamID equals tea.ID into bb
                         from x3 in bb.DefaultIfEmpty()
                         join dep in departments on record.DepartmentID equals dep.DepartmentID into ab
                         from x4 in ab.DefaultIfEmpty()
                             //Left Join LinQ
                         join sub in subCategories on record.SubCategoryID equals sub.SubCategoryID into scate
                         from x5 in scate.DefaultIfEmpty()
                         join usr in db.UserAccounts on record.UserID equals usr.UserID
                         //buoc nay la buoc bat buoc
                         select new LoadDataRecordVM
                         {
                             StatusID = x2.StatusID,
                             UserID = record.UserID,//
                             RecordID = record.RecordID,//
                             TrackingID = record.TrackingID,//
                             SubCategoryID = record.SubCategoryID,//
                             Title = record.Title,//
                             Location = x.Content == null ? "N/A" : x.Content,//
                             DocumentCategory = cate.Name,//
                             CreatedDate = record.CreatedDate,//
                             Modifieddate = record.Modifieddate,//
                             Description = record.Description,//
                             StatusName = x2.Name == null ? "Wait for document" : x2.Name,//
                             TeamName = x3.Name == null ? "N/A" : x3.Name,
                             Department = x4.Name == null ? "N/A" : x4.Name,//
                             ApprovalSheetCategory = x5.Name == null ? "N/A" : x5.Name,//
                             Username = usr.Username,
                             CategoryID = cate.CategoryID,
                             LocationID = record.LocationID,

                         }).AsEnumerable();
            var data = model.ToList();
            int totalRow = model.Count();
            if (!search.IsNullOrEmpty())
            {
                model = model.Where(x => x.TrackingID.Contains(search));
            }

            model = model.OrderByDescending(x => x.CreatedDate)
              .Skip((page - 1) * pageSize)
              .Take(pageSize).ToList();

            if (!sort.IsNullOrEmpty() && !type.IsNullOrEmpty())
            {
                //Sort theo createddate
                if (sort.ToUpper() == "CREATEDDATE" && type.ToUpper() == "DESC")
                    model = model.OrderByDescending(x => x.CreatedDate);
                else if (sort.ToUpper() == "CREATEDDATE" && type.ToUpper() == "ASC")
                {
                    model = model.OrderBy(x => x.CreatedDate);
                }

                //Sort theo Status
                else if (sort.ToUpper() == "STATUS" && type.ToUpper() == "DESC")
                    model = model.OrderByDescending(x => x.StatusName);
                else if (sort.ToUpper() == "STATUS" && type.ToUpper() == "ASC")
                {
                    model = model.OrderBy(x => x.StatusName);
                }
                //Sort theo department
                else if (sort.ToUpper() == "DEPARTMENT" && type.ToUpper() == "DESC")
                    model = model.OrderByDescending(x => x.DepartmentID);
                else if (sort.ToUpper() == "DEPARTMENT" && type.ToUpper() == "ASC")
                {
                    model = model.OrderBy(x => x.DepartmentID);
                }
            }

            return Json(new
            {
                status = true,
                total = totalRow,
                data = model,
                listStatus,
                listCategories,
                listSubCategories,
                listLocations,
                pageSize,
                page,
                role = user.RoleID
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult Add(Record record)
        {
            try
            {
                var user = (DemoDoan.ViewModel.UserVM)Session["ACCOUNT"];
                var LangID = Session[UserVM.CurrentCulture].ToString();
                var TrackingID = string.Empty;
                //Neu user thuoc department thi render ra trackingID
                if (user.RoleID == 1)
                {
                    TrackingID = RenderTrackingID(0);
                }
                else
                {
                    if (user.DepartmentID > 0)
                    {
                        TrackingID = RenderTrackingID(user.DepartmentID);
                    }
                    //Nguoc lai thi fai di tim departmentID cua team do
                    else
                    {
                        var departmentID = db.Teams.Find(user.TeamID).DepartmentID;
                        TrackingID = RenderTrackingID(departmentID);
                    }
                }

                record.LanguageID = LangID;
                record.TrackingID = TrackingID;
                record.CreatedDate = DateTime.Now;
                record.Modifieddate = DateTime.Now;
                record.UserID = user.UserID;
                db.Records.Add(record);
                db.SaveChanges();
                return Json(new
                {
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        public JsonResult Adduser(Record record)
        {
            try
            {
                var user = (DemoDoan.ViewModel.UserVM)Session["ACCOUNT"];
                var LangID = Session[UserVM.CurrentCulture].ToString();
                var TrackingID = string.Empty;
                //Neu user thuoc department thi render ra trackingID
                if (user.DepartmentID > 0)
                {
                    TrackingID = RenderTrackingID(user.DepartmentID);
                    record.LanguageID = LangID;
                    record.TrackingID = TrackingID;
                    record.CreatedDate = DateTime.Now;
                    record.Modifieddate = DateTime.Now;
                    record.UserID = user.UserID;
                    record.DepartmentID = user.DepartmentID;
                    record.LocationID = user.LocationID;
                    record.StatusID = 4; //set gia tri mac dinh la 4 = Wait For Document trong db

                }
                //Nguoc lai thi fai di tim departmentID cua team do
                else
                {
                    var departmentID = db.Teams.Find(user.TeamID).DepartmentID;
                    record.LanguageID = LangID;
                    TrackingID = RenderTrackingID(departmentID);
                    record.TrackingID = TrackingID;
                    record.CreatedDate = DateTime.Now;
                    record.Modifieddate = DateTime.Now;
                    record.UserID = user.UserID;
                    record.TeamID = user.TeamID;
                    record.LocationID = user.LocationID;
                    record.StatusID = 4; //set gia tri mac dinh la 4 = Wait For Document trong db
                }

                db.Records.Add(record);
                db.SaveChanges();

                //Khi add record xong thi add vao bang thong bao
                //b1 tao 1 doi tuong ten la Notifications o trong model

                var notification = new Notifications();
                notification.UserID = user.UserID;// thang 
                notification.Action = "Add";
                notification.RecordID = record.RecordID;//todolist

                //b2 goi class Dao co chua ham add notificationdetail
                new NotificationsDao().Add(notification);

                var listUserID = new List<int>();
                listUserID.Add(user.UserID); //session
                listUserID.Add(1); //admin

                foreach (var userid in listUserID)
                {
                    //b1 tao 1 doi tuong ten la notificationdetail o trong model

                    var notificationdetail = new NotificationDetail();
                    notificationdetail.NotificationID = notification.NotificationID;
                    notificationdetail.UserID = userid;

                    //b2 goi class Dao co chua ham add notificationdetail
                    new NotificationsDao().AddNotificationDetail(notificationdetail);
                }

                return Json(new
                {
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult EditRange(string listID, int StatusID)
        {
            try
            {
                //Cat chuoi, lay danh sach ToList hoac lay mang ToArray
                var list = listID.Split(',').Select(Int32.Parse).ToArray();
                var records = db.Records.Where(f => list.Contains(f.RecordID)).ToList(); //7 cai tai lieu ma dc duyet
                records.ForEach(a => a.StatusID = StatusID);
                db.SaveChanges();

                //Khi add record xong thi add vao bang thong bao
                //b1 tao 1 doi tuong ten la Notifications o trong model
                foreach (var item in records)
                {

                    var notification = new Notifications();
                    notification.UserID = 1;// admin 
                    notification.Action = "Update";//UpdateStatus
                    notification.RecordID = item.RecordID;//Lan 1:co RecordID la 21/ Lan 2: RecordID la 22/ Lan 2: RecordID la 23


                    //b2 goi class Dao co chua ham add notificationdetail
                    new NotificationsDao().Add(notification);

                    var notificationdetail = new NotificationDetail();
                    notificationdetail.NotificationID = notification.NotificationID;
                    notificationdetail.UserID = item.UserID;//Lan 1:cua userID la thang id = 4 ,/ Lan 2: id = 2 truc/ Lan 3:thang id = 4 
                    //b2 goi class Dao co chua ham add notificationdetail
                    new NotificationsDao().AddNotificationDetail(notificationdetail);
                }


                return Json(new
                {
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult LocationEditRange(string listID, int LocationID)
        {
            try
            {
                //Copy ham tren sua cai statusID thanh location
                //Nho dat ten ham giong ben js cho do nham
                //Xong roi gui listID co checkbox:checked va locationID 

                //Cat chuoi, lay danh sach ToList hoac lay mang ToArray
                var list = listID.Split(',').Select(Int32.Parse).ToArray();
                var records = db.Records.Where(f => list.Contains(f.RecordID)).ToList();
                records.ForEach(a => a.LocationID = LocationID);
                db.SaveChanges();
                return Json(new
                {
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new
                {
                    status = false
                }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult Edit(Record record)
        {
            var item = db.Records.Find(record.RecordID);

            item.Modifieddate = DateTime.Now;

            if (!record.Title.IsNullOrEmpty())
            {
                item.Title = record.Title;
            }

            //Neu description !isnullorempty
            if (!record.Description.IsNullOrEmpty())
            {
                item.Description = record.Description;
            }
            if (record.StatusID != 0)
            {
                item.StatusID = record.StatusID;
            }
            if (record.CategoryID != 0)
            {
                item.CategoryID = record.CategoryID;
            }
            if (record.SubCategoryID != 0)
            {
                item.SubCategoryID = record.SubCategoryID;
            }
            if (record.LocationID != 0)
            {
                item.LocationID = record.LocationID;
            }
            db.SaveChanges();
            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public async Task<JsonResult> GetAllSelect()
        {
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            //var record = serializer.Deserialize<Record>(obj);
            //Lay danh sach
            var LangID = Session[UserVM.CurrentCulture].ToString();

            var listStatus =await db.Status.Select(x=>new { x.StatusID, x.StatusLangs.FirstOrDefault(a=>a.LanguageID== LangID).Name }).ToListAsync();
            var listCategories =await db.Categories.Select(x => new { x.CategoryID, x.CategoryLangs.FirstOrDefault(a => a.LanguageID == LangID).Name }).ToListAsync();
            var listSubCategories =await db.SubCategories.Select(x => new { x.SubCategoryID, x.SubCategoryLangs.FirstOrDefault(a => a.LanguageID == LangID).Name }).ToListAsync();
            var listLocations =await db.Locations.Select(x => new { x.Number,Content= x.LocationLangs.FirstOrDefault(a => a.LanguageID == LangID).Name }).ToListAsync();
            var listColoCodes =await db.ColorCodes.Select(x => new { x.ID, x.Name }).ToListAsync();
            return Json(new
            {
                //Tra ve
                status = true,
                listStatus = listStatus,
                listCategories = listCategories,
                listSubCategories = listSubCategories,
                listLocations = listLocations,
                listColoCodes = listColoCodes
            }, JsonRequestBehavior.AllowGet);
        }
        //Records/Delete/{id}
        public JsonResult Delete(int id)
        {
            var item = db.Records.Find(id);

            db.Records.Remove(item);

            db.SaveChanges();
            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteRange(string listID)
        {
            //khong cat chuoi duoc 1 phan tu theo dau phay^~// kiem tra listID có chua ; khong?
            if (listID.IndexOf(',') == -1)
            {
                var id = Convert.ToInt32(listID);
                var item = db.Records.Find(id);

                db.Records.Remove(item);

                db.SaveChanges();
                return Json(new
                {
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //Cat chuoi
                var list = listID.Split(',').Select(Int32.Parse).ToArray();
                for (int i = 0; i < list.Length; i++)
                {
                    //vao tim id trong list vua cat chuoi
                    var item = db.Records.Find(list[i]);

                    db.Records.Remove(item);

                    db.SaveChanges();
                }

                return Json(new
                {
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }

        }

        //Ham tao TrackingID
        string RenderTrackingID(object departmentID)
        {
            //tao ra bien ket qua
            var result = string.Empty;

            //Lay ra ngay hien tai
            var currentDate = DateTime.Now.ToString("MM/dd");
            //Khai bao doi tuong random
            Random rnd = new Random();
            //random ra 1 so trong khoang tu 1 - 99
            int inonce = rnd.Next(1, 99);
            //dinh dang theo kieu 01,02,03 ...10
            var number = inonce.ToString("D2");

            //gan ket qua cuoi cung vao bien ket qua
            result = departmentID.ToString() + currentDate.Replace("/", "") + number;
            return result;
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
