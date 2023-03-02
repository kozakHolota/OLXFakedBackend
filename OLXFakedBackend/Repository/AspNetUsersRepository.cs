using System;
using Microsoft.AspNetCore.Identity;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Models;
using OLXFakedBackend.Models.Db;

namespace OLXFakedBackend.Repository
{
	public class AspNetUsersRepository : RepositoryBase<IdentityUser>, IAspNetUsersRepository
    {
		public AspNetUsersRepository(ShopDbContext _shopDbContext): base(_shopDbContext)
		{
		}
	}
}

