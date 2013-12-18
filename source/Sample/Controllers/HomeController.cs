using System.Web.Mvc;
using MvcFlashMessages;

namespace Sample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            this.Flash("info", "You were redirected.");
            return RedirectToAction("Index", "Products");
        }
    }
}
