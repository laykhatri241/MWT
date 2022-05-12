using MWTDbContext.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWTCore.Models.Custom
{
    public class CompanyHistory
    {
        public User user { get; set; }
        public ProductMaster product { get; set; }
        public OfferMaster offer { get; set; }
        public CartItem cartItem { get; set; }
        public CartMaster cart { get; set; }
    }
}
