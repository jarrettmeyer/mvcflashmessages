using System.Web.Mvc;
using MvcFlashMessages;

namespace Sample.Controllers
{
    public class FlashController : Controller
    {
        public ActionResult Error()
        {
            this.Flash("error", "Ack! Something awful happened!");
            return View("Index");
        }

        public ActionResult Index()
        {
            this.Flash("unstyled", "This is an unstyled flash message.");
            return View();
        }

        public ActionResult Info()
        {
            this.Flash("info", "This messages is providing you with some very important information.");
            return View("Index");
        }

        public ActionResult Success()
        {
            this.Flash("success", "Good job! Whatever you did must have worked!");
            return View("Index");
        }

        public ActionResult Warning()
        {
            this.Flash("warning", "Something almost broke. This is a warning. Whatever you just did, you probably shouldn't do it again.");
            return View("Index");
        }
    }
}
