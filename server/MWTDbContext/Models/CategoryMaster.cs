using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MWTDbContext.Models
{
    public class CategoryMaster
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string categoryName { get; set; }
    }
}
