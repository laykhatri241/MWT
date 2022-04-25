using System;
using System.Collections.Generic;
using System.Text;

namespace MWTCore.Models.Custom
{
    public class ProductModel
    {
        public int id { get; set; }
        public int SellerID { get; set; }
        public string ProdCompanyName { get; set; }
        public string ProdName { get; set; }
        public string ProdDetails { get; set; }

        public string ProdImage { get; set; }
        public int ProdPrice { get; set; }
        public int CategoryID { get; set; }
    }

}
