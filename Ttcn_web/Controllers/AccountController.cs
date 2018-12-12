using System.Web.Mvc;
using Ttcn_web.Models;
using Ttcn_web.Services.Abtractions;

namespace Ttcn_web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _loginService;

        public AccountController(IAccountService loginService)
        {
            _loginService = loginService;
        }

        Function function = new Function();

        // GET: Login
        public ActionResult Login()
        {
            Session["token"] = null;
            Session["userId"] = null;

            return View();
        }

        public ActionResult CheckLogin(FormCollection form)
        {
            string check = _loginService.GetToken(form["userName"], form["password"]);

            if (check == "")
            {
                return RedirectToAction("Login");
            }

            Session["token"] = check;

            ADUser user = function.GetUser(form["userName"]);

            if (user == null)
            {
                return RedirectToAction("Login");
            }

            Session["userId"] = user.ADUserID;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(FormCollection signupForm)
        {
            var password = Common.GetMD5(signupForm["password"]);

            var re_password = Common.GetMD5(signupForm["re_password"]);

            if (password != re_password)
            {
                return View();
            }

            string check = _loginService.CreateUser(signupForm);

            if (check == "fail")
            {
                return View();
            }

            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}