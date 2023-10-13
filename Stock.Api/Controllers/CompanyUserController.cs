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
    public class CompanyUserController : BaseController
    {
        public CompanyUserController(RepositoryWrapper repo,IMemoryCache cache):base(repo,cache) 
        {
            
        }
        [HttpGet("GetCompanyUser")]
        public async Task<ActionResult<IEnumerable<object>>> GetCompanyUser()
        {
            var companyUser = await repo.CompanyUserRepository.GetCompanyUserAsync();
            if (companyUser == null || !companyUser.Any())
            {
                return NotFound("bulunamadı");
            }
            return Ok(companyUser);
        }


        [HttpPost("Kaydet")]
        public dynamic Kaydet([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());

            CompanyUser item = new CompanyUser()
            {
                Id = json.Id,
                CompanyId = json.CompanyId,
                DepartmentId = json.DepartmentId,
                UserId = json.UserId,
                RoleId = json.RoleId,
                SuperiorId = json.SuperiorId
           
            };
            if (item.Id > 0)
            {
                repo.CompanyUserRepository.Update(item);
            }
            else
            {
                repo.CompanyUserRepository.Create(item);
            }
            repo.SaveChanges();
            return new
            {
                success = true
            };
        }
    }
}
