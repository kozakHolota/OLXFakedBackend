using CoreLibrary.Contracts;
using CoreLibrary.DbContexts;
using CoreLibrary.Models.Db;

namespace CoreLibrary.Repository;

public class UserItemRepository: RepositoryBase<UserItem>, IUserItemRepository
{
	public UserItemRepository(ShopDbContext shopDbContext): base(shopDbContext)

	{
	}
}