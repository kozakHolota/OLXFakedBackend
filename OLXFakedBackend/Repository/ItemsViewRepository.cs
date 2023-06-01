using System;
using System.Linq;
using ChoETL;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Models;
using OLXFakedBackend.Models.Api;
using OLXFakedBackend.Models.Api.Utils;
using OLXFakedBackend.Models.Db;
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
                .Include(c => c.Category)
                .Include(co => co.ContactData)
                .Include(cd => cd.ContactData.City)
                .Include(d => d.ContactData.City.District)
                .Select(
                itemsApi => new ItemApi
                {
                    itemId = itemsApi.ItemId,
                    name = itemsApi.Name,
                    subject = itemsApi.Subject,
                    category = itemsApi.Category.Name,
                    description = itemsApi.Description,
                    images = ShopDbContext.ItemImage.Include(i => i.Image).Include(i => i.Item).Where(i => i.Item.ItemId == itemsApi.ItemId).Select(i => new ImageApi { path = i.Image.Path, isFavorite = i.Image.IsFavorite }).AsNoTracking().ToList(),
                    autoContinue = itemsApi.AutoContinue,
                    email = itemsApi.ContactData.Email,
                    phone = itemsApi.ContactData.Phone,
                    city = itemsApi.ContactData.City.Name,
                    district = itemsApi.ContactData.City.District.Name
                }
                ) ;

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

        public async Task Update(ItemApi itemApi) {
            int itemId = itemApi.itemId;
            var item = ShopDbContext.Item.Where(i => i.ItemId == itemId).Select(i => i).FirstOrDefault();
            UserItem userItem = ShopDbContext.UserItem.Where(ui => ui.Item.ItemId == itemId).FirstOrDefault();
            List<ImageApi> currentImagesPaths = ShopDbContext.ItemImage.Where(i => i.Item.ItemId == itemId).Select(i => new ImageApi { path = i.Image.Path, isFavorite = i.Image.IsFavorite }).ToList();
            List<ImageApi> imagesToDelete = itemApi.images.Except(currentImagesPaths).ToList();
            List<ImageApi> imagesToCreate = currentImagesPaths.Except(itemApi.images).ToList();
            foreach (ImageApi img in imagesToDelete) {
                await ShopDbContext.Image.Where(i=>i.Path == img.path).ExecuteDeleteAsync();
            }
            foreach(ImageApi image in imagesToCreate) {
                await ShopDbContext.ItemImage.AddAsync(new ItemImage { Item = item, Image = new Image{Path=image.path, IsFavorite=image.isFavorite}});
            }
            if (item.Category.Name != itemApi.category) item.Category = await ShopDbContext.Category.Where(c => c.Name == itemApi.category).Select(c => c).FirstOrDefaultAsync();
            if (item.ContactData.City.Name != itemApi.city) item.ContactData.City = await ShopDbContext.City.Where(c => c.Name == itemApi.city).Select(c => c).FirstOrDefaultAsync();
            if (item.ContactData.Email != itemApi.email) item.ContactData.Email = itemApi.email;
            if (item.Name != itemApi.name) item.Name = itemApi.name;
            if (item.Description != itemApi.description) item.Description = itemApi.description;
            if (item.Subject != itemApi.subject) item.Subject = itemApi.subject;
            if (item.AutoContinue != itemApi.autoContinue) item.AutoContinue = itemApi.autoContinue;
        }

    }
}

