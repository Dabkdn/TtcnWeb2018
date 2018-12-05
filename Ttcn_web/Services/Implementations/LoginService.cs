using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ttcn_web.Models;
using Ttcn_web.Services.Abtractions;

namespace Ttcn_web.Services.Implementations
{
    public class LoginService : ILoginService
    {
        private TtcnWebEntities context = new TtcnWebEntities();

        public string GetToken(string userName, string password)
        {
            //password = Common.GetMD5(password); 
            // Todo: 

            ADUser userFromDb = context.ADUsers.FirstOrDefault(x => x.ADUserName == userName && x.ADPassword == password);

            if (userFromDb == null)
            {
                return "";
            }

            string token = Common.GetToken();

            userFromDb.ADUserResetToken = token;

            context.SaveChanges();

            return token;
        }

    }
}