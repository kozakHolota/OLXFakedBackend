using CoreLibrary.Contracts;
using CoreLibrary.DbContexts;
using CoreLibrary.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace CoreLibrary.Repository
{
    public class TokensRepository : RepositoryBase<RefreshToken>, ITokensRepository
    {
        public DbSet<RefreshToken> RefreshTokens {
            get
            {
                return ShopDbContext.RefreshToken;
            }
        }

        public TokensRepository(ShopDbContext shopDbContext) : base(shopDbContext)
        {
        }
    }
}

