using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MWTWebApi.Model.Custom
{
    public class UserModel
    {
        
        public int id { get; set; }
        
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Avatar { get; set; }
        public virtual int Role { get; set; }

        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public bool isActive { get; set; }
    }
}
