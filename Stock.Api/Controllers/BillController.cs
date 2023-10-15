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
    public class BillController : BaseController
    {
        public BillController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }
        [HttpGet("GetBill")]
        public async Task<ActionResult<IEnumerable<object>>> GetBills()
        {
            var bills = await repo.BillRepository.GetBillAsync();
            if (bills == null || !bills.Any())
                return NotFound("Fatura Bulunamadı");
            return Ok(bills);
        }
        [HttpPost("Save")]
        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());
            Bill bill = new Bill()
            {
                Id = json.Id != null ? (int)json.Id : 0,
                Price = json.Price != null ? (decimal)json.Price : 0m,
                Amount = json.Amount != null ? (int)json.Amount : 0,
                CategoryId = json.CategoryId != null ? (int)json.CategoryId : 0,
                ProductId = json.ProductId != null ? (int)json.ProductId : 0,
                SupplierCompanyId = json.SupplierCompanyId != null ? (int)json.SupplierCompanyId : 0,    
                CreateDate = json.CreateDate != null ? (DateTime)json.CreateDate : DateTime.Now


            };
            if (bill.Id > 0)
            {
                repo.BillRepository.Update(bill);
            }
            else
            {
                repo.BillRepository.Create(bill);
            }

            repo.SaveChanges();

            return new
            {

                success = true,
                message = "Property saved successfully"
            };


        }
        [HttpPost("DeleteBill")]
        public async Task<ActionResult> DeleteBill(int billId)
        {
            var bill = await repo.BillRepository.FindByIdAsync(billId);
            if (bill == null)
                return NotFound("Fatura Bulunamadı");
            repo.BillRepository.Delete(bill);
            repo.SaveChanges();
            return Ok("Fatura Başarı ile silindi");
        }
    }
}
