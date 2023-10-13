using Microsoft.EntityFrameworkCore;
using Stock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public class RoleUserRepository : RepositoryBase<RoleUser>
    {
        private readonly RepositoryContext _context;
        public RoleUserRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<object>> GetRoleUserAsync()
        {
            var roleUser = await _context.RoleUsers
                .Include(r => r.Role)
                .Include(r => r.User)
                .Include(r => r.Company)
                .ToListAsync();

            var roleUserDetails = roleUser.Select(roleUser => new
            {
                RoleName = roleUser.Role != null ? roleUser.Role.RoleName : null,
                UserFullName = roleUser.User != null ? roleUser.User.UserFullName : null,
                CompanyName = roleUser.Company != null ? roleUser.Company.CompanyName : null,
            });

            return roleUserDetails;
        }
        public async Task<IEnumerable<object>> GetUsersByCompanyId(int companyId)
        {
            var users = await _context.RoleUsers
                .Where(r => r.CompanyId == companyId)
                .Select(r => new
                {
                    UserFullName = r.User.UserFullName,
                    // Diğer kullanıcı bilgilerini buraya ekleyebilirsiniz
                })
                .ToListAsync();

            return users;
        }
        public async Task<RoleUser> FindByIdAsync(int roleUserId)
        {
            var roleUser=await _context.RoleUsers.FirstOrDefaultAsync(r=>r.Id == roleUserId);
            return roleUser;
        }
    }
}
