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
    public class AccountService: IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        =>
            _accountRepository = accountRepository;
        

        public async Task<int> AddUserDetails(DetailsMaster details)
        => await _accountRepository.InsertUserDetails(details);
        
        public async Task<bool> checkUsername(string username)
        => await _accountRepository.RetrieveUsername(username);
        

        public async Task<int> CreateUser(User usr)
            =>await _accountRepository.InsertUser(usr);


        public async Task<User> FetchUser(int id)
        => await _accountRepository.RetriveUser(id);

        public async Task<bool> UpdatePassword(ChangePassword changePassword)
        => await _accountRepository.UpdatePassword(changePassword);

        public async Task<bool> UpdateUser(UserModel usr)
        => await _accountRepository.UpdateUser(usr);

        public async Task<bool> UpdateUserDetails(UserDetailsModel details)
        => await _accountRepository.UpdateUserDetails(details);

        public async Task<User> UserExists(string Username, string Password)
            =>await _accountRepository.IsUser(Username, Password);
        

    }
}
