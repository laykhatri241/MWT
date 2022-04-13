using MWTDbContext.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MWTCore.Services.Interfaces
{
    public interface IProductService
    {
        public Task<List<Product>> GetProducts();

        public Task<int> AddProduct(Product prod);

        public Task<Product> GetProduct(int id);

        public Task<int> DeleteProduct(int id);

        public Task<int> EditProduct(Product prod);
    }
}
