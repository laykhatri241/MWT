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
        private readonly IProductService _productService;
        private readonly IAuthentication _authentication;

        public MWTController(IAccountService accountService, IProductService productService, IAuthentication authentication)
        {
            _accountService = accountService;
            _productService = productService;
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
        public HttpAPIResponse LogIn(UserLogin usr)
        {
            usr.Password = ComputeSha256Hash(usr.Password);
            var status = _accountService.FindUser(usr).Result;
            if (status != null)
            {
                // TODO: Implement UserRole
                var token = _authentication.AuthenticateData(status.Username, ((int)status.Role));
                status.Password = token;

                return new HttpAPIResponse()
                {
                    Content = JsonConvert.SerializeObject(status),
                    StatusCode = HttpStatusCode.OK
                };
            }
            else
            {
                return new HttpAPIResponse()
                {
                    Content = JsonConvert.SerializeObject(null),
                    StatusCode = HttpStatusCode.OK
                };
            }
        }
        #endregion

        #region AllProduct
        [Authorize(Roles ="1")]
        [HttpGet("AllProduct")]
        public HttpAPIResponse AllProduct()
        {
            var status = _productService.GetProducts().Result;
            if (status != null)
            {
                return new HttpAPIResponse()
                {
                    Content = JsonConvert.SerializeObject(status),
                    StatusCode = HttpStatusCode.OK
                };
            }
            else
            {
                return new HttpAPIResponse()
                {
                    Content = JsonConvert.SerializeObject(""),
                    StatusCode = HttpStatusCode.OK
                };
            }
        }
        #endregion

        #region AddProduct
        [HttpPost("AddProduct")]
        public HttpAPIResponse AddProduct(Product prod)
        {
            var status = _productService.AddProduct(prod).Result;
            if (status == 1)
            {
                return new HttpAPIResponse()
                {
                    Content = JsonConvert.SerializeObject("ProductAdded"),
                    StatusCode = HttpStatusCode.OK
                };
            }
            else
            {
                return new HttpAPIResponse()
                {
                    Content = JsonConvert.SerializeObject("Error Adding Product"),
                    StatusCode = HttpStatusCode.OK
                };
            }
        }
        #endregion

        #region GetProduct
        [HttpPost("GetProduct")]
        public HttpAPIResponse GetProduct([FromBody] int id)
        {
            var product = _productService.GetProduct(id).Result;
            if (product != null)
            {
                return new HttpAPIResponse()
                {
                    Content = JsonConvert.SerializeObject(product),
                    StatusCode = HttpStatusCode.OK
                };
            }
            else
            {
                return new HttpAPIResponse()
                {
                    Content = JsonConvert.SerializeObject(""),
                    StatusCode = HttpStatusCode.OK
                };
            }
        }
        #endregion

        #region DeleteProduct
        [HttpPost("DeleteProduct")]
        public HttpAPIResponse DeleteProduct([FromBody] int id)
        {
            var status = _productService.DeleteProduct(id).Result;
            if (status == 1)
            {
                return new HttpAPIResponse()
                {
                    Content = JsonConvert.SerializeObject("Success"),
                    StatusCode = HttpStatusCode.OK
                };
            }
            else
            {
                return new HttpAPIResponse()
                {
                    Content = JsonConvert.SerializeObject("Failed"),
                    StatusCode = HttpStatusCode.OK
                };
            }
        }
        #endregion

        #region UpdateProduct
        [HttpPost("UpdateProduct")]
        public HttpAPIResponse UpdateProduct([FromBody] Product prod)
        {
            var status = _productService.EditProduct(prod);

            return new HttpAPIResponse()
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonConvert.SerializeObject("Success")
            };
        }
        #endregion

    }
}
