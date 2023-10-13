using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Model
{
    [Table("tblRequest")]
    public class Requests
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int Amount { get; set; }
        public bool? Status { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public int? RequestStatusId { get; set; }
        [ForeignKey("RequestStatusId")]
        public RequestStatus? RequestStatus { get; set; }
        public int? UserSuperiorId { get; set; }
    }
}
