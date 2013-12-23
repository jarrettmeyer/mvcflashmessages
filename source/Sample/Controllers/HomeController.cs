using System.Web.Mvc;
using MvcFlashMessages;

namespace Sample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {            
            var action = Url.Action("Index", "Flash");            
            this.Flash("info", string.Format("You were redirected to <a href=\"{0}\">{0}</a>.", action));
            return RedirectToAction("Index", "Flash");
        }
    }
}
