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
    public class ReportController : BaseController
    {
        public ReportController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }
        [HttpGet("GetReports")]
        public async Task<ActionResult<IEnumerable<object>>> GetReports()
        {
            var reports = await repo.ReportRepository.GetReportsAsync();
            if (reports == null || !reports.Any())
            {
                return NotFound("Rapor bulunamadı");
            }
            return Ok(reports);
        }
        [HttpPost("Save")]
        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());
            Report rep = new Report()
            {
                Id = json.Id,
                UserId = json.UserId,
                UnitId = json.UnitId,
                ProductId = json.ProductId,
                SupplierCompanyId = json.SupplierCompanyId,
            };
            if (rep.Id > 0)
            {
                repo.ReportRepository.Update(rep);
            }
            else
            {
                repo.ReportRepository.Create(rep);
            }

            repo.SaveChanges();

            return new
            {

                success = true,
                message = "Property saved successfully"
            };

        }
        [HttpPost("DeleteReport")]
        public async Task<ActionResult> DeleteReport(int reportId)
        {
            var report = await repo.ReportRepository.FindByIdAsync(reportId);
            if (reportId == null)
                return NotFound("Rapor Bulunamadı");
            repo.ReportRepository.Delete(report);
           repo.SaveChanges();
            return Ok("Rapor Başarı ile silindi");
        }
    }
}
