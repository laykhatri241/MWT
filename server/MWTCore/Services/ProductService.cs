using MWTCore.Models.Custom;
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

        public async Task<int> AddCategory(CategoryMaster category)
        {
            return await _productRepository.AddCategory(category);
        }

        public async Task<int> AddProduct(ProductMaster product)
        {
            return await _productRepository.AddProduct(product);
        }

        public async Task<int> AddStock(StockModel stock)
        {
            return await _productRepository.CreateStock(stock);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            return await _productRepository.RemoveProduct(id);
        }

        public async Task<List<ProductMaster>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }

        public async Task<List<CategoryMaster>> GetCategoryMasters()
        {
            return await _productRepository.GetCategoryMasters();
        }

        public async Task<List<ProductMaster>> GetMyProducts(int id)
        {
            return await _productRepository.RetrieveMyProduct(id);
        }

        public async Task<ProductMaster> GetProduct(int id)
        {
            return await _productRepository.RetrieveProduct(id);
        }

        public async Task<List<ProductMaster>> GetProductByCategory(int CategoryID)
        {
            return await _productRepository.RetrieveByCategory(CategoryID);
        }

        public async Task<List<ProductMaster>> GetRandomByCategory(int count,int categoryID)
        {
            return await _productRepository.RetrieveRandomByCategory(count, categoryID);
        }

        public async Task<List<ProductMaster>> GetRandomProducts(int Count)
        {
            return await _productRepository.RetrieveRandom(Count);
        }

        public async Task<StockMaster> GetStock(int id)
        {
            return await _productRepository.RetrieveStock(id);
        }

        public async Task<bool> UpdateProduct(ProductModel product)
        {
            return await _productRepository.UpdateProduct(product);
        }

        public async Task<bool> UpdateStock(StockModel stock)
        {
            return await _productRepository.UpdateStock(stock);
        }

        public async Task<int> AddEditOffer(OfferMaster offer)
        {
            return await _productRepository.AddEditOffer(offer);
        }

        public async Task<OfferMaster> GetOffer(int ProductID)
        {
            return await _productRepository.RetrieveOffer(ProductID);
        }
    }
}
