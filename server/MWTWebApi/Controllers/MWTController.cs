using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MWTCore.Services.Interfaces;
using MWTDbContext.Models;
using MWTWebApi.Model;
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
            var response = new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject("WebAPI is working As expected"),
                StatusCode = HttpStatusCode.OK
            };

            return response;
        }
        #endregion

        #region SignUp
        [AllowAnonymous]
        [Route("SignUp")]
        [HttpPost]
        public HttpAPIResponse SignUp(User usr)
        {
            usr.Password = ComputeSha256Hash(usr.Password);
            int status = _accountService.CreateUser(usr).Result;
            if (status == 1)
            {
                return new HttpAPIResponse()
                {
                    Content = JsonConvert.SerializeObject("User Created Successfully"),
                    StatusCode = HttpStatusCode.OK
                };
            }
            else
            {
                return new HttpAPIResponse()
                {
                    Content = JsonConvert.SerializeObject("Error Creating User"),
                    StatusCode = HttpStatusCode.OK
                };
            }
        }
        #endregion

        #region Login
        [AllowAnonymous]
        [HttpPost("LogIn")]
        // TODO: ReImplement Login
        public HttpAPIResponse LogIn(User usr)
        {
            usr.Password = ComputeSha256Hash(usr.Password);
            return new HttpAPIResponse()
            {
                Content = "None"
            };
        }
        #endregion

    }
}
