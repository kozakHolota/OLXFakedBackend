using CoreLibrary.Contracts;
using CoreLibrary.DbContexts;
using CoreLibrary.Models.Db;

namespace CoreLibrary.Repository
{
	public class UserUnitedRepository: RepositoryBase<UserUnited>, IUserUnitedRepository
    {
		public UserUnitedRepository(ShopDbContext _shopDbContext): base(_shopDbContext)
		{
		}
	}
}

