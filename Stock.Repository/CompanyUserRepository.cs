using Microsoft.EntityFrameworkCore;
using Stock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public class CompanyUserRepository:RepositoryBase<CompanyUser>
    {
        private readonly RepositoryContext _context;
        public CompanyUserRepository(RepositoryContext context) : base(context)
        {

            _context = context;

        }
        public async Task<IEnumerable<object>> GetCompanyUserAsync()
        {
            var companyUser = await _context.CompanyUsers
                .Include(b => b.Company)
                .Include(b => b.CompanyDepartment)
                .Include(b => b.User)
                .Include(b => b.Role)
                .ToListAsync();
            var companyUserDetails = companyUser.Select(companyUser => new
            {
                companyUser.Id,
                CompanyName = companyUser.Company != null ? companyUser.Company.CompanyName : null,
                DepartmanName = companyUser.CompanyDepartment != null ? companyUser.CompanyDepartment.DepartmentName : null,
                UserFullName = companyUser.User != null ? companyUser.User.UserFullName : null,
                UserEmail = companyUser.User != null ? companyUser.User.UserEmail : null,
                Password = companyUser.User != null ? companyUser.User.Password : null,
                RoleName = companyUser.Role != null ? companyUser.Role.RoleName : null,
                SuperiorName = companyUser.SuperiorId != null ? _context.Users.FirstOrDefault(u => u.Id == companyUser.SuperiorId)?.UserFullName : null


            });
            return companyUserDetails;
        }

        //public async Task SaveCompanyUserAsync(CompanyUser model)
        //{
        //    if (model.UserId == 0)
        //    {
        //        // Yeni bir kullanıcı eklemek istiyoruz
        //        var newUser = new User
        //        {
        //            UserFullName = model.User.UserFullName,
        //            UserEmail = model.User.UserEmail,
        //            Password = model.User.Password,
        //            RoleId = model.RoleId,
        //            SuperiorId = model.SuperiorId
        //        };

        //        // Yeni kullanıcıyı ekleyin
        //        _context.Users.Add(newUser);

        //        var newCompanyUser = new CompanyUser
        //        {
        //            CompanyId = model.CompanyId,
        //            DepartmentId = model.DepartmentId,
        //            UserId = newUser.Id, // Yeni kullanıcının Id'sini alın
        //            RoleId = model.RoleId
        //        };

        //        // Yeni CompanyUser'ı ekleyin
        //        _context.CompanyUsers.Add(newCompanyUser);
        //    }
        //    else
        //    {
        //        // Varolan bir kullanıcıyı güncellemek istiyoruz
        //        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.UserId);

        //        if (existingUser == null)
        //        {
        //            throw new InvalidOperationException("Kullanıcı bulunamadı.");
        //        }

        //        existingUser.UserFullName = model.User.UserFullName;
        //        existingUser.UserEmail = model.User.UserEmail;
        //        existingUser.Password = model.User.Password;
        //        existingUser.RoleId = model.RoleId;
        //        existingUser.SuperiorId = model.SuperiorId;

        //        // Varolan kullanıcıyı güncelleyin
        //        _context.Users.Update(existingUser);

        //        var existingCompanyUser = await _context.CompanyUsers.FirstOrDefaultAsync(cu => cu.UserId == model.UserId);

        //        if (existingCompanyUser == null)
        //        {
        //            throw new InvalidOperationException("CompanyUser bulunamadı.");
        //        }

        //        existingCompanyUser.CompanyId = model.CompanyId;
        //        existingCompanyUser.DepartmentId = model.de;
        //        existingCompanyUser.RoleId = model.RoleId;

        //        // Varolan CompanyUser'ı güncelleyin
        //        _context.CompanyUsers.Update(existingCompanyUser);
        //    }

        //    await _context.SaveChangesAsync();
        //}


    }
}
