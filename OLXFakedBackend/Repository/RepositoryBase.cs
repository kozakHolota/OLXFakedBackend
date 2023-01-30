using System;
using Microsoft.EntityFrameworkCore;
using OLXFakedBackend.Models;
using OLXFakedBackend.Utils;

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

        public async ValueTask<List<T>> FindAll(Paginator<T> paginator = null, int pageNum = 1) {
            if (paginator != null) return await paginator.Get(pageNum, ShopDbContext.Set<T>().AsNoTracking()).ToListAsync();
            return await ShopDbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async ValueTask<List<T>> FindByConditions(List<System.Linq.Expressions.Expression<Func<T, bool>>> expressions, Paginator<T> paginator = null, int pageNum = 1)
        {
            var result = ShopDbContext.Set<T>().AsNoTracking();
            foreach(var e in expressions)
            {
                result = result.Where(e);
            }

            if(paginator != null) return await paginator.Get(pageNum, result).ToListAsync();

            return await result.ToListAsync();

        }

        public async Task Update(T entity) => ShopDbContext.Set<T>().Update(entity);
    }
}

