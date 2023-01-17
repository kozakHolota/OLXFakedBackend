using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Models.Api;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OLXFakedBackend.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private IRepositoryWrapper _repositoryWrapper;

        public CitiesController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult> GetAllCities()
        {
            var citiesQuery = await _repositoryWrapper.CitiesRepository.FindAll().ToListAsync();
            return Ok(new Cities { cities = citiesQuery });
        }
    }
}

