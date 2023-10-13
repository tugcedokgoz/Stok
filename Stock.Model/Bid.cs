using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Model
{
    [Table("tblBid")]
    public class Bid
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int SupplierCompanyId { get; set; }
        [ForeignKey("SupplierCompanyId")]
        public SupplierCompany SupplierCompany { get; set; }
    
      
    }
}
