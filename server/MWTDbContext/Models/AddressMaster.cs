using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MWTDbContext.Models
{
    public class AddressMaster
    {
        [Key]
        public int id { get; set; }

        [ForeignKey("User")]
        public virtual int UserID { get; set; }

        
        [Required,MaxLength(250)]
        public string Address1 { get; set; }

        [MaxLength(250)]
        public string Address2 { get; set; }
        
        [Required,MaxLength(33)]
        public string Pincode { get; set; }
        [Required]
        public DateTime createdAt { get; set; }
        [Required]
        public DateTime updatedAt { get; set; }

    }
}
