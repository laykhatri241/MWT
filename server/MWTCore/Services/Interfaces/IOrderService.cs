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

        public Task<int> UpdateCart(CartItemModel cartItem);

        public Task<CartCheckout> cartCheckout(int CartID);

        public Task<int> PurchaseSuccess(int CartID);

        public Task<List<OrderHistory>> OrderHistory(int UserID);

        public Task<List<CompanyOrderHistory>> OrderPlaced(int SellerID);
    }
}
