using Microsoft.EntityFrameworkCore;
using Stock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public class CategoryRepository : RepositoryBase<Category>
    {
        private readonly RepositoryContext _context;
        public CategoryRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<Category>FindByIdAsync(int categoryId)
        {
            var category=await _context.Categories.FirstOrDefaultAsync(c=>c.Id==categoryId);
            return category;
        }
    }
}
