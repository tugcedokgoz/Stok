using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Model
{
    [Table("tblDepartmentProductStock")]
    public class DepartmentProductStock
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public CompanyDepartment CompanyDepartment { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int Amount { get; set; }
    }
}
