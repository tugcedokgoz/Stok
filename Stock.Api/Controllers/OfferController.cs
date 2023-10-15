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
        [HttpPost("Save")]
        public dynamic Save([FromBody] dynamic model)
        {
            dynamic json = JObject.Parse(model.GetRawText());

            Offer offer = new Offer()
            {
                Id = json.Id != null ? (int)json.Id : 0,
                RequestId = json.RequestId != null ? (int)json.RequestId : 0,
                OfferPrice = json.OfferPrice != null ? (decimal)json.OfferPrice : 0m,
                Status = json.Status ?? true,
                SupplierCompanyId = json.SupplierCompanyId != null ? (int)json.SupplierCompanyId : 0,
                Amount = json.Amount != null ? (int)json.Amount : 0,
                UnitPrice = json.UnitPrice != null ? (decimal)json.UnitPrice : 0m,
                CreateDate = json.CreateDate != null ? (DateTime)json.CreateDate : DateTime.Now

            };

            if (offer.Id > 0)
            {
                repo.OfferRepository.Update(offer);
            }
            else
            {
                repo.OfferRepository.Create(offer);
            }

            repo.SaveChanges();

            return new
            {
                success = true,
                message = "Offer saved successfully"
            };
        }

        //müdür için
        [HttpGet("ListOffersWithRequestStatus8")]
        public IActionResult ListOffersWithRequestStatus8()
        {
            var offers = repo.OfferRepository.FindByCondition(offer => offer.Requests.RequestStatusId == 8)
                .Select(offer => new
                {
                    OfferId = offer.Id,
                    RequestId=offer.RequestId,
                    ProductName = offer.Requests.Product.ProductName, 
                    SupplierCompanyName = offer.SupplierCompany.SupplierCompanyName, 
                    offer.OfferPrice,
                    offer.Status,
                    offer.Amount,
                    offer.UnitPrice,
                    offer.CreateDate
                })
                .ToList();

            return Ok(offers);
        }

        //Yönetim için

        [HttpGet("ListOffersWithRequestStatus11")]
        public IActionResult ListOffersWithRequestStatus11()
        {
            var offers = repo.OfferRepository.FindByCondition(offer => offer.Requests.RequestStatusId == 11)
                .Select(offer => new
                {
                    OfferId = offer.Id,
                    RequestId = offer.RequestId,
                    ProductName = offer.Requests.Product.ProductName,
                    SupplierCompanyName = offer.SupplierCompany.SupplierCompanyName,
                    offer.OfferPrice,
                    offer.Status,
                    offer.Amount,
                    offer.UnitPrice,
                    offer.CreateDate
                })
                .ToList();

            return Ok(offers);
        }

        //onaylanmış teklifler muhasebe

        [HttpGet("GetApprovedOffer")]
        public IActionResult GetApprovedOffer()
        {
            var offers = repo.OfferRepository.FindByCondition(offer =>
                offer.Requests.RequestStatusId == 12 || offer.Requests.RequestStatusId == 14)
                .Select(offer => new
                {
                    OfferId = offer.Id,
                    ProductName = offer.Requests.Product.ProductName,
                    SupplierCompanyName = offer.SupplierCompany.SupplierCompanyName,
                    offer.OfferPrice,
                    offer.Status,
                    offer.Amount,
                    offer.UnitPrice,
                    offer.CreateDate
                })
                .ToList();

            return Ok(offers);
        }
    }
}
