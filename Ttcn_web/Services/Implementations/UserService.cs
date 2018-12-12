using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using Ttcn_web.Models;
using Ttcn_web.Services.Abtractions;

namespace Ttcn_web.Services.Implementations
{
    public class UserService : IUserService
    {
        private TtcnWebEntities db = new TtcnWebEntities();

        public IEnumerable<ADUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public ADUser GetUserByID(int userUD)
        {
            ADUser objUser = db.ADUsers.Where(user => user.ADUserID == userUD).FirstOrDefault();
            return objUser;
        }
    }
}