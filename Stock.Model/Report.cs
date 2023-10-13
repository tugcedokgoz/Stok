using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Model
{
    [Table("tblReport")]
    public class Report
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UnitId { get; set; }
        [ForeignKey("UnitId")]
        public CompanyDepartment CompanyDepartment { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int SupplierCompanyId { get; set; }
        [ForeignKey("SupplierCompanyId")]
        public SupplierCompany SupplierCompany { get; set; }
    }
}
