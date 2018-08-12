using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TryWebAPI0813.Models;

namespace TryWebAPI0813.Controllers
{
    public class oldProductsController : ApiController
    {
        //static List<Product> products = new List<Product>
        //{
        //    new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
        //    new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
        //    new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        //};

        //public IEnumerable<Product> GetAllProducts()
        //{
        //    return products;
        //}

        //public IHttpActionResult GetProduct(int id)
        //{
        //    var product = products.FirstOrDefault((p) => p.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(product);
        //}

        //public IHttpActionResult PostProduct(Product product)
        //{
        //    products.Add(product);
        //    return Created(Url.Link("DefaultApi", new { id = product.Id }), product);
        //}
    }
}
