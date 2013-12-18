using System.Web.Mvc;
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

        public ActionResult New()
        {
            var product = new Product();
            return View("New", product);
        }
    }
}
