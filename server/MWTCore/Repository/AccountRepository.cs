using Microsoft.EntityFrameworkCore;
using MWTCore.Models.Custom;
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
                usr.createdAt = DateTime.Now;
                usr.updatedAt = DateTime.Now;
                usr.isActive = true;
                _context.users.Add(usr);
                return await _context.SaveChangesAsync();
        }

        public async Task<int> AddAddress(AddressMaster address)
        {
            address.createdAt = DateTime.Now;
            address.updatedAt = DateTime.Now;
            _context.addressMasters.Add(address);
            return await _context.SaveChangesAsync();
        }

        public async Task<User> IsUser(string Username, string Password)
        {
            return await (_context.users.AsNoTracking().FirstOrDefaultAsync(u => u.Username.Equals(Username) && u.Password.Equals(Password) && u.isActive==true));
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

        public async Task<bool> UpdateUser(UpdateUser user)
        {
            var _user = await _context.users.FirstAsync(us => us.id == user.id);

            _user.Fullname = user.Fullname;
            _user.updatedAt = DateTime.Now;
            _user.DateOfBirth = user.DateOfBirth;
            _user.Avatar = user.Avatar;

            return await _context.SaveChangesAsync() == 1? true : false;
        }

        public async Task<bool> UpdateAddress(AddressModel address)
        {
            var _Address = await _context.addressMasters.FirstAsync(dm => dm.id == address.id);

            
            _Address.Address1 = address.Address1;
            _Address.Address2 = address.Address2;
            _Address.Pincode = address.Pincode;
            _Address.updatedAt = DateTime.Now;

            return await _context.SaveChangesAsync() == 1 ? true : false;
        }
    }
}
