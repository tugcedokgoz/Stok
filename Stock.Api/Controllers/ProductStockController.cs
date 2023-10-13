using Microsoft.AspNetCore.Authorization;
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
    public class ProductStockController : BaseController
    {
        public ProductStockController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }
  
        [HttpGet("GetProductStock")]
        public async Task<ActionResult<IEnumerable<object>>> GetProductStocks()
        {
            var productStock = await repo.ProductStockRepository.GetProductStockAsync();
            if(productStock == null || !productStock.Any())
            {
                return NotFound("Stok Bulunamadı");
            }
            return Ok(productStock);
        }
        [HttpPost("Save")]
        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());
            ProductStock productStock = new ProductStock()
            {
                Id = json.Id,
                CategoryId = json.CategoryId,
                ProductId = json.ProductId,
                Piece=json.Piece,



            };
            if (productStock.Id > 0)
            {
                repo.ProductStockRepository.Update(productStock);
            }
            else
            {
                repo.ProductStockRepository.Create(productStock);
            }

            repo.SaveChanges();

            return new
            {

                success = true,
                message = "Property saved successfully"
            };

        }
        [HttpPost("DeleteProductStock")]
        public async Task<ActionResult>DeleteProductStock(int productStockId)
        {
            var productStock = await repo.ProductStockRepository.FindByIdAsync(productStockId);
            if (productStock == null)
                return NotFound("Stok bulunamadı");
            repo.ProductStockRepository.Delete(productStock);
           repo.SaveChanges();
            return Ok(productStock);
        }
    }
}
