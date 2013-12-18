using System.Web.Mvc;

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

    }
}
