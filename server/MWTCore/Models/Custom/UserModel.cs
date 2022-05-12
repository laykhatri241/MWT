using System;
using System.Collections.Generic;
using System.Text;

namespace MWTCore.Models.Custom
{
    public class UserModel
    {
        public int id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Avatar { get; set; }
        public virtual int Role { get; set; }
    }
}
