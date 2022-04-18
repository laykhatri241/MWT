using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MWTDbContext.Models
{
    public class BusinessDetailsMaster
    {
        [Key]
        public int id{ get; set; }

        [ForeignKey("users")]
        public virtual int UserID { get; set; }

        [Required,MaxLength(16)]
        public string GSTIN { get; set; }
        [Required,MaxLength(11)]
        public string PAN { get; set; }

        [Required]
        public DateTime updatedAt { get; set; }
        [Required]
        public DateTime createdAt { get; set; }

    }
}
