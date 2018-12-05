using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ttcn_web.Models
{
    public class Login
    {
        private TtcnWebEntities context = new TtcnWebEntities();

        public string CheckLogin(string userName, string password)
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