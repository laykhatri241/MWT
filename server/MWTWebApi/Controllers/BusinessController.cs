using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MWTCore.Models.Custom;
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
    [EnableCors("AllowOrigin")]
    [Route("[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        #region DI
        private readonly IAccountService _accountService;
        public BusinessController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        #endregion

        #region AddBusinessDetails
        [Authorize(Roles = "2")]
        [HttpPost("AddBusinessDetails")]
        public HttpAPIResponse AddBusinessDetails(BusinessDetailsMaster businessDetails)
        {
            var status = _accountService.CreateBusinessDetails(businessDetails).Result;

            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(status),
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

        #region IsBusinessDetail
        [Authorize(Roles = "2")]
        [HttpGet("IsBusinessDetail/{BusinessDetailid}")]
        public HttpAPIResponse IsBusinessDetail(int BusinessDetailid)
        {
            var status = _accountService.BusinessDetailsExist(BusinessDetailid).Result;
            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(status),
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

        #region DeleteBusinessDetail
        [Authorize(Roles = "2")]
        [HttpPost("DeleteBusinessDetail")]
        public HttpAPIResponse DeleteBusinessDetail([FromBody] int BusinessDetailid)
        {
            var status = _accountService.DeleteBusinessDetails(BusinessDetailid).Result;

            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(status),
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

        #region GetBusinessDetail
        [Authorize(Roles = "2")]
        [HttpGet("GetBusinessDetail/{BusinessDetailid}")]
        public HttpAPIResponse GetBusinessDetail(int BusinessDetailid)
        {
            var detail = _accountService.GetBusinessDetails(BusinessDetailid).Result;

            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(detail),
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

        #region UpdateBusinessDetail
        [Authorize(Roles = "2")]
        [HttpPost("UpdateBusinessDetail")]
        public HttpAPIResponse UpdateBusinessDetail(BusinessDetailModel businessDetail)
        {
            var status = _accountService.UpdateBusinessDetail(businessDetail).Result;

            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(status),
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion
    }
}
