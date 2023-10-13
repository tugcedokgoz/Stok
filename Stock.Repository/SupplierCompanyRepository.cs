using Stock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public class SupplierCompanyRepository:RepositoryBase<SupplierCompany>
    {
        public SupplierCompanyRepository(RepositoryContext _context):base(_context) 
        {
            
        }
    }
}
