using Microsoft.EntityFrameworkCore;
using Stock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public class RoleRepository:RepositoryBase<Role>
    {
        private readonly RepositoryContext _context; 
        public RoleRepository(RepositoryContext context):base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> FindByIdAsync(int roleId)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
            return role;
        }
        
        //public Role GetRoleByName(string roleName)
        //{
        //    return FindByCondition(r => r.RoleName == roleName)
        //        .FirstOrDefault();
        //}

    }
}
