using System.Collections.Generic;
using System.Web.Mvc;

using Ttcn_web.Models;
using Ttcn_web.Models.DTO;

namespace Ttcn_web.Services.Abtractions
{
    public interface ICartService
    {
        Carts GetCartsOfUser(int userID);
    }
}
