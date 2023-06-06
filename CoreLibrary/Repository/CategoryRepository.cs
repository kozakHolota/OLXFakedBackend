using CoreLibrary.Contracts;
using CoreLibrary.DbContexts;
using CoreLibrary.Models.Db;

namespace CoreLibrary.Repository
{
	public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
	{
		public CategoryRepository(ShopDbContext shopDbContext) : base(shopDbContext)
        {
		}
	}
}

