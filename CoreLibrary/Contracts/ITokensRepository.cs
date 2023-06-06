using CoreLibrary.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace CoreLibrary.Contracts
{
	public interface ITokensRepository : IRepositoryBase<RefreshToken>
    {
        public DbSet<RefreshToken> RefreshTokens { get; }

    }
}

