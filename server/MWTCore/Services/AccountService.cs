using MWTCore.Models.Custom;
using MWTCore.Repository.Interfaces;
using MWTCore.Services.Interfaces;
using MWTDbContext.Models;
using MWTWebApi.Model.Custom;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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


        #region CheckGSTIN
        private bool CheckSGSTIN(string gst)
        {
            var regex = "^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$";
            return Regex.Match(gst, regex).Success;

        }
        #endregion

        #region CheckPAN
        private bool CheckPAN(string pan)
        {
            var regex = "[A-Z]{5}[0-9]{4}[A-Z]{1}";
            return Regex.Match(pan, regex).Success;
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
            changePassword.NewPass = ComputeSha256Hash(changePassword.NewPass);
            return await _accountRepository.UpdatePassword(changePassword);
        }

        public async Task<bool> UpdateUser(UpdateUser user)
        => await _accountRepository.UpdateUser(user);

        public async Task<bool> UpdateAddress(AddressModel address)
        => await _accountRepository.UpdateAddress(address);

        public async Task<User> UserExists(string Username, string Password)
        =>
        await _accountRepository.IsUser(Username, ComputeSha256Hash(Password));

        public async Task<bool> CheckOldPassword(string oldPassword, int id)
        =>
        (await _accountRepository.RetriveUser(id)).Password.Equals(ComputeSha256Hash(oldPassword));

        public async Task<List<AddressMaster>> GetAddresses(int id)
        => await _accountRepository.GetAddresses(id);

        public async Task<AddressMaster> GetAddress(int id)
        =>
        await _accountRepository.GetAddress(id);

        public async Task<bool> RemoveAddress(int id)
        => await _accountRepository.DeleteAddress(id);

        public async Task<int> CreateBusinessDetails(BusinessDetailsMaster businessDetails)
        {
            if (CheckSGSTIN(businessDetails.GSTIN))
            {
                if (CheckPAN(businessDetails.PAN))
                {
                    return await _accountRepository.CreateBusinessDetails(businessDetails);
                }
                else
                {
                    return -2;
                }
            }
            else
            {
                return -1;
            }
        }


        public async Task<bool> BusinessDetailsExist(int id)
        =>
        await _accountRepository.IsBusinessDetail(id);


        public async Task<bool> DeleteBusinessDetails(int id)
        => await _accountRepository.DeleteBusinessDetail(id);


        public async Task<BusinessDetailsMaster> GetBusinessDetails(int id)
        => await _accountRepository.RetrieveBusinessDetail(id);

        public async Task<bool> UpdateBusinessDetail(BusinessDetailModel businessDetail)
        {
            if (CheckSGSTIN(businessDetail.GSTIN))
            {
                if (CheckPAN(businessDetail.PAN))
                {
                    return await _accountRepository.UpdateBusinessDetail(businessDetail);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


    }
}
