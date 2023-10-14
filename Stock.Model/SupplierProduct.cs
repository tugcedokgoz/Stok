using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Model
{

    [Table("tblSupplierProduct")]
    public class SupplierProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int SupplierCompanyId { get; set; }
        [ForeignKey("SupplierCompanyId")]
        public SupplierCompany SupplierCompany { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
    }
}
