using CoreLibrary.Contracts;
using CoreLibrary.DbContexts;
using Microsoft.AspNetCore.Identity;

namespace CoreLibrary.Repository
{
	public class AspNetUsersRepository : RepositoryBase<IdentityUser>, IAspNetUsersRepository
    {
		public AspNetUsersRepository(ShopDbContext _shopDbContext): base(_shopDbContext)
		{
		}
	}
}

