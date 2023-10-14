using Microsoft.EntityFrameworkCore;
using Stock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public class SupplierCompanyRepository : RepositoryBase<SupplierCompany>
    {
        private readonly RepositoryContext _context;
        public SupplierCompanyRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SupplierCompany>> GetSupplierAsync()
        {
            return await _context.SupplierCompanies.ToListAsync();
        }

      
    }
}
