using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using Stock.Model;
using Stock.Repository;

namespace Stock.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : BaseController
    {
        public BidController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }
        //[Authorize(Roles = "Yönetim Kurulu Başkanı")]
        [HttpGet("GetBid")]
        public async Task<ActionResult<IEnumerable<object>>> GetBids()
        {
            var bids = await repo.BidRepository.GetBidsAsync();
            if (!bids.Any())
            {
                return NotFound("Teklif Bulunamadı");
            }
            return Ok(bids);
        }

        [HttpPost("Save")]
        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());
            Bid bid = new Bid()
            {
                Id = json.Id,
                SupplierCompanyId = json.SupplierCompanyId,
                ProductId = json.ProductId,
                Price = json.Price,
         
            };
            if (bid.Id > 0)
            {
                repo.BidRepository.Update(bid);
            }
            else
            {
                repo.BidRepository.Create(bid);
            }

            repo.SaveChanges();

            return new
            {

                success = true,
                message = "Property saved successfully"
            };


        }

        [HttpPost("DeleteBid")]
        public async Task<ActionResult> DeleteBid(int bidId)
        {
            var bid = await repo.BidRepository.FindByIdAsync(bidId);
            if (bid == null)
                return NotFound("Ürün bulunamadı");
            repo.BidRepository.Delete(bid);
             repo.SaveChanges();
            return Ok(bid);
        }
    }
}
