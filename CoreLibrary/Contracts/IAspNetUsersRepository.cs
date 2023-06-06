using Microsoft.AspNetCore.Identity;

namespace CoreLibrary.Contracts
{
	public interface IAspNetUsersRepository: IRepositoryBase<IdentityUser>
	{
	}
}

