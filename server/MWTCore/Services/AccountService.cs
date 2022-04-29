using MWTCore.Models.Custom;
using MWTCore.Repository.Interfaces;
using MWTCore.Services.Interfaces;
using MWTDbContext.Models;
using MWTWebApi.Model.Custom;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MWTCore.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        #region ComputeSHA265
        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        #endregion
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<int> AddAddress(AddressMaster address)
        => await _accountRepository.AddAddress(address);

        public async Task<bool> checkUsername(string username)
        => await _accountRepository.RetrieveUsername(username);

        public async Task<int> CreateUser(User usr)
        {
            usr.Password = ComputeSha256Hash(usr.Password);
            return await _accountRepository.InsertUser(usr);
        }

        public async Task<User> FetchUser(int id)
        => await _accountRepository.RetriveUser(id);

        public async Task<bool> UpdatePassword(ChangePassword changePassword)
        {
            changePassword.OldPass = ComputeSha256Hash(changePassword.OldPass);
            changePassword.NewPass= ComputeSha256Hash(changePassword.NewPass);
            return await _accountRepository.UpdatePassword(changePassword);
        }

        public async Task<bool> UpdateUser(UpdateUser user)
        => await _accountRepository.UpdateUser(user);

        public async Task<bool> UpdateAddress(AddressModel address)
        => await _accountRepository.UpdateAddress(address);

        public async Task<User> UserExists(string Username, string Password)
        =>
        await _accountRepository.IsUser(Username, ComputeSha256Hash( Password));
        
        public async Task<bool> CheckOldPassword(string oldPassword , int id)
        =>
        (await _accountRepository.RetriveUser(id)).Password.Equals(ComputeSha256Hash(oldPassword));

        public async Task<List<AddressMaster>> GetAddresses(int id)
        => await _accountRepository.GetAddresses(id);

        public async Task<AddressMaster> GetAddress(int id)
        => 
        await _accountRepository.GetAddress(id);

        public async Task<bool> RemoveAddress(int id)
        =>await _accountRepository.DeleteAddress(id);

        public async Task<int> CreateBusinessDetails(BusinessDetailsMaster businessDetails)
        => 
        await _accountRepository.CreateBusinessDetails(businessDetails);
        

        public async Task<bool> BusinessDetailsExist(int id)
        => 
        await _accountRepository.IsBusinessDetail(id);
        

        public async Task<bool> DeleteBusinessDetails(int id)
        =>await _accountRepository.DeleteBusinessDetail(id);


        public async Task<BusinessDetailsMaster> GetBusinessDetails(int id)
        => await _accountRepository.RetrieveBusinessDetail(id);

        public async Task<bool> UpdateBusinessDetail(BusinessDetailModel businessDetail)
        => await _accountRepository.UpdateBusinessDetail(businessDetail);

        public async Task<bool> isInit()
        {
            return await _accountRepository.isInit();
        }

        public async Task<bool> Init()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Role = 1,
                    Fullname = "Admin",
                    Username = "Admin",
                    Password = ComputeSha256Hash("Admin"),
                    isActive = true,
                },
                new User()
                {
                    Role = 2,
                    Fullname = "Company",
                    Username = "Company",
                    Password = ComputeSha256Hash("Company"),
                    isActive = true,
                },new User()
                {
                    Role = 3,
                    Fullname = "User",
                    Username = "User",
                    Password = ComputeSha256Hash("User"),
                    isActive = true,
                },
            };

            int userCount = 0;
            foreach(var user in users)
            {
                if(await CreateUser(user)>0)
                {
                    userCount++;
                }
            }

            if(userCount == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
