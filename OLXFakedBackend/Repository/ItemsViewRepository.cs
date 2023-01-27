using System;
using Microsoft.EntityFrameworkCore;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Models;
using OLXFakedBackend.Models.Api;

namespace OLXFakedBackend.Repository
{
	public class ItemsViewRepository : RepositoryBase<ItemApi>, IItemsViewRepository
    {
		public ItemsViewRepository(ShopDbContext shopDbContext) : base(shopDbContext)
        {
		}

        public async Task<IQueryable<ItemApi>> GetItemsQuery()
        {
            return  (
                from item in ShopDbContext.Item
                join category in ShopDbContext.Category on item.Category.CategoryId equals category.CategoryId into itemCategory
                from itemCategoryResult in itemCategory.DefaultIfEmpty() 
                join image in ShopDbContext.Image on item.Image.ImageId equals image.ImageId into itemImage
                from itemImageResult in itemImage.DefaultIfEmpty() 
                join contactData in ShopDbContext.ContactData on item.ContactData.ContactDataId equals contactData.ContactDataId into itemContactData
                from itemContactDataResult in itemContactData.DefaultIfEmpty() 
                join city in ShopDbContext.City on itemContactDataResult.City.CityId equals city.CityId into itemCity
                from itemCityResult in itemCity.DefaultIfEmpty()  
                select new ItemApi
                {
                    itemId=item.ItemId,
                    name=item.Name,
                    category= itemCategoryResult.Name,
                    description=item.Description,
                    image= itemImageResult.Path,
                    autoContinue=item.AutoContinue,
                    email= itemContactDataResult.Email,
                    phone=itemContactDataResult.Phone,
                    city= itemCityResult.Name
                }
             );
        }

        public async ValueTask<List<ItemApi>> FindAll() => await (await GetItemsQuery()).AsNoTracking().ToListAsync();


    }
}

