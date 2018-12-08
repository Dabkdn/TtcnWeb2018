using System.Web.Mvc;

namespace Ttcn_web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index", "_Layout");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}