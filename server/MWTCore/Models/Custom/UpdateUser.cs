using System;
using System.Collections.Generic;
using System.Text;

namespace MWTCore.Models.Custom
{
    public class UpdateUser
    {
        public int id { get; set; }
        public string Fullname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Avatar { get; set; }
    }
}
