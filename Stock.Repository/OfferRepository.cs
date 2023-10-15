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
                .Include(u => u.SupplierCompany)
                .ToListAsync();

            var offersDetail = offers.Select(offer => new
            {
                offer.Id,
                offer.OfferPrice,
                offer.Status,
                offer.Amount,
                offer.UnitPrice,
                offer.CreateDate,
                RequestStatus = offer.Requests != null ? offer.Requests.RequestStatus : null,
                SupplierCompany = offer.SupplierCompany != null ? offer.SupplierCompany.SupplierCompanyName : null,

            });

            return offersDetail;
        }


    }
}
