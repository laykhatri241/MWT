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

        public async Task<int> AddToCart(CartItemModel cartItem)
        {
            var item = await _context.cartItems.FirstOrDefaultAsync(ci => ci.CartID == cartItem.CartID && ci.ProductID == cartItem.ProductID);
            if (item == null)
            {
                item.CartID = cartItem.CartID;
                item.OfferID = cartItem.OfferID;
                item.Count = cartItem.Count;
                item.createdAt = DateTime.Now;
                item.updatedAt = DateTime.Now;
                item.ProductID = cartItem.ProductID;
            }
            else
            {
                if((item.Count + cartItem.Count) < 0)
                {
                    return await RemoveFromCart(cartItem.CartID, cartItem.ProductID);
                }
                item.Count += cartItem.Count;
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<CartCheckout> cartCheckout(int cartID)
        {
            var checkout = new CartCheckout();

            return checkout;

        }

        public async Task<int> createCart(int UserID)
        {
            var cart = new CartMaster();
            cart.UserID = UserID;
            cart.isPaid = false;
            cart.updatedAt = DateTime.Now;
            cart.createdAt = DateTime.Now;
            cart.CartID = await lastCartNumber() + 1;

            _context.cartMasters.Add(cart);
            return await _context.SaveChangesAsync() == 1 ? cart.id : 0;
        }

        public async Task<bool> isCartAvailable(int id)
        {
            return await _context.cartMasters.AsNoTracking().AnyAsync(cm => cm.UserID == id && cm.isPaid == false);
        }

        public async Task<List<OrderHistory>> OrderHistory(int UserID)
        {
            var ReturnHistory = new List<OrderHistory>();
            var orders = await _context.cartMasters.Where(cm => cm.UserID == UserID && cm.isPaid == true).OrderByDescending(cm => cm.updatedAt).ToListAsync();
            foreach (var order in orders)
            {


                var orderItems = await _context.cartItems.Where(ci => ci.CartID == order.CartID).ToListAsync();
                foreach (var orderItem in orderItems)
                {
                    var Order = new OrderHistory();
                    Order.OrderID = order.OrderID;
                    Order.cartID = order.CartID;
                    Order.OrderedOn = order.updatedAt;
                    Order.ProductID = orderItem.ProductID;
                    Order.Count = orderItems.Where(oi => oi.ProductID == orderItem.ProductID).Count();
                    var prod = await _context.productMasters.Where(pm => pm.id == orderItem.ProductID).FirstAsync();
                    Order.Cost = prod.ProdPrice * Order.Count;
                    ReturnHistory.Add(Order);
                }

            }
            return ReturnHistory;
        }

        public async Task<int> PurchaseSuccess(int cartID)
        {
            var checkout = cartCheckout(cartID).Result;

            if (checkout.TotalItems > 0)
            {
                var cartRecord = await _context.cartMasters.FirstAsync(cm => cm.CartID == cartID);

                cartRecord.OrderID = await lastOrderNumber() + 1;
                cartRecord.isPaid = true;
                cartRecord.updatedAt = DateTime.Now;

                foreach (var product in checkout.Products)
                {
                    var stock = _context.stockMasters.Where(sm => sm.ProductID == product.id).FirstOrDefault();
                    if (stock.Stock > 0)
                    {
                        stock.Stock -= checkout.ProductCounts[checkout.Products.FindIndex(fi => fi.id == product.id)];
                    }
                    else
                    {

                        return -1;
                    }
                }

                var updates = await _context.SaveChangesAsync();

                return updates == 1 + checkout.Products.Count ? cartRecord.OrderID : 0;
            }

            else
            {
                return -2;
            }


        }

        public async Task<int> RemoveFromCart(int cartId, int productId)
        {
            _context.cartItems.Remove(new CartItem()
            {
                CartID = cartId,
                ProductID = productId
            });

            return await _context.SaveChangesAsync();
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
