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
    public class RoleController : BaseController
    {
        public RoleController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }

        [HttpGet("GetRoles")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var roles = await repo.RoleRepository.GetRolesAsync();

            if (roles == null || !roles.Any())
            {
                return NotFound("Rol bulunamadı.");
            }

            return Ok(roles);
        }

        [HttpPost("SaveRole")]
        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());

            Role role = new Role()
            {
                Id = json.Id,
                RoleName = json.roleName,
            };


            if (role.Id > 0)
            {
                repo.RoleRepository.Update(role);
            }
            else
            {
                repo.RoleRepository.Create(role);
            }

            repo.SaveChanges();

            return new
            {
                success = true,
                message = "Role saved successfully"
            };
        }

        [HttpPost("DeleteRole")]
        public async Task<ActionResult> DeleteRole(int roleId)
        {
            var role = await repo.RoleRepository.FindByIdAsync(roleId);

            if (role == null)
            {
                return NotFound("Rol bulunamadı.");
            }

            repo.RoleRepository.Delete(role);
             repo.SaveChanges(); 

            return Ok("Rol başarıyla silindi.");
        }


    }
}
