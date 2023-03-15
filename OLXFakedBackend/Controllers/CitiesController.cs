using System.Linq.Expressions;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Models.Api;
using OLXFakedBackend.Models.Api.Product.Requests;
using OLXFakedBackend.Utils;

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
        public async Task<ActionResult> GetAllCities([FromQuery] CitiesRequest cities)
        {
            List<CityApi> resList;
            var _paginator = new Paginator<CityApi>(cities.pageSize);

            if (cities.namePart != null && cities.namePart.Length > 0)
            {

                resList = await _repositoryWrapper.CitiesRepository.FindByConditions(new List<Expression<Func<CityApi, bool>>>() { city => city.name.StartsWith(cities.namePart) }, paginator: _paginator, pageNum: cities.pageNum);

            } else
            {
                resList = await _repositoryWrapper.CitiesRepository.FindAll(paginator: _paginator, pageNum: cities.pageNum);
            }

            return Ok(new Cities { page= cities.pageNum, pages=_paginator.GetPagesNumber(), cities = resList });
        }
    }
}

