using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Models;
using OLXFakedBackend.Models.Api;
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
        public async Task<ActionResult> GetAllCities(string namePart="", int pageSize=5, int pageNum=1)
        {
            List<CityApi> resList;
            var _paginator = new Paginator<CityApi>(pageSize);

            if (namePart.Length > 0)
            {

                resList = await _repositoryWrapper.CitiesRepository.FindByConditions(new List<Expression<Func<CityApi, bool>>>() { city => city.name.StartsWith(namePart) }, paginator: _paginator, pageNum: pageNum);

            } else
            {
                resList = await _repositoryWrapper.CitiesRepository.FindAll(paginator: _paginator, pageNum: pageNum);
            }

            return Ok(new Cities { page=pageNum, pages=_paginator.GetPagesNumber(), cities = resList });
        }
    }
}

