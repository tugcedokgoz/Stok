using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Model
{
    [Table("tblCompany")]
    public class Company
    {
   
        public int Id { get; set; }
        public string CompanyName { get; set; }
		public int? SuperiorId { get; set; }
		[ForeignKey("SuperiorId")]
		public Company Superior { get; set; }

        public ICollection<CompanyDepartment> CompanyDepartments { get; set; }
    }
}
