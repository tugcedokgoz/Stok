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
    public class CompanyController : BaseController
    {
        public CompanyController(RepositoryWrapper repo,IMemoryCache cache):base(repo,cache) 
        {
            
        }
        [HttpGet("GetCompany")]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            var companies = await repo.CompanyRepository.GetCompanyAsync();

            if (companies == null || !companies.Any())
            {
                // Şirket bulunamadığında NotFound yerine NoContent dönelim.
                return NoContent();
            }

            return Ok(companies);
        }


        [HttpPost("Save")]
        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());
            Company company = new Company()
            {
                Id = json.Id,
                CompanyName = json.CompanyName,
                SuperiorId = json.SuperiorId,
            };
            if (company.Id > 0)
            {
                repo.CompanyRepository.Update(company);
            }
            else
            {
                repo.CompanyRepository.Create(company);
            }

            repo.SaveChanges();

            return new
            {

                success = true,
                message = "Property saved successfully"
            };


        }

        [HttpPost("DeleteCompany")]
        public async Task<ActionResult>DeleteCompany(int companyId)
        {
            var company= await repo.CompanyRepository.FindByIdAsync(companyId); 
            if(company == null)
            {
                return NotFound("Şirket Bulunamadı");
            }
            repo.CompanyRepository.Delete(company);
           repo.SaveChanges();
            return Ok("Şirket Başarı ile Silindi");
        }
    }
}
