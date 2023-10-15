using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Model
{
    [Table("tblOffer")]
    public class Offer
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        [ForeignKey("RequestId")]
        public Requests Requests { get; set; }
        public decimal OfferPrice { get; set; }
        public bool? Status { get; set; }
        public int SupplierCompanyId { get; set; }
        [ForeignKey("SupplierCompanyId")]
        public SupplierCompany SupplierCompany { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
