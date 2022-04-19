using MWTCore.Models.Custom;
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

        public Task<int> AddAddress(AddressMaster address);

        public Task<User> RetriveUser(int id);

        public Task<bool> UpdatePassword(ChangePassword changePassword);

        public Task<bool> UpdateUser(UpdateUser user);

        public Task<bool> UpdateAddress(AddressModel address);

        public Task<List<AddressMaster>> GetAddresses(int id);

        public Task<AddressMaster> GetAddress(int id);

        public Task<bool> DeleteAddress(int id);

        public Task<int> CreateBusinessDetails(BusinessDetailsMaster businessDetails);

        public Task<bool> IsBusinessDetail(int id);

        public Task<bool> DeleteBusinessDetail(int id);

        public Task<BusinessDetailsMaster> RetrieveBusinessDetail(int id);

        public Task<bool> UpdateBusinessDetail(BusinessDetailModel businessDetail);
    }
}
