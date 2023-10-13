using Microsoft.EntityFrameworkCore;
using Stock.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public class DepartmentStockRepository:RepositoryBase<DepartmentProductStock>
    {
        private readonly RepositoryContext _context;
        public DepartmentStockRepository(RepositoryContext context):base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<object>> GetDepartmentStockAsync()
        {
            var departmentStock = await _context.DepartmentProductStocks
                .Include(d => d.CompanyDepartment)
                .Include(d => d.Product)
                .ToListAsync();
            var departmentStockDeatils = departmentStock.Select(departmentStock => new
            {
                departmentStock.Id,
                departmentStock.Amount,
                DepartmentName = departmentStock.CompanyDepartment != null ? departmentStock.CompanyDepartment.DepartmentName : null,
                ProductName = departmentStock.Product != null ? departmentStock.Product.ProductName : null,

            });
            return departmentStockDeatils;
        }

        public async Task<DepartmentProductStock>FindByIdAsync(int departmentStockId)
        {
            var departmentStock = await _context.DepartmentProductStocks.FirstOrDefaultAsync(d => d.Id == departmentStockId);
            return departmentStock;
        }
    }
}
