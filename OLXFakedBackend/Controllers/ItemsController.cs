using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using ChoETL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Models;
using OLXFakedBackend.Models.Api;
using OLXFakedBackend.Models.Api.Authentication.Requests;
using OLXFakedBackend.Models.Api.Product.Requests;
using OLXFakedBackend.Models.Db;
using OLXFakedBackend.Utils;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OLXFakedBackend.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/items")]
    public class ItemsController : Controller
    {
        private IRepositoryWrapper _repositoryWrapper;

        public ItemsController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        //items/categories(GET)
        [Route("categories")]
        [HttpGet]
        public async Task<ActionResult> GetItemCategories()
        {
            List<SuperCategory> superCategories = new List<SuperCategory>();

            foreach(var category in await _repositoryWrapper.CategoryRepository.FindByConditions(new List<System.Linq.Expressions.Expression<Func<Category, bool>>>() { cat => cat.ParentCategoryId == null }))
            {
                superCategories.Add(new SuperCategory { CategoryId = category.CategoryId, Name = category.Name, iconPath=category.CategoryIcon });
            }

            foreach(var superCategory in superCategories)
            {
                superCategory.SubCategories = new List<CategoryApi>();
                foreach(var subCategory in await _repositoryWrapper.CategoryRepository.FindByConditions(new List<System.Linq.Expressions.Expression<Func<Category, bool>>>() { category => category.ParentCategoryId == superCategory.CategoryId })) {
                    superCategory.SubCategories.Add(new CategoryApi { CategoryId = subCategory.CategoryId, Name = subCategory.Name });
                }
            }

            return Ok(superCategories);
        }


        //items(items search endpoint - GET
        [HttpGet]
        public async Task<ActionResult> GetAllItems([FromQuery] ItemsRequest itemsRequest)
        {
            List<ItemApi> items;
            var _paginator = new Paginator<ItemApi>(itemsRequest.pageSize);

            List<System.Linq.Expressions.Expression<Func<ItemApi, bool>>> conditions = new List<System.Linq.Expressions.Expression<Func<ItemApi, bool>>>();

            if (itemsRequest.category != null) conditions.Add(c => c.category == itemsRequest.category);
            if (itemsRequest.cityPart != null) conditions.Add(c => c.city.StartsWith(itemsRequest.cityPart));
            if (itemsRequest.itemKeyword != null) conditions.Add(
                c=>c.name.Contains(itemsRequest.itemKeyword)
                || c.description.Contains(itemsRequest.itemKeyword)
                || c.subject.Contains(itemsRequest.itemKeyword)
                || c.category.Contains(itemsRequest.itemKeyword)
                || c.city.Contains(itemsRequest.itemKeyword)
                || c.district.Contains(itemsRequest.itemKeyword)
                );

            if (conditions.Count > 0) items = await _repositoryWrapper.ItemsViewRepository.FindByConditions(conditions, paginator: _paginator, pageNum: itemsRequest.pageNum);
            else items = await _repositoryWrapper.ItemsViewRepository.FindAll(paginator: _paginator, pageNum: itemsRequest.pageNum);
            
            return Ok(new Items { page = itemsRequest.pageNum, pages = _paginator.GetPagesNumber(), items = items });
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("user/current/items")]
        [HttpGet]
        public async Task<ActionResult> GetItemsByCurrentUser([FromQuery] ItemsRequest itemsRequest) {
            var userid = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (userid == null) return BadRequest("Problems with the authentication. User ID does not exist");
            List<ItemApi> items;
            var userItemIds = await _repositoryWrapper.UserItemRepository.FindByConditions(new List<System.Linq.Expressions.Expression<Func<UserItem, bool>>>() { ui => ui.UserId == userid });
            var itemIds = (from item in userItemIds select item.ItemId).ToList();

            var _paginator = new Paginator<ItemApi>(itemsRequest.pageSize);

            List<System.Linq.Expressions.Expression<Func<ItemApi, bool>>> conditions = new List<System.Linq.Expressions.Expression<Func<ItemApi, bool>>>();

            if (itemsRequest.category != null) conditions.Add(c => c.category == itemsRequest.category);
            if (itemsRequest.cityPart != null) conditions.Add(c => c.city.StartsWith(itemsRequest.cityPart));
            if (itemsRequest.itemKeyword != null) conditions.Add(
                c => c.name.Contains(itemsRequest.itemKeyword)
                || c.description.Contains(itemsRequest.itemKeyword)
                || c.subject.Contains(itemsRequest.itemKeyword)
                || c.category.Contains(itemsRequest.itemKeyword)
                || c.city.Contains(itemsRequest.itemKeyword)
                || c.district.Contains(itemsRequest.itemKeyword)
                );
            conditions.Add(c => itemIds.Contains(c.itemId));

            if (conditions.Count > 0) items = await _repositoryWrapper.ItemsViewRepository.FindByConditions(conditions, paginator: _paginator, pageNum: itemsRequest.pageNum);
            else items = await _repositoryWrapper.ItemsViewRepository.FindAll(paginator: _paginator, pageNum: itemsRequest.pageNum);

            return Ok(new Items { page = itemsRequest.pageNum, pages = _paginator.GetPagesNumber(), items = items });
        }

        //items/{item_id}(GET)
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> GetItemById(int id)
        {
            var result = await _repositoryWrapper.ItemsViewRepository.FindByConditions(new List<System.Linq.Expressions.Expression<Func<ItemApi, bool>>>() { it => it.itemId == id });
            if (result.Count == 0) return Ok(null);
            else if(result.Count > 1) return StatusCode(StatusCodes.Status500InternalServerError, "Server contains more then one item with the same ID");

            return Ok(result[0]);
        }


        //items/add(POST)
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("add")]
        [HttpPost]
        public async Task<ActionResult> AddNewItem([FromBody] ItemAddRequest itemRequest)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (userid == null) return BadRequest("Problems with the authentication. User ID does not exist");

            int categoryId = (await _repositoryWrapper.CategoryRepository.FindByConditions(new List<System.Linq.Expressions.Expression<Func<Category, bool>>>() {
                     c=>c.Name == itemRequest.category
                   }
                ))[0].CategoryId;
            int cityId = (await _repositoryWrapper.CitiesRepository.FindByConditions(new List<System.Linq.Expressions.Expression<Func<CityApi, bool>>>() { c => c.name == itemRequest.contactCity })).FirstOrDefault().cityID;

            Item item = new Item
            {
                Name = itemRequest.name,
                Subject = itemRequest.subject,
                CategoryId = categoryId,
                Description = itemRequest.description,
                AutoContinue = itemRequest.autoContinue,
                ContactData = new ContactData {
                    CityId = cityId,
                    Email = itemRequest.contactEmail,
                    Phone = itemRequest.contactPhone
                }
            };

            await _repositoryWrapper.UserItemRepository.Create(new UserItem { UserId = userid, Item = item });
            await _repositoryWrapper.SaveAsync();

            int itemId = (await _repositoryWrapper.UserItemRepository.FindByConditions(
                 new List<System.Linq.Expressions.Expression<Func<UserItem, bool>>>()
                 {
                     i=>i.UserId == userid,
                     i=>i.Item.CategoryId == categoryId,
                     i=>i.Item.Subject == itemRequest.subject,
                     i=>i.Item.Name == itemRequest.name,
                     i=>i.Item.Description == itemRequest.description,
                     i=>i.Item.AutoContinue == itemRequest.autoContinue,
                     i=>i.Item.ContactData.CityId == cityId,
                     i=>i.Item.ContactData.Email == itemRequest.contactEmail,
                     i=>i.Item.ContactData.Phone == itemRequest.contactPhone
                 }
               )).FirstOrDefault().ItemId;


            foreach (var image in itemRequest.images)
            {
                string serverPath = ImageUtil.GetItemImagesPath(itemId);
                ImageUtil.PlaceImage(image.fileName, serverPath, image.fileName);
                bool isFavorite = true ? itemRequest.images.IndexOf(image) == 0 : false;
                string imgApiPath = ImageUtil.GetItemImageApiPath(itemId, image.fileName);

                await _repositoryWrapper.ImageItemRepository.Create(
                    new ItemImage { ItemId = itemId, Image = new Image { Path = imgApiPath, IsFavorite = isFavorite } }
                    );
            }

            await _repositoryWrapper.SaveAsync();



            return Ok(new { result = "success" });
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[Route("update")]
        //[HttpPatch]
        //public async Task<ActionResult> UpdateItem([FromBody] ItemApi item) {
        //    var userid = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
        //    if (userid == null) return BadRequest("Problems with the authentication. User ID does not exist");
        //    string itemUserId = (await _repositoryWrapper.UserItemRepository.FindByConditions(new List<System.Linq.Expressions.Expression<Func<UserItem, bool>>>() { u=>u.Item.ItemId == item.itemId})).FirstOrDefault().UserId;
        //    if (userid != itemUserId) return BadRequest("This item does not belong to the current user");
        //    await _repositoryWrapper.ItemsViewRepository.Update(item);
        //    await _repositoryWrapper.SaveAsync();
        //    return Ok(item);
        //}

        //items/delete/{item_id} (DELETE)
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("delete/{id}")]
        [HttpDelete]
       public async Task DeleteItem(int id)
        {
            var userid = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            await _repositoryWrapper.ItemRepository.Delete(id: id, userId: userid);
            await _repositoryWrapper.SaveAsync();
        }

    }
}

