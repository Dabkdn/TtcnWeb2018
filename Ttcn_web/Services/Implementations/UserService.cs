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

        public ADUser Get(int userId)
        {
            return db.ADUsers.Find(userId);
        }

        public IEnumerable<ADUser> GetAll()
        {
            return db.ADUsers.Where(x => x.AAStatus == "Alive").ToList();
        }

        public ADUser GetUserByID(int userUD)
        {
            ADUser objUser = db.ADUsers.Where(user => user.ADUserID == userUD).FirstOrDefault();
            return objUser;
        }

        public void Create(FormCollection formCollection)
        {
            var user = new ADUser();
            var lastUser = db.ADUsers.ToList().LastOrDefault();

            user.ADUserID = lastUser.ADUserID + 1;
            user.AAStatus = "Alive";
            user.AACreatedDate = DateTime.Now;
            user.ADUserName = formCollection["username"];
            user.ADPassword = Common.GetMD5(formCollection["password"]);
            user.FK_ADUserGroupID = 1;

            if (user == null)
            {
                return;
            }

            db.ADUsers.Add(user);
            db.SaveChanges();
        }

        public void Edit(FormCollection formCollection, int userId)
        {
            var user = db.ADUsers.Find(userId);

            if (user == null)
            {
                return;
            }

            user.AAUpdatedDate = DateTime.Now;
            user.ADUserName = formCollection["username"];
            user.ADPassword = Common.GetMD5(formCollection["password"]);

            db.ADUsers.AddOrUpdate();
            db.SaveChanges();
        }

        public void Delete(int userId)
        {
            var user = db.ADUsers.Find(userId);

            if (user == null)
            {
                return;
            }

            user.AAStatus = "Delete";

            db.ADUsers.AddOrUpdate();
            db.SaveChanges();
        }
    }
}