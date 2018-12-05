using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ttcn_web.Models;
using Ttcn_web.Services.Abtractions;

namespace Ttcn_web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        Function function = new Function();
        // GET: Login
        public ActionResult Login()
        {
            Session["token"] = "";
            Session["userId"] = "";
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

            return RedirectToAction("About", "Home");
        }
    }
}