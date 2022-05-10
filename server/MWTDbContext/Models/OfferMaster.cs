using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MWTDbContext.Models
{
    public class OfferMaster
    {
        [Key]
        public int id{ get; set; }

        [ForeignKey("productMasters")]
        public int ProductID { get; set; }

        public int Offer { get; set; }
        public DateTime OfferStart { get; set; }
        public DateTime OfferEnd { get; set; }
    }
}
