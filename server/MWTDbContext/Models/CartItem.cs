using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MWTDbContext.Models
{
    public class CartItem
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("productMasters")]
        public int ProductID { get; set; }
        [ForeignKey("cartMasters")]
        public int CartID { get; set; }

        [Required]
        public DateTime createdAt { get; set; }
    }
}
