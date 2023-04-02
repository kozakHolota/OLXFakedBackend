using System;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Models;

namespace OLXFakedBackend.Repository
{
	public class UserItemRepository: RepositoryBase<UserItem>, IUserItemRepository
	{
		public UserItemRepository(ShopDbContext shopDbContext): base(shopDbContext)

        {
		}
	}
}

