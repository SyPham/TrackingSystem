using DemoDoan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoDoan.Dao
{
    public class NotificationsDao
    {
        
        OurDbContext _dbContext = null;
        public NotificationsDao()
        {
            this._dbContext = new OurDbContext();
        }
        public bool Add(Notifications entity)
        {
            try
            {
                _dbContext.Notifications.Add(entity);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return false;
            }

        }
        public IEnumerable<Notifications> GetAll()
        {
            return _dbContext.Notifications.ToList();
        }

        public bool AddNotificationDetail(NotificationDetail entity)
        {
            try
            {
                _dbContext.NotificationDetails.Add(entity);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return false;
            }

        }
       

        public object GetAll(int userId)
        {
            //var model = from 
            return " " ;
        }

        public object LoadData(string name, int page, int pageSize = 3)
        {
            return "";
        }
    }
}
