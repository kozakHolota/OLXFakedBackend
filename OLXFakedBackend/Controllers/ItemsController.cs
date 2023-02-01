using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using ChoETL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Models;
using OLXFakedBackend.Models.Api;
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
                superCategories.Add(new SuperCategory { CategoryId = category.CategoryId, Name = category.Name});
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
        public async Task<ActionResult> GetAllItems(int pageSize = 50, int pageNum = 1, string category=null, string cityPart=null)
        {
            List<ItemApi> items;
            var _paginator = new Paginator<ItemApi>(pageSize);

            List<System.Linq.Expressions.Expression<Func<ItemApi, bool>>> conditions = new List<System.Linq.Expressions.Expression<Func<ItemApi, bool>>>();

            if (category != null) conditions.Add(c => c.category == category);
            if (cityPart != null) conditions.Add(c => c.city.StartsWith(cityPart));

            if (conditions.Count > 0) items = await _repositoryWrapper.ItemsViewRepository.FindByConditions(conditions, paginator: _paginator, pageNum: pageNum);
            else items = await _repositoryWrapper.ItemsViewRepository.FindAll(paginator: _paginator, pageNum: pageNum);
            
            return Ok(new Items { page = pageNum, pages = _paginator.GetPagesNumber(), items = items });
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
        [Route("add")]
        [HttpPost]
        public string AddNewItem()
        {
            return $"item added successfully";
        }

        //items/delete/{item_id} (DELETE)
        [Route("delete/{id}")]
        [HttpDelete]
       public string DeleteItem(int id)
        {
            return $"item #{id} deleted successfully";
        }

    }
}

