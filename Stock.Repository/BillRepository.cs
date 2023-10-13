using Microsoft.EntityFrameworkCore;
using Stock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public class BillRepository : RepositoryBase<Bill>
    {
        private readonly RepositoryContext _context;
        public BillRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<object>> GetBillAsync()
        {
            var bill = await _context.Bills
                .Include(b => b.Category)
                .Include(b => b.Product)
                .ToListAsync();
            var billDetails = bill.Select(bill => new
            {
                bill.Id,
                bill.Price,
                bill.Amount,
                bill.CreateDate,
                CategoryName = bill.Category != null ? bill.Category.CategoryName : null,
                ProductName = bill.Product != null ? bill.Product.ProductName : null,
            });
            return billDetails;
        }

        public async Task<Bill> FindByIdAsync(int billId)
        {
            var bill = await _context.Bills.FirstOrDefaultAsync(b => b.Id == billId);
            return bill;
        }
    }
}
