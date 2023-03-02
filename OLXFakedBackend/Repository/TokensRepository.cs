using System;
using Microsoft.EntityFrameworkCore;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Models;
using OLXFakedBackend.Models.Db;

namespace OLXFakedBackend.Repository
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

