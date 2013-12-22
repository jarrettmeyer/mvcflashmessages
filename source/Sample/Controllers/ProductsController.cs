using System.Web.Mvc;
using MvcFlashMessages;
using Sample.Models;

namespace Sample.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductRepository repository;

        public ProductsController()
        {
            repository = new ProductRepository();
        }

        public ActionResult Index()
        {
            var products = repository.FindAll();
            return View("Index", products);
        }

        [HttpGet]
        public ActionResult New()
        {
            var product = new Product();
            return View("New", product);
        }

        [HttpPost]
        public ActionResult New(Product product)
        {
            repository.Add(product);
            this.Flash("success", "Successfully saved new product!");
            return RedirectToAction("Index", "Products");
        }
    }
}
