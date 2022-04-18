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
            await _context.SaveChangesAsync();
            return usr.id;
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
            return await (_context.users.AsNoTracking().FirstOrDefaultAsync(u => u.Username.Equals(Username) && u.Password.Equals(Password) && u.isActive == true));
        }

        public async Task<bool> RetrieveUsername(string username)
        {
            return await (_context.users.AsNoTracking().AnyAsync(usr => usr.Username.Equals(username)));
        }

        public async Task<User> RetriveUser(int id)
        {
            var user = await _context.users.AsNoTracking().Where(us => us.id == id).FirstOrDefaultAsync();
            
            return user;
        }

        public async Task<bool> UpdatePassword(ChangePassword changePassword)
        {
            var _user = await _context.users.Where(usr => usr.id == changePassword.id && usr.Password.Equals(changePassword.OldPass)).FirstOrDefaultAsync();
            _user.updatedAt = DateTime.Now;
            _user.Password = changePassword.NewPass;
            return await _context.SaveChangesAsync() == 1 ? true : false;

        }

        public async Task<bool> UpdateUser(UpdateUser user)
        {
            var _user = await _context.users.FirstAsync(us => us.id == user.id);

            _user.Fullname = user.Fullname;
            _user.updatedAt = DateTime.Now;
            _user.DateOfBirth = user.DateOfBirth;
            _user.Avatar = user.Avatar;

            return await _context.SaveChangesAsync() == 1 ? true : false;
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

        public async Task<List<AddressMaster>> GetAddresses(int id)
        {
            var _Addresses = await _context.addressMasters.AsNoTracking().Where(ad => ad.UserID == id).ToListAsync();

            return _Addresses;
        }

        public async Task<AddressMaster> GetAddress(int id)
        {
            var _address = await _context.addressMasters.AsNoTracking().FirstOrDefaultAsync(ad => ad.id == id);
            return _address;
        }
        public async Task<bool> DeleteAddress(int id)
        {
            var address = await _context.addressMasters.Where(ad => ad.id == id).FirstOrDefaultAsync();
            if(address != null)
            {
             _context.addressMasters.Remove(address);
                return await _context.SaveChangesAsync() == 1 ? true : false;
            }
            return false;
        }
        public async Task<int> CreateBusinessDetails(BusinessDetailsMaster businessDetails)
        {
            businessDetails.updatedAt = DateTime.Now;
            businessDetails.createdAt = DateTime.Now;
            var status = await _context.businessDetailsMasters.AddAsync(businessDetails);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> IsBusinessDetail(int id)
        {
            var business = await _context.businessDetailsMasters.AsNoTracking().Where(bs => bs.UserID == id).ToListAsync();
            return business.Count() == 1 ? true : false;
        }

        public async Task<bool> DeleteBusinessDetail(int id)
        {
            var detail = await _context.businessDetailsMasters.Where(dt => dt.id == id).FirstOrDefaultAsync();

            if(detail != null)
            {
                _context.businessDetailsMasters.Remove(detail);
                return await _context.SaveChangesAsync() == 1 ? true : false;
            }
            return false;
        }

        public async Task<BusinessDetailsMaster> RetrieveBusinessDetail(int id)
        {
            var detail = await _context.businessDetailsMasters.Where(dt => dt.UserID == id).FirstOrDefaultAsync();

            if(detail != null)
            {
                return detail;
            }
            return null;
        }

        public async Task<bool> UpdateBusinessDetail(BusinessDetailModel businessDetail)
        {
            var detail = await _context.businessDetailsMasters.FirstOrDefaultAsync(dt => dt.UserID == businessDetail.UserID && dt.id == businessDetail.id);

            detail.PAN = businessDetail.PAN;
            detail.GSTIN = businessDetail.GSTIN;
            detail.updatedAt = DateTime.Now;

            return await _context.SaveChangesAsync() == 1 ? true : false;
        }
    }
}
