using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MWTDbContext.Models
{
    public class JustANother
    {
        [Key]
        public int id { get; set; }

        public string test { get; set; }
    }
}
