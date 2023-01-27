using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
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

            foreach(var category in await _repositoryWrapper.CategoryRepository.FindByCondition(cat => cat.ParentCategoryId == null))
            {
                superCategories.Add(new SuperCategory { CategoryId = category.CategoryId, Name = category.Name});
            }

            foreach(var superCategory in superCategories)
            {
                superCategory.SubCategories = new List<CategoryApi>();
                foreach(var subCategory in await _repositoryWrapper.CategoryRepository.FindByCondition(category => category.ParentCategoryId == superCategory.CategoryId)) {
                    superCategory.SubCategories.Add(new CategoryApi { CategoryId = subCategory.CategoryId, Name = subCategory.Name });
                }
            }

            return Ok(superCategories);
        }


        //items(items search endpoint - GET
        [HttpGet]
        public async Task<ActionResult> GetAllItems(int pageSize = 50, int pageNum = 1)
        {
            List<ItemApi> items = await _repositoryWrapper.ItemsViewRepository.FindAll();
            var _paginator = new Paginator<ItemApi>(pageSize, items);
            return Ok(new Items { page = pageNum, pages = _paginator.GetPagesNumber(), items = _paginator.Get(pageNum) });
        }

        //items/{item_id}(GET)
        [Route("{id}")]
        [HttpGet]
        public string GetItemById(int id)
        {
            return $"this is item with id# {id}";
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

