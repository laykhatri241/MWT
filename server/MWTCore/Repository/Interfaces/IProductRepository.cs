using MWTDbContext.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MWTCore.Repository.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<Product>> SelectAllProducts();

        public Task<int> CreateProduct(Product prod);

        public Task<Product> SelectProduct(int id);

        public Task<int> RemoveProduct(int id);

        public Task<int> UpdateProduct(Product prod);

    }
}
