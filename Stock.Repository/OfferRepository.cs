using Microsoft.EntityFrameworkCore;
using Stock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public class OfferRepository : RepositoryBase<Offer>
    {
        private readonly RepositoryContext _context;
        public OfferRepository(RepositoryContext context) : base(context)
        {

            _context = context;

        }
        public async Task<IEnumerable<object>> GetOffersAsync()
        {
            var offers = await _context.Offers
                .Include(u => u.Requests)
                .ToListAsync();

            var offersDetail = offers.Select(offer => new
            {
                offer.Id,
                offer.OfferPrice,
                offer.Status,
                RequestStatus = offer.Requests != null ? offer.Requests.RequestStatus : null,

            });

            return offersDetail;
        }

        //public Offer GetOfferById(int offerId)
        //{
        //    return FindByCondition(o => o.Id == offerId)
        //        .Include(o => o.Requests)
        //        .FirstOrDefault();
        //}

    }
}
