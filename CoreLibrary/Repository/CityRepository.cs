using CoreLibrary.Contracts;
using CoreLibrary.DbContexts;
using CoreLibrary.Models.Db;

namespace CoreLibrary.Repository
{
	public class CityRepository: RepositoryBase<City>, ICityRepository
    {
		public CityRepository(ShopDbContext _shopDbContext): base(_shopDbContext)
		{
		}
	}
}

