using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ttcn_web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            var token = Session["token"];

            var username = Session["userName"];

            if (token == null || "admin".Equals(username)== false)
            {
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }

            return View();
        }
    }
}