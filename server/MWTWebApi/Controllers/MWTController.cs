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
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MWTWebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("AllowOrigin")]
    [Route("Account")]
    [ApiController]
    public class MWTController : ControllerBase
    {
        #region DI
        private readonly IAccountService _accountService;
        private readonly IAuthentication _authentication;
        private readonly IOrderService _orderService;

        public MWTController(IAccountService accountService, IAuthentication authentication, IOrderService orderService)
        {
            _accountService = accountService;
            _authentication = authentication;
            _orderService = orderService;
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
                int id = _accountService.CreateUser(usr).Result;

                return new HttpAPIResponse()
                {
                    Content = JsonConvert.SerializeObject(id),
                    StatusCode = HttpStatusCode.OK,
                    Timestamp = DateTime.Now
                };
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
        public HttpAPIResponse CheckUsername([FromBody] string usrname)
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
        public HttpAPIResponse Login(LoginModel usr)
        {
            var _user = _accountService.UserExists(usr.Username, usr.Password).Result;
            if (_user != null)
            {
                _user.Password = _authentication.AuthenticateData(_user.Username, _user.Role);
                if (!_orderService.isCartAvailable(_user.id).Result && _user.Role == 3)
                {
                    if (_orderService.AddCart(_user.id).Result == 0)
                    {
                        return new HttpAPIResponse()
                        {
                            Content = JsonConvert.SerializeObject(_user),
                            Timestamp = DateTime.Now,
                            StatusCode = HttpStatusCode.OK,
                            ErrorMessage = "Failed to Create Cart"
                        };
                    }
                }
            }

            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(_user),
                Timestamp = DateTime.Now,
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

        #region GetMyuser
        [Authorize(Roles = "1,2,3")]
        [HttpGet("GetMyUser/{Userid}")]
        public HttpAPIResponse GetMyUser(int Userid)
        {
            var _user = _accountService.FetchUser(Userid).Result;
            _user.Password = "";
            if (_user != null)
            {
                return new HttpAPIResponse()
                {
                    Timestamp = DateTime.Now,
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonConvert.SerializeObject(_user)
                };
            }

            return new HttpAPIResponse()
            {
                Timestamp = DateTime.Now,
                StatusCode = HttpStatusCode.OK,
                Content = JsonConvert.SerializeObject(null)
            };

        }
        #endregion

        #region ChangePassword
        [Authorize(Roles = "1,2,3")]
        [HttpPost("ChangePassword")]
        public HttpAPIResponse ChangePassword(ChangePassword changePassword)
        {
            if (_accountService.CheckOldPassword(changePassword.OldPass, changePassword.id).Result)
            {

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
        [Authorize(Roles = "1,2,3,")]
        [HttpPost("UpdateUser")]
        public HttpAPIResponse UpdateUser()
        {
            var formCollection = Request.ReadFormAsync().Result;
            var user = JsonConvert.DeserializeObject<UpdateUser>(formCollection.First().Value);
            if (formCollection.Files.Any())
            {
                var file = formCollection.Files.First();
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Images", "Avatar"));
                var fileExt = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                var fileName = Guid.NewGuid().ToString("N");

                try
                {
                    using (var stream = new FileStream(Path.Combine(pathToSave, fileName + fileExt), FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                catch (Exception ex)
                {
                    return new HttpAPIResponse()
                    {
                        Content = JsonConvert.SerializeObject(null),
                        StatusCode = HttpStatusCode.OK,
                        Timestamp = DateTime.Now
                    };
                }

                user.Avatar = fileName + fileExt;
            }

            var status = _accountService.UpdateUser(user).Result;

            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(status),
                StatusCode = HttpStatusCode.OK,
                Timestamp = DateTime.Now
            };
        }
        #endregion

        #region GetMyCart
        [Authorize(Roles = "3")]
        [HttpGet("GetMyCart/{Cartid}")]
        public HttpAPIResponse GetMyCart(int Cartid)
        {
            var cart = _orderService.GetCart(Cartid).Result;

            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(cart),
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

        #region UpdateCart
        [Authorize(Roles = "3")]
        [HttpPost("UpdateCart")]
        public HttpAPIResponse UpdateCart(CartItemModel cartItem)
        {
            var status = _orderService.UpdateCart(cartItem).Result;

            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(status),
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

        #region CartCheckout
        [Authorize(Roles = "3")]
        [HttpGet("CartCheckout/{Cartid}")]
        public HttpAPIResponse CartCheckout(int Cartid)
        {
            var checkout = _orderService.cartCheckout(Cartid).Result;

            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(checkout),
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

        #region PaymentSuccess
        [Authorize(Roles = "3")]
        [HttpGet("PaymentSuccess/{Cartid}/{UserID}")]
        public HttpAPIResponse PaymentSuccess(int Cartid, int UserID)
        {
            var status = _orderService.PurchaseSuccess(Cartid).Result;
            if (status > 0)
            {
                var cartstatus = _orderService.AddCart(UserID).Result;
                if (cartstatus == 0)
                {
                    return new HttpAPIResponse()
                    {
                        Content = JsonConvert.SerializeObject(status),
                        StatusCode = HttpStatusCode.OK,
                        ErrorMessage = "FailedToCreateNewCart"
                    };
                }
            }
            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(status),
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

        #region OrderHistory
        [Authorize(Roles = "2,3")]
        [HttpGet("OrderHistory/{UserID}")]
        public HttpAPIResponse OrderHistory(int UserID)
        {
            var orders = _orderService.OrderHistory(UserID).Result;
            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(orders),
                StatusCode = HttpStatusCode.OK,
            };
        }
        #endregion

        #region OrderPlaced
        [Authorize(Roles = "2")]
        [HttpGet("OrderPlaced/{SellerID}")]
        public HttpAPIResponse OrderPlaced(int SellerID)
        {
            var orders = _orderService.OrderPlaced(SellerID).Result;
            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(orders),
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion
    }
}

