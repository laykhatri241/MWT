using MWTDbContext.Models;
using MWTWebApi.Model.Custom;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MWTCore.Repository.Interfaces
{
    public interface IAccountRepository
    {
        public Task<int> InsertUser(User usr);

        public Task<bool> RetrieveUsername(string username);

        public Task<User> IsUser(string Username, string Password);

        public Task<int> InsertUserDetails(DetailsMaster details);

        public Task<User> RetriveUser(int id);

        public Task<bool> UpdatePassword(ChangePassword changePassword);

        public Task<bool> UpdateUser(User usr);

    }
}
