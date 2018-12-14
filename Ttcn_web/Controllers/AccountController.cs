using System.Web.Mvc;
using Ttcn_web.Models;
using Ttcn_web.Services.Abtractions;

namespace Ttcn_web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _loginService;
		
		private readonly ISaleOrderService _saleOrderService;

        public AccountController(IAccountService loginService, ISaleOrderService saleOrderService)
        {
            _loginService = loginService;
			_saleOrderService = saleOrderService;
        }

        Function function = new Function();

        // GET: Login
        public ActionResult Login()
        {
            Session.Clear();

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
            Session["userName"] = user.ADUserName;
			Session["CartItemQty"] = _saleOrderService.GetItemQuantityInSaleOrderByUserID(user.ADUserID);

            Session["userType"] = _loginService.GetUserGroupNameByID(user.FK_ADUserGroupID);

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

            string userType = Request.Form["userType"].ToString();

            string check = _loginService.CreateUser(signupForm, userType);

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