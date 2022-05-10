using System;
using System.Collections.Generic;
using System.Text;

namespace MWTCore.Models.Custom
{
    public class OrderHistory
    {
        public int cartID { get; set; }
        public int ProductID { get; set; }
        public int OrderID { get; set; }
        public int Count { get; set; }
        public int Cost { get; set; }
        public DateTime OrderedOn { get; set; }
    }
}
