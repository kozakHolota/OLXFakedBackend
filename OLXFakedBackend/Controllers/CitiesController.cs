using System;
using System.Collections.Generic;
using System.Linq;
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
            List<City> resList;
            var _paginator = new Paginator<City>(pageSize);

            if (namePart.Length > 0)
            {

                resList = await _repositoryWrapper.CitiesRepository.FindByCondition(city => city.Name.StartsWith(namePart)).ToListAsync();

            } else
            {
                resList = await _repositoryWrapper.CitiesRepository.FindAll().ToListAsync();
            }

            return Ok(new Cities { page=pageNum, pages=_paginator.GetPagesNumber(resList), cities = _paginator.Get(resList, pageNum) });
        }
    }
}

