using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OLXFakedBackend.Controllers
{


    [Route("api/[controller]")]
    public class UsersController : Controller
    {

        [Route("register")]
        [HttpPost]
        public string userRegister()
        {
            return "user is successfully registered";
        }

      
        [Route("login")]
        [HttpPost]
        public string userLogin()
        {
            return "user has successfully logged in";
        }

        [HttpGet("{id}")]
        public string GetUserById(int id)
        {
            return $"this user with id# {id}";
        }


    }
}

