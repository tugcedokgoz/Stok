using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Model
{
    [Table("tblUser")]
    public class User
    {
        public int Id { get; set; }
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role? Role { get; set; }

        public int? CompanyDepartmentId { get; set; }
        [ForeignKey("CompanyDepartmentId")]
        public CompanyDepartment? CompanyDepartment { get; set; }
        public int? SuperiorId { get; set; }
        [ForeignKey("SuperiorId")]
        public User? Superior { get; set; }

        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }


    }
}
