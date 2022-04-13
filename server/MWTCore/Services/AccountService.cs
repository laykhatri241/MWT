using MWTCore.Repository.Interfaces;
using MWTCore.Services.Interfaces;
using MWTDbContext.Models;
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
        {
            _accountRepository = accountRepository;
        }

        public async Task<int> CreateUser(User usr)
        {
            return await _accountRepository.InsertUser(usr);
        }

      

        public bool UserExists(string Username, string Password)
        {
            return _accountRepository.IsUser(Username, Password);
        }
    }
}
