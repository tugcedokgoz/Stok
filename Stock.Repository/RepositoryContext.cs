using Microsoft.EntityFrameworkCore;
using Stock.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Repository
{
    public class RepositoryContext:DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Requests>().Property(d => d.RequestDate).HasDefaultValue();

        //}

        public DbSet<Category>Categories { get; set; }
        public DbSet<CompanyDepartment> CompanyDepartments { get; set; }
        public DbSet<Product>Products { get; set; }
        public DbSet<Report>Reports{ get; set; }
        public DbSet<Requests>Requests{ get; set; }
        public DbSet<Role>Roles{ get; set; }
        public DbSet<RoleUser>RoleUsers{ get; set; }
        public DbSet<ProductStock>ProductStocks { get; set; }
        public DbSet<SupplierCompany>SupplierCompanies { get; set; }
        public DbSet<User>Users { get; set; }
        public DbSet<DepartmentProductStock>DepartmentProductStocks { get; set;}
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Company>Companies { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<CompanyUser> CompanyUsers { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<SupplierProduct>SupplierProducts { get; set; }


     

    }
}
