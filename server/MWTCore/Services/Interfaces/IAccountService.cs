using MWTDbContext.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MWTCore.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<int> CreateUser(User usr);
        

        public bool UserExists(string Username, string Password);

    }
}
