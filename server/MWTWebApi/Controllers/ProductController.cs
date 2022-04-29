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
using System.IO;
using System.Linq;
using System.Net;

namespace MWTWebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors("AllowOrigin")]
    [Route("[controller]")]
    [Authorize(Roles = "1,2,3")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        #region DI
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        #region GetCategorires
        [HttpPost("GetCategories")]
        public HttpAPIResponse GetCategories()
        {
            var categories = _productService.GetCategoryMasters().Result;
            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(categories),
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

        #region AddCategories
        [Authorize(Roles = "1")]
        [HttpPost("AddCategory")]
        public HttpAPIResponse AddCateogry(CategoryMaster category)
        {
            var status = _productService.AddCategory(category).Result;
            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(status),
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

        #region AddProduct
        [Authorize(Roles = "2")]
        [HttpPost("AddProduct")]
        public HttpAPIResponse AddProduct()
        {
            var formCollection = Request.ReadFormAsync().Result;
            var product = JsonConvert.DeserializeObject<ProductMaster>(formCollection.First().Value);

            if (formCollection.Files.Any())
            {
                var file = formCollection.Files.First();
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Images", "Product"));
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
                        Content = null,
                        StatusCode = HttpStatusCode.OK
                    };
                }
                product.ProdImage = fileName + fileExt;
            }

            var status = _productService.AddProduct(product).Result;
            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(status),
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

        #region GetMyProducts
        [Authorize(Roles = "2")]
        [HttpGet("GetMyProducts/{id}")]
        public HttpAPIResponse GetMyProducts(int id)
        {
            var prods = _productService.GetMyProducts(id).Result;
            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(prods),
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

        #region GetProduct
        [HttpGet("GetProduct/{id}")]
        public HttpAPIResponse GetProduct(int id)
        {
            var prods = _productService.GetProduct(id).Result;
            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(prods),
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

        #region EditProduct
        [Authorize(Roles = "2")]
        [HttpPost("UpdateProduct")]
        public HttpAPIResponse UpdateProduct()
        {
            var formCollection = Request.ReadFormAsync().Result;
            var product = JsonConvert.DeserializeObject<ProductModel>(formCollection.First().Value);

            if (formCollection.Files.Any())
            {
                var file = formCollection.Files.First();
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Images", "Product"));
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
                        Content = null,
                        StatusCode = HttpStatusCode.OK
                    };
                }
                product.ProdImage = fileName + fileExt;
            }


            var status = _productService.UpdateProduct(product).Result;
            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(status),
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

        #region RemoveProduct
        [Authorize(Roles = "2")]
        [HttpGet("DeleteProduct/{id}")]
        public HttpAPIResponse DeleteProduct(int id)
        {
            var status = _productService.DeleteProduct(id).Result;
            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(status),
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

        #region AddStock
        [Authorize(Roles = "2")]
        [HttpPost("AddStock")]
        public HttpAPIResponse AddStock(StockMaster stock)
        {
            var status = _productService.AddStock(stock).Result;
            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(status),
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

        #region GetStock
        [Authorize(Roles = "2")]
        [HttpGet("GetStock/{id}")]
        public HttpAPIResponse GetStock(int id)
        {
            var stock = _productService.GetStock(id).Result;
            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(stock),
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

        #region UpdateStock
        [Authorize(Roles = "2")]
        [HttpPost("UpdateStock")]
        public HttpAPIResponse UpdateStock(StockModel stock)
        {
            
            var status = _productService.UpdateStock(stock).Result;
            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(status),
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

        #region GetProductsByCategory
        [Authorize (Roles = "3")]
        [HttpGet("GetProductsByCategory/{id}")]
        public HttpAPIResponse GetProductsByCategory(int id)
        {
            var prodByCategory = _productService.GetProductByCategory(id).Result;

            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(prodByCategory),
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

        #region GetRandomProducts
        [Authorize(Roles = "3")]
        [HttpGet("GetRandomProducts/{id}")]
        public HttpAPIResponse GetRandomProducts(int id)
        {
            var prods = _productService.GetRandomProducts(id).Result;
            return new HttpAPIResponse()
            {
                Content = JsonConvert.SerializeObject(prods),
                StatusCode = HttpStatusCode.OK
            };
        }
        #endregion

    }
}
