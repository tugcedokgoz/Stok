using Microsoft.EntityFrameworkCore;
using Stock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public class CompanyDepartmentRepository:RepositoryBase<CompanyDepartment>
    {
        private readonly RepositoryContext _context;
        public CompanyDepartmentRepository(RepositoryContext context):base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetCompanyDeparmentsAsync()
        {
            var companyDepartments=await _context.CompanyDepartments
                .Include(c=>c.Company)
                .ToListAsync();
            var companyDepartmentDetails = companyDepartments.Select(companyDepartment => new
            {
                companyDepartment.Id,
                companyDepartment.DepartmentName,
                CompanyName = companyDepartment.Company != null ? companyDepartment.Company.CompanyName : null,
            });
            return companyDepartmentDetails;
        }
        public async Task<CompanyDepartment> FindByIdAsync(int companyDepartmentId)
        {
            var companyDepartment = await _context.CompanyDepartments.FirstOrDefaultAsync(c => c.Id == companyDepartmentId);
            return companyDepartment;
        }
		public async Task<IEnumerable<object>> GetCompanyDepartmentsByCompanyIdAsync(int companyId)
		{
			var companyDepartments = await _context.CompanyDepartments
				.Where(cd => cd.CompanyId == companyId)
				.Include(cd => cd.Company)
				.ToListAsync();

			var companyDepartmentDetails = companyDepartments.Select(companyDepartment => new
			{
				companyDepartment.Id,
				companyDepartment.DepartmentName,
				CompanyName = companyDepartment.Company != null ? companyDepartment.Company.CompanyName : null,
			});

			return companyDepartmentDetails;
		}

        public CompanyDepartment GetDepartmentByName(string departmentName, int companyId)
        {
            return FindByCondition(cd => cd.DepartmentName == departmentName && cd.CompanyId == companyId)
                .FirstOrDefault();
        }
    }

}


