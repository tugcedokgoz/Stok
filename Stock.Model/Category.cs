using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Model
{
    [Table("tblCategory")]
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }
}
