using System;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Models;

namespace OLXFakedBackend.Repository
{
	public class CityRepository: RepositoryBase<City>, ICityRepository
    {
		public CityRepository(ShopDbContext _shopDbContext): base(_shopDbContext)
		{
		}
	}
}

