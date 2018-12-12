using System.Web.Mvc;

namespace Ttcn_web.Services.Abtractions
{
    public interface IAccountService
    {
        string GetToken(string userName, string password);

        string CreateUser(FormCollection signupForm);
    }
}
