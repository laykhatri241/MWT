using System;
using System.Collections.Generic;
using System.Text;

namespace MWTCore.Models.Custom
{
    public class CartModel
    {
        public int id { get; set; }
        public int UserID { get; set; }
        public int CartID { get; set; }
        public int OrderID { get; set; }
        public bool isPaid { get; set; }
    }
}
