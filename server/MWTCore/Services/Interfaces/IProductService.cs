using MWTCore.Models.Custom;
using MWTDbContext.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MWTCore.Services.Interfaces
{
    public interface IProductService
    {
        public Task<List<CategoryMaster>> GetCategoryMasters();

        public Task<int> AddCategory(CategoryMaster category);

        public Task<int> AddProduct(ProductMaster product);

        public Task<List<ProductMaster>> GetMyProducts(int id);

        public Task<ProductMaster> GetProduct(int id);

        public Task<bool> UpdateProduct(ProductModel product);


        public Task<bool> DeleteProduct(int id);

        public Task<StockMaster> GetStock(int id);

        public Task<bool> UpdateStock(StockModel stock);

        public Task<List<ProductMaster>> GetProductByCategory(int CategoryID);

        public Task<List<ProductMaster>> GetRandomProducts(int Count);

        public Task<List<ProductMaster>> GetRandomByCategory(int count,int categoryID);

        public Task<List<ProductMaster>> GetAllProducts();

        public Task<int> AddEditOffer(OfferMaster offer);
        public Task<OfferMaster> GetOffer(int ProductID);
    }
}
