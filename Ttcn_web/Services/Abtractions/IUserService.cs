using System.Collections.Generic;
using System.Web.Mvc;

using Ttcn_web.Models;

namespace Ttcn_web.Services.Abtractions
{
    public interface IUserService
    {
        IEnumerable<ADUser> GetAll();

        ADUser GetUserByID(int userUD);
    }
}
