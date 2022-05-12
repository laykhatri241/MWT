using MWTCore.Models.Custom;
using MWTDbContext.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MWTCore.Repository.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<CategoryMaster>> GetCategoryMasters();

        public Task<int> AddCategory(CategoryMaster category);

        public Task<int> AddProduct(ProductMaster product);

        public Task<List<ProductMaster>> RetrieveMyProduct(int id);

        public Task<ProductMaster> RetrieveProduct(int id);


        public Task<bool> UpdateProduct(ProductModel product);

        public Task<bool> RemoveProduct(int id);

        public Task<StockMaster> RetrieveStock(int id);

        public Task<bool> UpdateStock(StockModel stock);

        public Task<List<ProductMaster>> RetrieveByCategory(int categoryID);

        public Task<List<ProductMaster>> RetrieveRandom(int count);

        public Task<List<ProductMaster>> RetrieveRandomByCategory(int count,int categoryID);

        public Task<List<ProductMaster>> GetAllProducts();

        public Task<int> AddEditOffer(OfferMaster offer);

        public Task<OfferMaster> RetrieveOffer(int ProductID);
    }
}
