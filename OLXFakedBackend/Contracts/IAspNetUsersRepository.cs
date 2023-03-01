using System;
using Microsoft.AspNetCore.Identity;
using OLXFakedBackend.Models.Db;

namespace OLXFakedBackend.Contracts
{
	public interface IAspNetUsersRepository: IRepositoryBase<IdentityUser>
	{
	}
}

