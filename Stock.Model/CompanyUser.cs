using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Model
{
    [Table("tblCompanyUser")]
    public class CompanyUser
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }
        public int? DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public CompanyDepartment? CompanyDepartment { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public int? RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role? Role { get; set; }
        public int? SuperiorId { get; set; }
        [ForeignKey("SuperiorId")]
        public User? Superior { get; set; }


    }
}
