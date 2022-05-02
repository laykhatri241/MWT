using MWTCore.Models.Custom;
using MWTDbContext.Models;
using MWTWebApi.Model.Custom;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MWTCore.Services.Interfaces
{
    public interface IAccountService
    {

        

        public Task<int> CreateUser(User usr);

        public Task<bool> checkUsername(string username);

        public Task<User> UserExists(string Username, string Password);

        public Task<bool> CheckOldPassword(string oldPassword, int id);

        public Task<int> AddAddress(AddressMaster details);

        public Task<User> FetchUser(int id);

        public Task<bool> UpdatePassword(ChangePassword changePassword);

        public Task<bool> UpdateUser(UpdateUser user);

        public Task<bool> UpdateAddress(AddressModel details);

        

        public Task<List<AddressMaster>> GetAddresses(int id);

        public Task<AddressMaster> GetAddress(int id);
        public Task<bool> RemoveAddress(int id);

        public Task<int> CreateBusinessDetails(BusinessDetailsMaster businessDetails);

        public Task<bool> BusinessDetailsExist(int id);

        public Task<bool> DeleteBusinessDetails(int id);

        public Task<BusinessDetailsMaster> GetBusinessDetails(int id);

        public Task<bool> UpdateBusinessDetail(BusinessDetailModel businessDetail);
    }
}
