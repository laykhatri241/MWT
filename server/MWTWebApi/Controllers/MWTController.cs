using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MWTCore.Services.Interfaces;
using MWTDbContext.Models;
using MWTWebApi.Model;
using MWTWebApi.Model.Custom;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MWTWebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class MWTController : ControllerBase
    {
        #region DI
        private readonly IAccountService _accountService;
        private readonly IAuthentication _authentication;

        public MWTController(IAccountService accountService, IAuthentication authentication)
        {
            _accountService = accountService;

            _authentication = authentication;
        }
        #endregion

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

        #region TestAPI
        [AllowAnonymous]
        [Route("TestAPI")]
        [HttpGet]
        public HttpAPIResponse TestWebAPI()
        {
            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject("WebAPI is working As expected"),
                StatusCode = HttpStatusCode.OK,
                Timestamp = DateTime.Now
            };
        }
        #endregion

        #region SignUp
        [AllowAnonymous]
        [Route("SignUp")]
        [HttpPost]
        public HttpAPIResponse SignUp(User usr)
        {
            if (!_accountService.checkUsername(usr.Username).Result)
            {
                usr.Password = ComputeSha256Hash(usr.Password);
                int status = _accountService.CreateUser(usr).Result;
                if (status == 1)
                {
                    return new HttpAPIResponse()
                    {
                        Content = JsonConvert.SerializeObject(1),
                        StatusCode = HttpStatusCode.OK,
                        Timestamp = DateTime.Now
                    };
                }
                else
                {
                    return new HttpAPIResponse()
                    {
                        Content = JsonConvert.SerializeObject(-1),
                        StatusCode = HttpStatusCode.OK,
                        Timestamp = DateTime.Now
                    };
                }
            }
            else
            {
                return new HttpAPIResponse()
                {
                    Content = JsonConvert.SerializeObject("UsernameExists"),
                    StatusCode = HttpStatusCode.OK,
                    Timestamp = DateTime.Now
                };
            }
        }
        #endregion

        #region CheckUsername
        [AllowAnonymous]
        [HttpPost("CheckUsername")]
        public HttpAPIResponse CheckUsername(string usrname)
        {
            var response = _accountService.checkUsername(usrname).Result;

            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(response),
                StatusCode = HttpStatusCode.OK,
                Timestamp = DateTime.Now
            };
        }
        #endregion

        #region Login
        [AllowAnonymous]
        [HttpPost("Login")]
        public HttpAPIResponse Login(UserModel usr)
        {
            var _user = _accountService.UserExists(usr.Username, ComputeSha256Hash(usr.Password)).Result;
            if (_user != null)
            {
                _user.Password = _authentication.AuthenticateData(_user.Username, _user.Role);
            }

            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(_user),
                Timestamp = DateTime.Now,
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

        #region AddUserDetails
        [Authorize(Roles = "1,2,3")]
        [HttpPost("AddUserDetails")]
        public HttpAPIResponse AddUserDetails(DetailsMaster details)
        {
            var status = _accountService.AddUserDetails(details).Result;
            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(status),
                StatusCode = HttpStatusCode.OK,
                Timestamp = DateTime.Now
            };
        }
        #endregion

        #region GetMyuser
        [Authorize(Roles = "1,2,3")]
        [HttpPost("GetMyUser")]
        public HttpAPIResponse GetMyUser(int id)
        {
            var _user = _accountService.FetchUser(id).Result;
            _user.Password = "";
            return new HttpAPIResponse()
            {
                Timestamp = DateTime.Now,
                StatusCode = HttpStatusCode.OK,
                Content = JsonConvert.SerializeObject(_user)
            };
        }
        #endregion

        #region ChangePassword
        [Authorize(Roles = "1,2,3")]
        [HttpPost("ChangePassword")]
        public HttpAPIResponse ChangePassword(ChangePassword changePassword)
        {
            if (ComputeSha256Hash(changePassword.OldPass).Equals(_accountService.FetchUser(changePassword.id).Result.Password))
            {
                changePassword.OldPass = ComputeSha256Hash(changePassword.OldPass);
                changePassword.NewPass = ComputeSha256Hash(changePassword.NewPass);
                var status = _accountService.UpdatePassword(changePassword).Result;
                return new HttpAPIResponse()
                {
                    Timestamp = DateTime.Now,
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonConvert.SerializeObject(status)
                };
            }
            else
            {
                return new HttpAPIResponse()
                {
                    Timestamp = DateTime.Now,
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonConvert.SerializeObject(-1)
                };
            }
        }
        #endregion

        #region UpdateUser
        [Authorize(Roles ="1,2,3,")]
        [HttpPost("UpdateUser")]
        public HttpAPIResponse UpdateUser(User usr)
        {
            try
            {
                var status = _accountService.UpdateUser(usr).Result;

                return new HttpAPIResponse()
                {
                    Content = JsonConvert.SerializeObject(status),
                    StatusCode = HttpStatusCode.OK,
                    Timestamp = DateTime.Now
                };
            }
            catch(Exception ex)
            {
                return new HttpAPIResponse()
                {
                    Content = JsonConvert.SerializeObject(-1),
                    StatusCode = HttpStatusCode.OK,
                    Timestamp = DateTime.Now
                };
            }
            
        }
        #endregion
    }


}

