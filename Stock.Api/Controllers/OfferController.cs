using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Stock.Repository;

namespace Stock.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : BaseController
    {
        public OfferController(RepositoryWrapper repo, IMemoryCache cache) : base(repo, cache)
        {

        }
        [HttpGet("GetOffers")]
        public async Task<ActionResult<IEnumerable<object>>> GetOffers()
        {
            var offers = await repo.OfferRepository.GetOffersAsync();
            if (offers == null || !offers.Any())
            {
                return NotFound("Ürün bulunamadı");
            }
            return Ok(offers);
        }
      

    }
}
