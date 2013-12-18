using System.Collections.Generic;
using System.Linq;
using Sample.Models;

namespace Sample
{
    public class ProductRepository
    {
        private static int currentId = 0;
        private readonly static object mutex = new object();
        private readonly static IDictionary<int, Product> products = new Dictionary<int, Product>();

        public void Add(Product product)
        {
            lock (mutex)
            {
                currentId += 1;
                product.Id = currentId;
                products.Add(currentId, product);
            }
        }

        public IQueryable<Product> FindAll()
        {
            return products.Values.AsQueryable();
        }

        public Product FindById(int id)
        {
            return products[id];
        }

        public void Remove(int id)
        {
            lock (mutex)
            {
                products.Remove(id);
            }
        }
    }
}