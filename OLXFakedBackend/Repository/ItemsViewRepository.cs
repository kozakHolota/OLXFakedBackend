using System;
using ChoETL;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Models;
using OLXFakedBackend.Models.Api;
using OLXFakedBackend.Utils;

namespace OLXFakedBackend.Repository
{
	public class ItemsViewRepository : RepositoryBase<ItemApi>, IItemsViewRepository
    {
		public ItemsViewRepository(ShopDbContext shopDbContext) : base(shopDbContext)
        {
		}

        public async Task<IQueryable<ItemApi>> GetItemsQuery(List<System.Linq.Expressions.Expression<Func<ItemApi, bool>>> expressions = null)
        {
            var query = ShopDbContext.Item
                .Include(nameof(ShopDbContext.Category))
                .Include(nameof(ShopDbContext.ContactData))
                .Select(
                itemsApi=> new ItemApi
                {
                    itemId = itemsApi.ItemId,
                    name = itemsApi.Name,
                    subject = itemsApi.Subject,
                    category = itemsApi.Category.Name,
                    description = itemsApi.Description,
                    images = ShopDbContext.Set<Image>().AsNoTracking().Select(i=>i.Path).ToList(),
                    autoContinue = itemsApi.AutoContinue,
                    email = itemsApi.ContactData.Email,
                    phone = itemsApi.ContactData.Phone,
                    city = itemsApi.ContactData.City.Name
                }
              );

            if (expressions != null)
            {
                foreach(var expression in expressions)
                {
                    query = query.Where(expression);
                }
            }

            return query;
        }

        public async ValueTask<List<ItemApi>> FindAll(Paginator<ItemApi> paginator = null, int pageNum = 1)
        {
            if (paginator != null) return await paginator.Get(pageNum, (await GetItemsQuery()).AsNoTracking()).ToListAsync();
            return await (await GetItemsQuery()).AsNoTracking().ToListAsync();
        }

        public async ValueTask<List<ItemApi>> FindByConditions(
            List<System.Linq.Expressions.Expression<Func<ItemApi, bool>>> expressions,
            Paginator<ItemApi> paginator = null, int pageNum = 1
            )
        {
            IQueryable<ItemApi> query = await GetItemsQuery(expressions);
            if (paginator != null) query = paginator.Get(pageNum, query);
            return await query.AsNoTracking().ToListAsync();
        }

    }
}

