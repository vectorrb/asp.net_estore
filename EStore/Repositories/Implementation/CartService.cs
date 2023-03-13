using EStore.Models.Domain;
using EStore.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EStore.Models.Domain;

namespace EStore.Repositories.Implementation
{
	public class CartService : ICartService
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly DBContext dbContext;
		private readonly IHttpContextAccessor httpContextAccessor;

		public CartService(UserManager<ApplicationUser> userManager,
			DBContext dbContext, IHttpContextAccessor httpContextAccessor)
		{
			this.userManager = userManager;
			this.dbContext = dbContext;
			this.httpContextAccessor = httpContextAccessor;

		}

		private string GetUserId()
		{
			var principal = httpContextAccessor.HttpContext.User;
			string userId = userManager.GetUserId(principal);
			return userId;
		}

		public async Task<int> AddItem(int bookId, int qty)
		{
			string userId = GetUserId();
			using var transaction = dbContext.Database.BeginTransaction();
			try
			{
				if (string.IsNullOrEmpty(userId))
					throw new Exception("user is not logged-in");
				var cart = await GetCart(userId);
				if (cart is null)
				{
					cart = new ShoppingCart
					{
						UserId = userId
					};
					dbContext.ShoppingCarts.Add(cart);
				}
				dbContext.SaveChanges();
				// cart detail section
				var cartItem = dbContext.CartDetails
								  .FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.BookId == bookId);
				if (cartItem is not null)
				{
					cartItem.Quantity += qty;
				}
				else
				{
					var movie = dbContext.Movies.Find(bookId);
					cartItem = new CartDetail
					{
						BookId = bookId,
						ShoppingCartId = cart.Id,
						Quantity = qty,
						UnitPrice = book.Price  // it is a new line after update
					};
					_db.CartDetails.Add(cartItem);
				}
				_db.SaveChanges();
				transaction.Commit();
			}
			catch (Exception ex)
			{
			}
			var cartItemCount = await GetCartItemCount(userId);
			return cartItemCount;

		}

		public Task<bool> DoCheckout()
		{
			throw new NotImplementedException();
		}

		public Task<ShoppingCart> GetCart(string userId)
		{
			var cart = await dbContext.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
			return cart;
		}

		public Task<int> GetCartItemCount(string userId = "")
		{
			throw new NotImplementedException();
		}

		public Task<ShoppingCart> GetUserCart()
		{
			throw new NotImplementedException();
		}

		public Task<int> RemoveItem(int bookId)
		{
			throw new NotImplementedException();
		}
	}
}
