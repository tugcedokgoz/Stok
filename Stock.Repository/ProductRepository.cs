using Microsoft.EntityFrameworkCore;
using Stock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public class ProductRepository : RepositoryBase<Product>
    {
        private readonly RepositoryContext _context;
        public ProductRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<object>> GetProductsAsync()
        {
            var products = await _context.Products
                .Include(u => u.Category)
                .ToListAsync();

            var productDetails = products.Select(product => new
            {
                product.Id,
                product.ProductName,
                product.CategoryId,
                CategoryName = product.Category != null ? product.Category.CategoryName : null,
              
            });

            return productDetails;
        }

        public async Task<Product> FindBxyIdAsync(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            return product;
        }

        public async Task<IEnumerable<object>> GetProductByCatgeoryIdAsync(int categoryId)
        {
            var products = await _context.Products
                .Where(cd => cd.CategoryId == categoryId)
                .Include(cd => cd.Category)
                .ToListAsync();

            var productDetails = products.Select(products => new
            {
                products.Id,
                products.ProductName,
                CompanyName = products.Category != null ? products.Category.CategoryName : null,
            });

            return productDetails;
        }
    }
}
