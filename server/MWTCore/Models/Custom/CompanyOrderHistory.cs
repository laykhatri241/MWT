using MWTDbContext.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWTCore.Models.Custom
{
    public class CompanyOrderHistory
    {
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int OfferID { get; set; }
        public int OrderID { get; set; }
        public int Count { get; set; }
    }
}
