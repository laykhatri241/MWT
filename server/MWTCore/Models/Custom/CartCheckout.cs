using System;
using System.Collections.Generic;
using System.Text;

namespace MWTCore.Models.Custom
{
    public class CartCheckout
    {
        public List<ProductModel> Products { get; set; }
        public List<StockModel> StockInfo { get; set; }
        public List<int> ProductCounts { get; set; }
        public int TotalCost{ get; set; }
        public int TotalItems { get; set; }
    }
}
