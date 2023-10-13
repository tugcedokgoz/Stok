using Microsoft.EntityFrameworkCore;
using Stock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public class UserRepository:RepositoryBase<User>
    {
        private readonly RepositoryContext _context;
        public UserRepository(RepositoryContext context):base(context) 
        {
            _context = context;
            
        }
        public async Task<IEnumerable<object>> GetUsersAsync()
        {
            var userDetails = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.CompanyDepartment)  
                .Select(user => new
                {
                    user.Id,
                    user.UserFullName,
                    user.UserEmail,
                    user.Password,
                    user.SuperiorId,
                    RoleName = user.Role != null ? user.Role.RoleName : null,
                    UnitName = user.CompanyDepartment != null ? user.CompanyDepartment.DepartmentName : null,
                    CompanyName = user.CompanyDepartment != null ? user.CompanyDepartment.Company.CompanyName : null, 
                    SuperiorName = user.SuperiorId != null ? user.Superior.UserFullName : "-"
                })
                .ToListAsync();

            return userDetails;
        }

        public async Task<User>FindByIdAsync(int userId)
        {
            var user=await _context.Users.FirstOrDefaultAsync(u=>u.Id==userId);
            return user;
        }


    }
}
