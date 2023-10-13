using Stock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public class OfferRepository:RepositoryBase<Offer>
    {
        private readonly RepositoryContext _context;
        public OfferRepository(RepositoryContext context):base(context)
        {

            _context = context;

        }
    }
}
