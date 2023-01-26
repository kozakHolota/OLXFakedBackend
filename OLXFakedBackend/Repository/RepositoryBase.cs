using System;
using Microsoft.EntityFrameworkCore;
using OLXFakedBackend.Models;

namespace OLXFakedBackend.Contracts
{
	public abstract class RepositoryBase<T>: IRepositoryBase<T> where T : class
    {
        protected ShopDbContext ShopDbContext { get; set; }

        public RepositoryBase(ShopDbContext shopDbContext)
		{
            ShopDbContext = shopDbContext;
		}

        public async Task Create(T entity) => ShopDbContext.Set<T>().Add(entity);

        public async Task Delete(T entity) => ShopDbContext.Set<T>().Remove(entity);

        public async ValueTask<List<T>> FindAll() => await ShopDbContext.Set<T>().AsNoTracking().ToListAsync();

        public async ValueTask<List<T>> FindByCondition(System.Linq.Expressions.Expression<Func<T, bool>> expression) => await ShopDbContext.Set<T>().Where(expression).AsNoTracking().ToListAsync();

        public async Task Update(T entity) => ShopDbContext.Set<T>().Update(entity);
    }
}

