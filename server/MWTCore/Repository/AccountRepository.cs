using Microsoft.EntityFrameworkCore;
using MWTCore.Repository.Interfaces;
using MWTDbContext;
using MWTDbContext.Models;
using MWTWebApi.Model.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWTCore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly StoreAppDbCon _context;
        public AccountRepository(StoreAppDbCon context)
        {
            _context = context;
        }

        public async Task<int> InsertUser(User usr)
        {
            try
            {
                usr.createdAt = DateTime.Now;
                usr.updatedAt = DateTime.Now;
                _context.users.Add(usr);
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public async Task<int> InsertUserDetails(DetailsMaster details)
        {
            try
            {

            details.createdAt = DateTime.Now;
            details.updatedAt = DateTime.Now;
            _context.detailsMasters.Add(details);
            return await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return -1;
            }

        }

        public async Task<User> IsUser(string Username, string Password)
        {
            return await (_context.users.AsNoTracking().FirstOrDefaultAsync(u => u.Username.Equals(Username) && u.Password.Equals(Password)));
        }

        public async Task<bool> RetrieveUsername(string username)
        {
            return await (_context.users.AsNoTracking().AnyAsync(usr => usr.Username.Equals(username)));
        }

        public async Task<User> RetriveUser(int id)
        {
            var user = await _context.users.AsNoTracking().Where(us=> us.id == id).FirstOrDefaultAsync();

            return user;
        }

        public async Task<bool> UpdatePassword(ChangePassword changePassword)
        {
            var _user = await _context.users.Where(usr => usr.id == changePassword.id && usr.Password.Equals(changePassword.OldPass)).FirstOrDefaultAsync();
            _user.updatedAt = DateTime.Now;
            _user.Password = changePassword.NewPass;
            return await _context.SaveChangesAsync() ==1 ?true:false;
            
        }

        public async Task<bool> UpdateUser(User usr)
        {
            var _user = await _context.users.FirstAsync(us => us.id == usr.id);

            _user.Fullname = usr.Fullname;
            _user.Avatar = usr.Avatar;
            _user.updatedAt = DateTime.Now;
            _user.Username = usr.Username;

            return await _context.SaveChangesAsync() == 1? true : false;
        }
    }
}
