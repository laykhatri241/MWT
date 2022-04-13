using Microsoft.EntityFrameworkCore;
using MWTCore.Repository.Interfaces;
using MWTDbContext;
using MWTDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWTCore.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreAppDbCon _context;
        public ProductRepository(StoreAppDbCon context)
        {
            _context = context;
        }

        public async Task<int> CreateProduct(Product prod)
        {
            _context.products.Add(prod);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveProduct(int id)
        {
            _context.Remove(await _context.products.FirstAsync(p => p.id == id));
            return await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> SelectAllProducts()
        {
            return await _context.products.ToListAsync();
        }

        public async Task<Product> SelectProduct(int id)
        {
            return await _context.products.FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task<int> UpdateProduct(Product prod)
        {
            _context.Entry(_context.products.FirstAsync(p => p.id == prod.id).Result).CurrentValues.SetValues(prod);
            return await _context.SaveChangesAsync();
        }
    }
}
