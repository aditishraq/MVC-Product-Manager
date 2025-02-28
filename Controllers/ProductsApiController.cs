using System.Linq;
using System.Web.Http;
using MvcApp.Models;

namespace MvcApp.Controllers
{
    // You can use attribute routing if you enabled it
    [RoutePrefix("api/products")]
    public class ProductsApiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/products
        [HttpGet, Route("")]
        public IHttpActionResult GetProducts()
        {
            var products = db.Products.ToList();
            return Ok(products);
        }

        // GET: api/products/5
        [HttpGet, Route("{id:int}", Name = "GetProductById")]
        public IHttpActionResult GetProduct(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: api/products
        [HttpPost, Route("")]
        public IHttpActionResult CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            db.SaveChanges();
            return CreatedAtRoute("GetProductById", new { id = product.Id }, product);
        }


        // PUT: api/products/5
        [HttpPut, Route("{id:int}")]
        public IHttpActionResult UpdateProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != product.Id)
            {
                return BadRequest("ID mismatch");
            }

            var existing = db.Products.Find(id);
            if (existing == null)
            {
                return NotFound();
            }

            existing.Name = product.Name;
            existing.Price = product.Price;
            db.SaveChanges();

            return Ok(existing);
        }

        // DELETE: api/products/5
        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult DeleteProduct(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
