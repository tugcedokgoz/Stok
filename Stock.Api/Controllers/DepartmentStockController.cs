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
    public class DepartmentStockController : BaseController
    {

        public DepartmentStockController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }

        [HttpGet("GetDepartmentStock")]
        public async Task<ActionResult<IEnumerable<object>>> GetDepartmentStock()
        {
            var departmentStock = await repo.DepartmentStockRepository.GetDepartmentStockAsync();
            if (departmentStock == null | !departmentStock.Any())
            {
                return NotFound("Stok Bulunamadı");
            }
            return Ok(departmentStock);
        }
        [HttpPost("Save")]
        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());
            DepartmentProductStock departmentProductStock = new DepartmentProductStock()
            {
                Id = json.Id,
                DepartmentId = json.DepartmentId,
                ProductId = json.ProductId,
                Amount = json.Amount,



            };
            if (departmentProductStock.Id > 0)
            {
                repo.DepartmentStockRepository.Update(departmentProductStock);
            }
            else
            {
                repo.DepartmentStockRepository.Create(departmentProductStock);
            }

            repo.SaveChanges();

            return new
            {

                success = true,
                message = "Department saved successfully"
            };

        }
        [HttpPost("Delete")]
        public async Task<ActionResult> DeleteDepartmentStock(int departmentStockId)
        {
            var departmentStock = await repo.DepartmentStockRepository.FindByIdAsync(departmentStockId);
            if (departmentStock == null)
                return NotFound("Stok Bulunamadı");
            repo.DepartmentStockRepository.Delete(departmentStock);
            repo.SaveChanges();
            return Ok("Stok Başarı ile silindi");

        }

        [HttpGet("GetDepartmentProductStockByDepartmentId/{departmentId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetDepartmentProductStockByDepartmentId(int departmentId)
        {
            var departmentStock = await repo.DepartmentStockRepository.GetDepartmentStockByDepartmentIdAsync(departmentId);

            if (departmentStock == null || !departmentStock.Any())
            {
                return NotFound("Belirtilen departmana ait stok bulunamadı");
            }

            return Ok(departmentStock);
        }

    }
}
