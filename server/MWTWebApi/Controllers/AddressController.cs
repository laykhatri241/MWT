using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

using Microsoft.AspNetCore.Mvc;
using MWTCore.Models.Custom;
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
    public class AddressController : ControllerBase
    {
        #region DI
        private readonly IAccountService _accountService;
        public AddressController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        #endregion


        #region AddAddress
        [Authorize(Roles = "2,3")]
        [HttpPost("AddAddress")]
        public HttpAPIResponse AddAddress(AddressMaster address)
        {
            var status = _accountService.AddAddress(address).Result;
            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(status),
                StatusCode = HttpStatusCode.OK,
                Timestamp = DateTime.Now
            };
        }
        #endregion

        #region UpdateAddress
        [Authorize(Roles = "2,3")]
        [HttpPost("UpdateAddress")]
        public HttpAPIResponse UpdateAddress(AddressModel address)
        {
            var status = _accountService.UpdateAddress(address).Result;
            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(status),
                StatusCode = HttpStatusCode.OK,
                Timestamp = DateTime.Now
            };
        }
        #endregion

        #region GetAllAddresses
        [Authorize(Roles = "2,3")]
        [HttpGet("GetAllAddress/{id}")]
        public HttpAPIResponse GetAllAddress(int id)
        {
            var addresses = _accountService.GetAddresses(id).Result;

            return new HttpAPIResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonConvert.SerializeObject(addresses)
            };
        }
        #endregion

        #region GetAddress
        [Authorize(Roles = "2,3")]
        [HttpGet("GetMyAddress/{id}")]
        public HttpAPIResponse GetMyAddress(int id)
        {
            var address = _accountService.GetAddress(id).Result;

            return new HttpAPIResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonConvert.SerializeObject(address)
            };
        }
        #endregion

        #region RemoveAddress
        [Authorize(Roles = "2,3")]
        [HttpPost("RemoveAddress")]
        public HttpAPIResponse RemoveAddress([FromBody] int id)
        {
            var status = _accountService.RemoveAddress(id).Result;

            return new HttpAPIResponse()
            {
                Content = status,
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion 
    }
}
