using System;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Models;
using OLXFakedBackend.Models.Db;

namespace OLXFakedBackend.Repository
{
	public class UserUnitedRepository: RepositoryBase<UserUnited>, IUserUnitedRepository
    {
		public UserUnitedRepository(ShopDbContext _shopDbContext): base(_shopDbContext)
		{
		}
	}
}

