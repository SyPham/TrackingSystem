using DemoDoan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoDoan.Dao
{
    public class RecordDao
    {
        OurDbContext _dbContext = null;
        public RecordDao()
        {
            this._dbContext = new OurDbContext();
        }
        public bool Add(Record entity)
        {
            try
            {
                _dbContext.Records.Add(entity);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return false;
            }

        }
        public bool Update(Record entity)
        {
            try
            {
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                //logging
                return false;
            }

        }
        public bool Delete(int id)
        {

            try
            {
                var record = _dbContext.Records.Find(id);
                _dbContext.Records.Remove(record);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return false;
            }

        }
        public IEnumerable<Record> GetAll()
        {
            return _dbContext.Records.ToList();
        }
        public object LoadData(string name, int page, int pageSize = 3)
        {
            return "";
        }
    }
}