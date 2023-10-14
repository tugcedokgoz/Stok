using Microsoft.EntityFrameworkCore;
using Stock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public class ReportRepository : RepositoryBase<Report>
    {
        private readonly RepositoryContext _context;
        public ReportRepository(RepositoryContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<object>> GetReportsAsync()
        {
            var reports = await _context.Reports
                .Include(r => r.User)
                .Include(r => r.CompanyDepartment)
                .Include(r => r.Product)
                .Include(r => r.SupplierCompany)

                .ToListAsync();

            var reportDetails = reports.Select(report => new
            {
                report.Id,
                report.UserId,
                report.UnitId,
                report.ProductId,
                report.SupplierCompanyId,
                UserFullName = report.User != null ? report.User.UserFullName : null,
                UserEmail = report.User != null ? report.User.UserEmail : null,
                UnitName = report.CompanyDepartment != null ? report.CompanyDepartment.DepartmentName : null,
                ProductName = report.Product != null ? report.Product.ProductName : null,
                SupplierCompanyName = report.Product != null ? report.SupplierCompany.SupplierCompanyName : null,
               


            });

            return reportDetails;
        }
        public async Task<Report> FindByIdAsync(int reportId)
        {
            var report = await _context.Reports.FirstOrDefaultAsync(r => r.Id == reportId);
            return report;
        }
    }
}
