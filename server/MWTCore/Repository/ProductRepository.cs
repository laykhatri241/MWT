using Microsoft.EntityFrameworkCore;
using MWTCore.Models.Custom;
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

        public async Task<int> AddCategory(CategoryMaster category)
        {
            _context.categoryMasters.Add(category);
            return await _context.SaveChangesAsync() == 1 ? category.id : 0;
        }

        public async Task<int> AddEditOffer(OfferMaster offer)
        {
            if (!_context.offerMasters.Any(om => om.ProductID == offer.ProductID && om.OfferStart >= offer.OfferStart && om.OfferEnd <= offer.OfferEnd))
            {

                if (offer.id == 0)
                {
                    _context.offerMasters.Add(offer);
                }
                else
                {
                    var offerRecord = await _context.offerMasters.FirstAsync(om => om.id == offer.id);
                    offerRecord.Offer = offer.Offer;
                    offerRecord.OfferStart = offer.OfferStart;
                    offerRecord.OfferEnd = offer.OfferEnd;
                }
                return await _context.SaveChangesAsync() == 1 ? offer.id : 0;
            }
            return -1;

        }

        public async Task<int> AddProduct(ProductMaster product)
        {
            product.updatedAt = DateTime.Now;
            product.createdAt = DateTime.Now;
            _context.productMasters.Add(product);
            var AddProduct = _context.SaveChanges();
            _context.stockMasters.Add(new StockMaster()
            {
                ProductID = product.id,
                Stock = 0,
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now,
            });
            return await _context.SaveChangesAsync() == 1 && AddProduct == 1 ? product.id : 0;
        }

        public async Task<int> CreateStock(StockModel stock)
        {
            var stk = new StockMaster()
            {
                Stock = stock.Stock,
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now
            };

            _context.stockMasters.Add(stk);
            return await _context.SaveChangesAsync() == 1 ? stk.id : 0;
        }

        public async Task<List<ProductMaster>> GetAllProducts()
        {
            return await _context.productMasters.AsNoTracking().ToListAsync();
        }

        public async Task<List<CategoryMaster>> GetCategoryMasters()
        {
            return await _context.categoryMasters.AsNoTracking().ToListAsync();
        }

        public async Task<bool> RemoveProduct(int id)
        {
            var prod = await _context.productMasters.FirstAsync(pm => pm.id == id);
            _context.Remove(prod);
            var stock = await _context.stockMasters.FirstAsync(sm => sm.ProductID == id);
            _context.Remove(stock);
            return await _context.SaveChangesAsync() == 2 ? true : false;
        }

        public async Task<List<ProductMaster>> RetrieveByCategory(int categoryID)
        {
            var prods = await _context.productMasters.Where(pm => pm.CategoryID == categoryID).ToListAsync();
            return prods;
        }

        public async Task<List<ProductMaster>> RetrieveMyProduct(int id)
        {
            var prods = await _context.productMasters.AsNoTracking().Where(pd => pd.SellerID == id).ToListAsync();

            return prods;
        }

        public async Task<OfferMaster> RetrieveOffer(int ProductID)
        {
            var offer = await _context.offerMasters.AsNoTracking().OrderByDescending(om => om.id).FirstOrDefaultAsync(om => om.ProductID == ProductID);
            return offer;
        }

        public async Task<ProductMaster> RetrieveProduct(int id)
        {
            return await _context.productMasters.AsNoTracking().FirstOrDefaultAsync(pd => pd.id == id);
        }

        public async Task<List<ProductMaster>> RetrieveRandom(int count)
        {
            var prodList = new List<ProductMaster>();
            int productTotalCount = _context.productMasters.ToList().Count();
            if (productTotalCount > 1)
            {
                while (prodList.Count < count)
                {
                    var prod = await _context.productMasters.OrderBy(r => Guid.NewGuid()).Take(1).FirstAsync();
                    if (!prodList.Contains(prod))
                    {
                        prodList.Add(prod);

                    }
                    if (count > productTotalCount)
                    {
                        if (prodList.Count() == productTotalCount)
                        {
                            break;
                        }
                    }
                }
            }
            return prodList;
        }

        public async Task<List<ProductMaster>> RetrieveRandomByCategory(int count, int categoryID)
        {
            var prodList = new List<ProductMaster>();
            int productTotalCount = _context.productMasters.AsNoTracking().Where(pm => pm.CategoryID == categoryID).ToList().Count();
            if (productTotalCount > 1)
            {
                while (prodList.Count < count)
                {
                    var prod = await _context.productMasters.OrderBy(r => Guid.NewGuid()).Take(1).FirstAsync();
                    if (!prodList.Contains(prod))
                    {
                        prodList.Add(prod);
                    }
                    if (count > productTotalCount)
                    {
                        if (prodList.Count() == productTotalCount)
                        {
                            break;
                        }
                    }
                }
            }
            return prodList;
        }

        public async Task<StockMaster> RetrieveStock(int id)
        {
            return await _context.stockMasters.FirstAsync(sm => sm.ProductID == id);
        }

        public async Task<bool> UpdateProduct(ProductModel product)
        {
            var prod = await _context.productMasters.FirstOrDefaultAsync(pm => pm.id == product.id);

            prod.ProdDetails = product.ProdDetails;
            prod.ProdName = product.ProdName;
            prod.ProdPrice = product.ProdPrice;
            prod.ProdCompanyName = product.ProdCompanyName;
            prod.CategoryID = product.CategoryID;
            prod.ProdImage = product.ProdImage;
            prod.updatedAt = DateTime.Now;

            return await _context.SaveChangesAsync() == 1 ? true : false;
        }

        public async Task<bool> UpdateStock(StockModel stock)
        {
            var stk = await _context.stockMasters.FirstAsync(sm => sm.id == stock.id);
            stk.Stock = stock.Stock;

            stk.updatedAt = DateTime.Now;

            return await _context.SaveChangesAsync() == 1 ? true : false;

        }
    }
}
