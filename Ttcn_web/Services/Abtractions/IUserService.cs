using System.Collections.Generic;
using System.Web.Mvc;

using Ttcn_web.Models;

namespace Ttcn_web.Services.Abtractions
{
    public interface IUserService
    {
        IEnumerable<ADUser> GetAll();

        ADUser Get(int userId);

        void Create(FormCollection formCollection);

        void Edit(FormCollection formCollection, int userId);

        void Delete(int userId);

        ADUser GetUserByID(int userUD);
    }
}
