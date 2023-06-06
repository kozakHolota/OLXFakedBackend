using System.Linq.Expressions;
using System.Net.Mime;
using CoreLibrary.Contracts;
using CoreLibrary.Models.Api.Product.Requests;
using CoreLibrary.Models.Api.Product.Responses;
using CoreLibrary.Utils;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CitiesService.Controllers
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

