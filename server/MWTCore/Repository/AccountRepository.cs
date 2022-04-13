using Microsoft.EntityFrameworkCore;
using MWTCore.Repository.Interfaces;
using MWTDbContext;
using MWTDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWTCore.Repository
{
    public class AccountRepository: IAccountRepository
    {
        private readonly StoreAppDbCon _context;
        public AccountRepository(StoreAppDbCon context)
        {
            _context = context;
        }

      

        public async Task<int> InsertUser(User usr)
        {
            _context.users.Add(usr);
            return await _context.SaveChangesAsync();

        }

        public bool IsUser(string Username, string Password)
        {
            return (_context.users.FirstOrDefaultAsync(u => u.Username.Equals(Username) && u.Password.Equals(Password)) != null);
        }
    }
}
