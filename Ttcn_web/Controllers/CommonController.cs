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

            if (token == null)
            {
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }

            return View();
        }
    }
}