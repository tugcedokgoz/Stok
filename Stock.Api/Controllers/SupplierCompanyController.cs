using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Stock.Model;
using Stock.Repository;

namespace Stock.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierCompanyController : BaseController
    {
        public SupplierCompanyController(RepositoryWrapper repo,IMemoryCache cache):base(repo,cache) 
        {
            
        }
        [HttpGet("GetSupplierCompany")]
        public async Task<ActionResult<IEnumerable<SupplierCompany>>> GetSupplierCompany()
        {
            var supplier = await repo.SupplierCompanyRepository.GetSupplierAsync();

            if (supplier == null || !supplier.Any())
            {
                return NotFound("Tedarikçi bulunamadı.");
            }

            return Ok(supplier);
        }
        //public async Task<ActionResult<IEnumerable<object>>> GetSupplierCompany()
        //{
        //    var supplierCompany = await repo.SupplierCompanyRepository.GetSupplierCompanyAsync();
        //    if (supplierCompany == null || !supplierCompany.Any())
        //    {
        //        return NotFound("Tedarikçi Bulunamadı");
        //    }
        //    return Ok(supplierCompany);
        //}
    }
}
