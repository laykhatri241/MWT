using MWTCore.Repository.Interfaces;
using MWTCore.Services.Interfaces;
using MWTDbContext.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MWTCore.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> AddProduct(Product prod)
        {
            return await  _productRepository.CreateProduct(prod);
        }

        public async Task<int> DeleteProduct(int id)
        {
            return await _productRepository.RemoveProduct(id);
        }

        public async Task<int> EditProduct(Product prod)
        {
            return await _productRepository.UpdateProduct(prod);
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _productRepository.SelectProduct(id);
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _productRepository.SelectAllProducts();
        }
    }
}
