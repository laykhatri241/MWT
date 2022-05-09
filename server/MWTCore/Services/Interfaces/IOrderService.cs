using MWTCore.Models.Custom;
using MWTDbContext.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MWTCore.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<bool> isCartAvailable(int id);

        public Task<int> AddCart(int UserID);

        public Task<CartMaster> GetCart(int UserID);

        public Task<int> AddToCart(CartItemModel cartItem,int count);

        public Task<int> RemoveFromCart(int cartId, int productId);

        public Task<CartCheckout> cartCheckout(int CartID);

        public Task<int> PurchaseSuccess(int CartID);
    }
}
