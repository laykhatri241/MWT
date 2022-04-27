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
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreAppDbCon _context;
        public OrderRepository(StoreAppDbCon context)
        {
            _context = context;
        }

        public async Task<int> AddToCart(CartItem cartItem)
        {
            cartItem.createdAt = DateTime.Now;
            _context.cartItems.Add(cartItem);
            return await _context.SaveChangesAsync() == 1 ? cartItem.id : 0;
        }

        public async Task<CartCheckout> cartCheckout(int cartID)
        {
            var checkout = new CartCheckout();
            
            var productList = await _context.cartItems.Where(ci => ci.CartID == cartID).ToListAsync();
            
            var prodlist = new List<ProductModel>();
            var prodCount = new List<int>();
            int totalCost = 0;
            for(int i =0;i<productList.Count();i++)
            {
                var prod = await _context.productMasters.Where(pm => pm.id == productList[i].ProductID).FirstAsync();
                var prodModel = new ProductModel() { 
                    id = prod.id,
                    CategoryID = prod.CategoryID,
                    ProdPrice = prod.ProdPrice,
                    ProdCompanyName = prod.ProdCompanyName,
                    ProdDetails = prod.ProdDetails,
                    ProdImage = prod.ProdImage,
                    ProdName = prod.ProdName,
                    SellerID = prod.SellerID
                };
                if(!prodlist.Any(pm=> pm.id == prodModel.id))
                {
                    prodlist.Add(prodModel);
                    prodCount.Add(1);
                }
                else
                {
                    prodCount[prodlist.FindIndex(pl => pl.id == prodModel.id)] += 1;
                }
                
                totalCost += prod.ProdPrice;
                
            }
            checkout.TotalItems = productList.Count();
            checkout.Products = prodlist;
            checkout.ProductCounts = prodCount;
            checkout.TotalCost = totalCost;
            return checkout;

        }

        public async Task<int> createCart(int UserID)
        {
            var cart = new CartMaster();
            cart.UserID = UserID;
            cart.isPaid = false;
            cart.createdAt = DateTime.Now;
            cart.CartID = await lastCartNumber() + 1;

            _context.cartMasters.Add(cart);
            return await _context.SaveChangesAsync() == 1 ? cart.id : 0;
        }

        public async Task<bool> isCartAvailable(int id)
        {
            return await _context.cartMasters.AsNoTracking().AnyAsync(cm => cm.UserID == id && cm.isPaid == false);
        }

        public async Task<bool> PurchaseSuccess(int cartID)
        {
            var checkout =  cartCheckout(cartID).Result;

            var cartRecord = await _context.cartMasters.FirstAsync(cm => cm.CartID == cartID);

            cartRecord.OrderID = await lastOrderNumber() + 1;
            cartRecord.isPaid = true;

            foreach(var product in checkout.Products)
            {
                var stock =  _context.stockMasters.Where(sm => sm.ProductID == product.id).FirstOrDefault();
                stock.Stock -= checkout.ProductCounts[checkout.Products.FindIndex(fi => fi.id == product.id)];
            }

            var updates = await _context.SaveChangesAsync();

            return updates == 1 + checkout.Products.Count ? true : false;
            
        }

        public async Task<CartMaster> RetrieveCart(int UserID)
        {
            return await _context.cartMasters.AsNoTracking().Where(cm => cm.UserID == UserID).OrderByDescending(or => or.CartID).FirstOrDefaultAsync();
        }

        private async Task<int> lastCartNumber()
        {
            var number = await _context.cartMasters.AsNoTracking().OrderByDescending(cm => cm.CartID).FirstOrDefaultAsync();

            return number == null ? 0 : number.CartID;

        }
        private async Task<int> lastOrderNumber()
        {
            var number = await _context.cartMasters.AsNoTracking().OrderByDescending(cm => cm.OrderID).FirstOrDefaultAsync();

            return number == null ? 0 : number.OrderID;

        }
    }
}
