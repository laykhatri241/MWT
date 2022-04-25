using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MWTDbContext.Models
{
    public class ProductMaster
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("users")]
        public int SellerID { get; set; }
        [MaxLength(65),Required]
        public string ProdCompanyName { get; set; }
        [Required, MaxLength(129)]
        public string ProdName { get; set; }
        [Required,MaxLength(500)]
        public string ProdDetails { get; set; }

        public string ProdImage { get; set; }
        [Required]
        public int ProdPrice { get; set; }
        [ForeignKey("categoryMasters")]
        public int CategoryID { get; set; }
        [Required]
        public DateTime updatedAt { get; set; }
        [Required]
        public DateTime createdAt { get; set; }
    }
}
