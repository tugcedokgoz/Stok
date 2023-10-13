using Microsoft.EntityFrameworkCore;
using Stock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public class CompanyRepository:RepositoryBase<Company>
    {
        private readonly RepositoryContext _context;
        public CompanyRepository(RepositoryContext context):base(context)
        {
            _context = context;
        }


		public async Task<IEnumerable<object>> GetCompanyAsync()
		{
			var companies = await _context.Companies.ToListAsync();

			var company = companies.Select(company => new
			{
				company.Id,
				company.CompanyName,
				company.SuperiorId,
				
				SuperiorName = company.SuperiorId != null ? GetCompnanySuperiorName(company.SuperiorId) : null

			});

			return company;
		}
		private string GetCompnanySuperiorName(int? superiorId)
		{
			if (superiorId.HasValue)
			{
				var superiorUser = _context.Companies.SingleOrDefault(u => u.Id == superiorId);
				if (superiorUser != null)
				{
					return superiorUser.CompanyName;
				}
			}
			return null;
		}

        public async Task<Company> FindByIdAsync(int companyId)
        {
            var company=await _context.Companies.FirstOrDefaultAsync(c=>c.Id==companyId);
            return company;
        }

        public Company GetCompanyByName(string companyName)
        {
            return FindByCondition(c => c.CompanyName == companyName)
                .FirstOrDefault();
        }

    }
}
