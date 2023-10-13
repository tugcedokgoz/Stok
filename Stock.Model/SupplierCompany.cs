using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Model
{
    [Table("tblSupplierCompany")]
    public class SupplierCompany
    {
        public int Id { get; set; }
        public string SupplierCompanyName { get; set; }
    }
}
