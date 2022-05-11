using MWTDbContext.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWTCore.Models.Custom
{
    public class CartCheckout
    {
        public List<ProductMaster> Products { get; set; }
        public List<StockMaster> StockInfo { get; set; }
        public List<int> ProductCounts { get; set; }
        public List<OfferMaster> Offers { get; set; }
        public int TotalCost{ get; set; }
        public int TotalItems { get; set; }
    }
}
