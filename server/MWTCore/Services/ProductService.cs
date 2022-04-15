using MWTCore.Repository.Interfaces;
using MWTCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWTCore.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
    }
}
