﻿using System.Linq;

namespace Ttcn_web.Models
{
    public class Function
    {
        TtcnWebEntities context = new TtcnWebEntities();

        public ADUser GetUser(string userName)
        {
            return context.ADUsers.FirstOrDefault(x => x.ADUserName == userName);
        }
    }
}