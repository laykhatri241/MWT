using System;
using System.Collections.Generic;
using System.Text;

namespace MWTCore.Models.Custom
{
    public class AddressModel
    {
        public int id { get; set; }

        public int UserID { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }
        
        public string Pincode { get; set; }
    }
}
