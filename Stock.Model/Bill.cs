using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Model
{
    [Table("tblBill")]
    public class Bill
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public DateTime CreateDate { get; set; }
        public int SupplierCompanyId { get; set; }
        [ForeignKey("SupplierCompanyId")]
        public SupplierCompany SupplierCompany { get; set; }
    }
}
