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
        =>
            _accountRepository = accountRepository;


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
            {
            return await _accountRepository.IsUser(Username, ComputeSha256Hash( Password));
        }

        public async Task<bool> CheckOldPassword(string oldPassword , int id)
        {
            var _user = await _accountRepository.RetriveUser(id);

            if(_user.Password.Equals(ComputeSha256Hash(oldPassword)))
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
