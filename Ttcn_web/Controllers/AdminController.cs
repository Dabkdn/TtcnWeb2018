using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ttcn_web.Models;

namespace Ttcn_web.Controllers
{
    public class AdminController : Controller
    {
        private TtcnWebEntities db = new TtcnWebEntities();

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

        public ActionResult Revenue()
        {
            var result = db.ARSaleOrders.Where(x => x.AAStatus == "Alive").ToList();

            return View(result);
        }
    }
}