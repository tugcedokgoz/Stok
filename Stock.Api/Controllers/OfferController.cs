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
        //[HttpPost("SubmitOffer")]
        //public dynamic SubmitOffer([FromBody] dynamic model)
        //{
        //    if (model == null)
        //    {
        //        return new { Status = "Error", Message = "Geçersiz veri" };
        //    }

        //    int offerId = model.offerId;
        //    decimal offerPrice = model.offerPrice;

        //    var offer = repo.OfferRepository.GetOfferById(offerId);

        //    if (offer == null)
        //    {
        //        return new { Status = "Error", Message = "Teklif bulunamadı" };
        //    }

        //    offer.OfferPrice = offerPrice;

        //    if (offerPrice <= 5000)
        //    {
        //        offer.Requests.RequestStatusId = 8;
        //    }
        //    else
        //    {
        //        offer.Requests.RequestStatusId = 11;
        //    }

        //    repo.SaveChanges(); // Değişiklikleri kaydet

        //    return new { Status = "Success", Message = "Teklif başarıyla kaydedildi" };
        //}


    }
}
