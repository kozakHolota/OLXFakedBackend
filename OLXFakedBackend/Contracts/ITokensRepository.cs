using System;
using Microsoft.EntityFrameworkCore;
using OLXFakedBackend.Models.Db;

namespace OLXFakedBackend.Contracts
{
	public interface ITokensRepository : IRepositoryBase<RefreshToken>
    {
        public DbSet<RefreshToken> RefreshTokens { get; }

    }
}

