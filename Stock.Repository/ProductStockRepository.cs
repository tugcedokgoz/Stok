using Microsoft.EntityFrameworkCore;
using Stock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public class ProductStockRepository : RepositoryBase<ProductStock>
    {
        private readonly RepositoryContext _context;
        public ProductStockRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<object>> GetProductStockAsync()
        {
            var productStock = await _context.ProductStocks
                .Include(s => s.Category)
                .Include(s => s.Product)
                .ToListAsync();
            var productStockDetails = productStock.Select(productStock => new
            {
                productStock.Id,
                productStock.CategoryId,
                productStock.ProductId,
                CategoryName = productStock.Category != null ? productStock.Category.CategoryName : null,
                ProductName = productStock.Product != null ? productStock.Product.ProductName : null,
            });
            return productStockDetails;
        }
        public async Task<ProductStock> FindByIdAsync(int productStockId)
        {
            var productStock = await _context.ProductStocks.FirstOrDefaultAsync(p => p.Id == productStockId);
            return productStock;
        }
    }
}
