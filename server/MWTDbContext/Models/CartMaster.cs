using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MWTDbContext.Models
{
    public class CartMaster
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("users")]
        public int UserID { get; set; }

        [Required]
        public int CartID { get; set; }

        public int OrderID { get; set; }
        [Required]
        public bool isPaid { get; set; }

        [Required]
        public DateTime createdAt { get; set; }
    }
}
