﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Model
{
    [Table("tblRole")]
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

    }
}
