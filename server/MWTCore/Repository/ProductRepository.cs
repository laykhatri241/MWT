using MWTCore.Repository.Interfaces;
using MWTDbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace MWTCore.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreAppDbCon _context;

        public ProductRepository(StoreAppDbCon context)
        {
            _context = context;
        }

    }
}
