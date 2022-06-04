
using Microsoft.EntityFrameworkCore;
using Shopye.Api.Data;
using Shopye.Api.Entities;
using Shopye.Api.Repositories.Contracts;
using Shopye.Models.Dtos;

namespace Shopye.Api.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShopyeDbContext ShopyeDbContext;

        public ShoppingCartRepository(ShopyeDbContext ShopyeDbContext)
        {
            this.ShopyeDbContext = ShopyeDbContext;
        }

        private async Task<bool> CartItemExists(int cartId, int productId)
        {
            return await this.ShopyeDbContext.CartItems.AnyAsync(c => c.CartId == cartId &&
                                                                     c.ProductId == productId);

        }
        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            if (await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId) == false)
            {
                var item = await (from product in this.ShopyeDbContext.Products
                                  where product.Id == cartItemToAddDto.ProductId
                                  select new CartItem
                                  {
                                      CartId = cartItemToAddDto.CartId,
                                      ProductId = product.Id,
                                      Qty = cartItemToAddDto.Qty
                                  }).SingleOrDefaultAsync();

                if (item != null)
                {
                    var result = await this.ShopyeDbContext.CartItems.AddAsync(item);
                    await this.ShopyeDbContext.SaveChangesAsync();
                    return result.Entity;
                }
            }

            return null;

        }

        public async Task<CartItem> DeleteItem(int id)
        {
            var item = await this.ShopyeDbContext.CartItems.FindAsync(id);

            if (item != null)
            {
                this.ShopyeDbContext.CartItems.Remove(item);
                await this.ShopyeDbContext.SaveChangesAsync();
            }
            
            return item;

        }

        public async Task<CartItem> GetItem(int id)
        {
            return await (from cart in this.ShopyeDbContext.Carts
                          join cartItem in this.ShopyeDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cartItem.Id == id
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty,
                              CartId = cartItem.CartId
                          }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {
            return await (from cart in this.ShopyeDbContext.Carts
                          join cartItem in this.ShopyeDbContext.CartItems
                          on cart.Id equals cartItem.CartId
                          where cart.UserId == userId
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Qty = cartItem.Qty,
                              CartId = cartItem.CartId
                          }).ToListAsync();
        }

        public async Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            var item = await this.ShopyeDbContext.CartItems.FindAsync(id);

            if (item != null)
            {
                item.Qty = cartItemQtyUpdateDto.Qty;
                await this.ShopyeDbContext.SaveChangesAsync();
                return item;
            }

            return null;
        }
    }
}
