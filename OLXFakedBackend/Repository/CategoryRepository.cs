using System;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Models;

namespace OLXFakedBackend.Repository
{
	public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
	{
		public CategoryRepository(ShopDbContext shopDbContext) : base(shopDbContext)
        {
		}
	}
}

