using Microsoft.EntityFrameworkCore;
using Stock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public class SupplierProductRepository:RepositoryBase<SupplierProduct>
    {
        private readonly RepositoryContext _context;
        public SupplierProductRepository(RepositoryContext context):base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<object>> GetSupplierProductAsync()
        {
            var supplierProduct = await _context.SupplierProducts
               .Include(r => r.Product)
               .Include(r => r.Product.Category)
               .Include(r => r.SupplierCompany)

               .ToListAsync();

            var supplierProductDetails = supplierProduct.Select(supplierProduct => new
            {
                supplierProduct.Id,
                supplierProduct.Amount,
                supplierProduct.Price,
                SupplierCompanyName = supplierProduct.SupplierCompany != null ? supplierProduct.SupplierCompany.SupplierCompanyName : null,
                ProductName = supplierProduct.Product != null ? supplierProduct.Product.ProductName : null,
                CategoryName = supplierProduct.Product != null ? supplierProduct.Product.Category.CategoryName : null,

            });
            return supplierProduct;
        }
    }
}
