using System;
using Microsoft.EntityFrameworkCore;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Models;
using OLXFakedBackend.Models.Api.Authentication.Requests;

namespace OLXFakedBackend.Repository
{
	public class ItemRepository: RepositoryBase<ItemAddRequestDb>, IItemRepository
    {
		public ItemRepository(ShopDbContext _shopDbContext): base(_shopDbContext)
		{
		}

		public async Task Create(ItemAddRequestDb request) {
			int categoryId = ShopDbContext.Category.Where(cat=>cat.Name == request.category).Select(cat => cat.CategoryId).FirstOrDefault();

			await ShopDbContext.UserItem.AddAsync(
				new UserItem {
					Item = new Item
                    {
                        Name = request.name,
                        Subject = request.subject,
                        Description = request.description,
                        Price = request.price,
                        AutoContinue = request.autoContinue,
                        CategoryId = categoryId,
                        ContactData = new ContactData
                        {
                            CityId = ShopDbContext.City.Where(c => c.Name == request.contactCity).Select(c => c.CityId).FirstOrDefault(),
                            Email = request.contactEmail,
                            Phone = request.contactPhone
                        }
                    },
					UserId = request.userId
				}
				);
		}

		public async Task Delete(int id, string userId) {
			var itemToDelete = ShopDbContext.Item.Where(i => i.ItemId == id).Select(i => i).FirstOrDefault();
			var userItemToDelete = ShopDbContext.UserItem.Where(ui => ui.UserId == userId && ui.Item.ItemId == id).FirstOrDefault();

			if (itemToDelete == null || userItemToDelete == null) throw new BadHttpRequestException("Cannot find given item. It does not exist or it does not belong to the current user");

			ShopDbContext.UserItem.Remove(userItemToDelete);
			ShopDbContext.Item.Remove(itemToDelete);
		}
	}
}

