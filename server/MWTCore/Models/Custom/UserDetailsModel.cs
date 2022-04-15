using System;
using System.Collections.Generic;
using System.Text;

namespace MWTCore.Models.Custom
{
    public class UserDetailsModel
    {
        public int id { get; set; }

        public virtual int UserID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string GSTIN { get; set; }
        public string Pincode { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
