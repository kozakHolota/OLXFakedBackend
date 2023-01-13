using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OLXFakedBackend.Controllers
{

    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        //items/categories(GET)
        [Route("categories")]
        [HttpGet]
        public string GetItemCategories()
        {
            return "this is all item catecories";
        }


        //items(items search endpoint - GET
        [HttpGet]
        public string GetAllItems()
        {
            return "this is all items";
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

