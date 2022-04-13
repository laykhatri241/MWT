using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MWTDbContext.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; } = 0;
        [Required]
        [MaxLength(33)]
        public string Company { get; set; }
        [Required]
        [MaxLength(33)]
        public string Model { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}
