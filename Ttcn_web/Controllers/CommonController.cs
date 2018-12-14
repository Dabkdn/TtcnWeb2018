using System.Web.Mvc;

namespace Ttcn_web.Controllers
{
    public class CommonController : Controller
    {
        /// <summary>
        /// template for prevent URL attacks
        /// </summary>
        public ActionResult ThietKeNoiThat()
        {
            var token = Session["token"];

            var username = Session["userName"];

            if (token == null || "ha.nguyen".Equals(username))
            {
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }

            return View();
        }
    }
}