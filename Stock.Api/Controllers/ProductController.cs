using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using Stock.Model;
using Stock.Repository;

namespace Stock.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        public ProductController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }
        [HttpGet("GetProduct")]
        public async Task<ActionResult<IEnumerable<object>>> GetProducts()
        {
            var products = await repo.ProductRepository.GetProductsAsync();
            if (products == null || !products.Any())
            {
                return NotFound("Ürün bulunamadı");
            }
            return Ok(products);
        }
        [HttpPost("Save")]
        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());
            Product product = new Product()
            {
                Id = json.Id,
                ProductName = json.ProductName,
                CategoryId = json.CategoryId,




            };
            if (product.Id > 0)
            {
                repo.ProductRepository.Update(product);
            }
            else
            {
                repo.ProductRepository.Create(product);
            }

            repo.SaveChanges();

            return new
            {

                success = true,
                message = "Property saved successfully"
            };

        }
        [HttpPost("DeleteProduct")]
        public async Task<ActionResult> DeleteProduct(int productId)
        {
            var product = await repo.ProductRepository.FindBxyIdAsync(productId);
            if (product == null)
                return NotFound("Ürün Bulunamadı");
            repo.ProductRepository.Delete(product);
           repo.SaveChanges();
            return Ok("Ürün Silindi");
        }

        [HttpGet("GetProductByCatgeoryId/{categoryId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetProductByCatgeoryId(int categoryId)
        {
            var products = await repo.ProductRepository.GetProductByCatgeoryIdAsync(categoryId);

            if (products == null || !products.Any())
            {
                return NotFound("Departman Bulunamadı");
            }
            return Ok(products);
        }
    }
}
