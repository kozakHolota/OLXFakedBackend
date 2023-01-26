using System;
using OLXFakedBackend.Models;

namespace OLXFakedBackend.Contracts
{
	public class CitiesRepository: RepositoryBase<City>, ICitiesRepository
    {
		public CitiesRepository(ShopDbContext shopDbContext): base(shopDbContext)
		{
		}
	}
}

