using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using Stock.Model;
using Stock.Repository;

namespace Stock.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : BaseController
    {
        public RequestController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }
        [HttpGet("GetRequests")]
        public async Task<ActionResult<IEnumerable<object>>> GetRequest()
        {
            var request = await repo.RequestRepository.GetRequestAsync();
            if (request == null || !request.Any())
            {
                return NotFound("Talep Bulunamadı");
            }
            return Ok(request);
        }
        [HttpGet("GetRequestsByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetRequestsByUserId(int userId)
        {
            var requests = await repo.RequestRepository.GetRequestsByUserIdAsync(userId);
            if (requests == null || !requests.Any())
            {
                return NotFound("Kullanıcıya ait talep bulunamadı");
            }
            return Ok(requests);
        }
        [HttpGet("GetRequestsByUserSuperiorId/{userSuperiorId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetRequestsByUserSuperiorId(int userSuperiorId)
        {
            var requests = await repo.RequestRepository.GetRequestsBySuperiorIdAsync(userSuperiorId);
            if (requests == null || !requests.Any())
            {
                return NotFound("Kullanıcıya ait talep bulunamadı");
            }
            return Ok(requests);
        }
        [HttpPost("Delete")]
        public dynamic Delete(int id)
        {
            if (id < 0)
                return new
                {
                    success = false,
                    message = "Invalid Id"
                };
            repo.RequestRepository.Delete(id);
            return new
            {
                success = true,
                message = "Deleted"
            };
        }
        [HttpPost("Save")]
        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());

            int userId = json.UserId != null ? (int)json.UserId : 0;
            int superiorId = repo.RequestRepository.GetSuperiorIdForUser(userId);

            Requests request = new Requests()
            {
                Id = json.Id != null ? (int)json.Id : 0,
                UserId = userId,
                CategoryId = json.CategoryId != null ? (int)json.CategoryId : 0,
                ProductId = json.ProductId != null ? (int)json.ProductId : 0,
                Amount = json.Amount != null ? (int)json.Amount : 0,
                CreateDate = json.CreateDate ?? DateTime.Now,
                RequestStatusId = json.RequestStatusId != null ? (int)json.RequestStatusId : 1,
                UserSuperiorId = superiorId
            };

            if (json.Status == null)
            {
                request.Status = false;
            }
            else
            {
                request.Status = json.Status;
            }

            if (request.Id > 0)
            {
                repo.RequestRepository.Update(request);
            }
            else
            {
                repo.RequestRepository.Create(request);
            }

            repo.SaveChanges();

            return new
            {
                success = true,
                message = "Property saved successfully"
            };
        }
        //request status güncelleme kodu
        [HttpPost("UpdateRequestStatus/{requestId}/{status}")]
        public IActionResult UpdateRequestStatus(int requestId, int status)
        {
            var request = repo.RequestRepository.FindByIdAsync(requestId).Result;

            if (request == null)
            {
                return NotFound("Talep Bulunamadı");
            }

            request.RequestStatusId = status; // Burada status, talep durumu için uygun bir değer olmalıdır.

            repo.RequestRepository.Update(request);
            repo.SaveChanges();

            return Ok("Talep Durumu Güncellendi");
        }


        //requeststatus güncelleme
        [HttpGet("GetRequestsByStatus/{statusId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetRequestsByStatus(int statusId)
        {
            var requests = await repo.RequestRepository.GetRequestsByStatusAsync(statusId);
            if (requests == null || !requests.Any())
            {
                return NotFound("Belirtilen duruma sahip talep bulunamadı");
            }
            return Ok(requests);
        }



    }
}
