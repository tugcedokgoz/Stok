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
    public class UserController : BaseController
    {
        public UserController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }
        [HttpGet("GetUsers")]
        public async Task<ActionResult<IEnumerable<object>>> GetUsers()
        {
            var users = await repo.UserRepository.GetUsersAsync();
            if (users == null || !users.Any())
            {
                return NotFound("Kullanıcı bulunamadı");
            }
            return Ok(users);
        }

        [HttpPost("Save")]
        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());
            User user = new User()
            {
                Id = json.Id,
                UserFullName = json.UserFullName,
                UserEmail = json.UserEmail,
                Password = json.Password,
                RoleId = json.RoleId,
                SuperiorId = json.SuperiorId,
                CompanyDepartmentId = json.CompanyDepartmentId,
            };
            if (user.Id > 0)
            {
                repo.UserRepository.Update(user);
            }
            else
            {
                repo.UserRepository.Create(user);
            }

            repo.SaveChanges();
           
            return new
            {
                  
                success = true,
                message = "Property saved successfully"
            };


        }



        [HttpPost("DeleteUser")]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            var user = await repo.UserRepository.FindByIdAsync(userId);
            if (user == null)
                return NotFound("Kullanıcu Bulunamadı.");

            repo.UserRepository.Delete(user);

             repo.SaveChanges();
            return Ok("Kullanıcı başarı ile silindi");
        }

    }
}
