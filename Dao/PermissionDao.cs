using DemoDoan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoDoan.Dao
{
    public class PermissionDao
    {
        private OurDbContext _dbContext;
        public PermissionDao()
        {
            _dbContext = new OurDbContext();
        }
        public string CheckRole(int roleid)
        {
            return _dbContext.Roles.FirstOrDefault(x => x.RoleID == roleid)?.Code.ToUpper() ?? "";
        }
    }
}