using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Stock.Repository;

namespace Stock.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierProductController : BaseController
    {
        public SupplierProductController(RepositoryWrapper repo,IMemoryCache cache):base(repo,cache)
        {
            
        }
        [HttpGet("GetSupplierProduct")]
        public async Task<ActionResult<IEnumerable<object>>> GetSupplierProduct()
        {
            var supplierProduct = await repo.SupplierProductRepository.GetSupplierProductAsync();
            if (supplierProduct == null || !supplierProduct.Any())
            {
                return NotFound("Talep Bulunamadı");
            }
            return Ok(supplierProduct);
        }
    }
}
