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

        //[HttpPost("CreateUser")]
        //public IActionResult CreateUser([FromBody] User user)
        //{
        //    // Kullanıcının ait olduğu rolü bul
        //    var role = repo.RoleRepository.GetRoleByName(user.Role.RoleName);
        //    if (role == null)
        //    {
        //        return BadRequest("Rol bulunamadı");
        //    }

        //    // Kullanıcının ait olduğu şirketi bul
        //    var company = repo.CompanyRepository.GetCompanyByName(user.Company.CompanyName);
        //    if (company == null)
        //    {
        //        return BadRequest("Şirket bulunamadı");
        //    }

        //    // Kullanıcının ait olduğu departmanı bul
        //    var department = repo.CompanyDepartmentRepository.GetDepartmentByName(user.CompanyDepartment.DepartmentName, company.Id);
        //    if (department == null)
        //    {
        //        return BadRequest("Departman bulunamadı");
        //    }

        //    // Kullanıcının üstünü bul
        //    User superior = null;
        //    if (!string.IsNullOrEmpty(user.Superior?.UserFullName))
        //    {
        //        superior = repo.UserRepository.GetUserByName(user.Superior.UserFullName);
        //        if (superior == null)
        //        {
        //            return BadRequest("Üst kullanıcı bulunamadı");
        //        }
        //    }

        //    // Yeni kullanıcı oluştur
        //    user.RoleId = role.Id;
        //    user.Role = role;
        //    user.CompanyId = company.Id;
        //    user.Company = company;
        //    user.CompanyDepartmentId = department.Id;
        //    user.CompanyDepartment = department;
        //    user.SuperiorId = superior?.Id;
        //    user.Superior = superior;

        //    repo.UserRepository.CreateUser(user);

        //    // Değişiklikleri kaydet
        //    repo.SaveChanges();

        //    return Ok("Kullanıcı oluşturuldu");
        //}

    }
}
