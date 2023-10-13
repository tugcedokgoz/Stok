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
    public class RoleUserController : BaseController
    {
        public RoleUserController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }

        [HttpGet("GetRoleUser")]
        public async Task<ActionResult<IEnumerable<object>>> GetRoleUser()
        {
            var roleUser = await repo.RoleUserRepository.GetRoleUserAsync();
            if(roleUser == null|| !roleUser.Any())
            {
                return NotFound("İstediğiniz veri bulunamadı");
            }
            return Ok(roleUser);
        }
        [HttpGet("GetUsersByCompanyId/{companyId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetUsersByCompanyId(int companyId)
        {
            var users = await repo.RoleUserRepository.GetUsersByCompanyId(companyId);

            if (users == null || !users.Any())
            {
                return NotFound("İstediğiniz veri bulunamadı");
            }

            return Ok(users);
        }
        [HttpPost("Save")]
        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());
            RoleUser roleUser = new RoleUser()
            {
                Id = json.Id,
                UserId = json.UserId,
                RoleId = json.RoleId,
                CompanyId = json.CompanyId,


            };
            if (roleUser.Id > 0)
            {
                repo.RoleUserRepository.Update(roleUser);
            }
            else
            {
                repo.RoleUserRepository.Create(roleUser);
            }

            repo.SaveChanges();

            return new
            {

                success = true,
                message = "Property saved successfully"
            };

        }
        [HttpPost("DeleteRoleUser")]
        public async Task<ActionResult> DeleteRoleUser(int roleUserId)
        {
            var roleUser = await repo.RoleUserRepository.FindByIdAsync(roleUserId);
            if (roleUser == null)
                return NotFound("Silmek istenilen Veri Bulunamadı");
            repo.RoleUserRepository.Delete(roleUser);
             repo.SaveChanges();
            return Ok("Veri Başarı İle Silindi");
        }

    }
}
