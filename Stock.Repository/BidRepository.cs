using Microsoft.EntityFrameworkCore;
using Stock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public class BidRepository : RepositoryBase<Bid>
    {
        private readonly RepositoryContext _context;
        public BidRepository(RepositoryContext context) : base(context)
        {

            _context = context;

        }
        public async Task<IEnumerable<object>> GetBidsAsync()
        {
            var bids = await _context.Bids
                .Include(b => b.Product)
                .Include(b => b.SupplierCompany)
                .ToListAsync();
            var bidsDetails =bids.Select(bid => new
            {
                bid.Id,
                bid.Price,
                ProductName = bid.Product != null ? bid.Product.ProductName : null,
                SupplierCompanyName = bid.SupplierCompany != null ? bid.SupplierCompany.SupplierCompanyName : null,

            });
            return bidsDetails;
        }
        public void Add(Bid bid)
        {
            _context.Bids.Add(bid);
        }
        public async Task<Bid> FindByIdAsync(int bidId)
        {
            var bid = await _context.Bids.FirstOrDefaultAsync(b => b.Id == bidId);
            return bid;
        }
    }
}
