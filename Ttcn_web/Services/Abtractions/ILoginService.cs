using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

using Ttcn_web.Models;

namespace Ttcn_web.Services.Abtractions
{
    public interface ILoginService
    {
        string GetToken(string userName, string password);
    }
}
