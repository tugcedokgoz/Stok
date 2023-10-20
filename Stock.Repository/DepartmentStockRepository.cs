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
    public class DepartmentStockRepository : RepositoryBase<DepartmentProductStock>
    {
        private readonly RepositoryContext _context;
        public DepartmentStockRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<object>> GetDepartmentStockAsync()
        {

            var departmentStock = await _context.DepartmentProductStocks
            .Include(ds => ds.CompanyDepartment.Company)
            .Include(ds => ds.Product.Category)
            .ToListAsync();

            var departmentStockDetails = departmentStock.Select(ds => new
            {
                ds.Id,
                ds.Amount,
                CompanyName = ds.CompanyDepartment.Company != null ? ds.CompanyDepartment.Company.CompanyName : null,
                CategoryName = ds.Product.Category != null ? ds.Product.Category.CategoryName : null,
                DepartmentName = ds.CompanyDepartment.DepartmentName,
                ProductName = ds.Product.ProductName
            });
            return departmentStockDetails;
        }

        public async Task<DepartmentProductStock> FindByIdAsync(int departmentStockId)
        {
            var departmentStock = await _context.DepartmentProductStocks.FirstOrDefaultAsync(d => d.Id == departmentStockId);
            return departmentStock;
        }
        public async Task<IEnumerable<object>> GetDepartmentStockByDepartmentIdAsync(int departmentId)
        {
            var departmentStock = await _context.DepartmentProductStocks
                .Include(ds => ds.CompanyDepartment.Company)
                .Include(ds => ds.Product.Category)
                .Where(ds => ds.DepartmentId == departmentId)
                .ToListAsync();

            var departmentStockDetails = departmentStock.Select(ds => new
            {
                ds.Id,
                ds.Amount,
                CompanyName = ds.CompanyDepartment.Company != null ? ds.CompanyDepartment.Company.CompanyName : null,
                CategoryName = ds.Product.Category != null ? ds.Product.Category.CategoryName : null,
                DepartmentName = ds.CompanyDepartment.DepartmentName,
                ProductName = ds.Product.ProductName
            });

            return departmentStockDetails;
        }
    }

}

