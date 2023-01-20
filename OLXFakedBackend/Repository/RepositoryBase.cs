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

        public async void Create(T entity) => ShopDbContext.Set<T>().Add(entity);

        public async void Delete(T entity) => ShopDbContext.Set<T>().Remove(entity);

        public async Task<IQueryable<T>> FindAll() => ShopDbContext.Set<T>().AsNoTracking();

        public async Task<IQueryable<T>> FindByCondition(System.Linq.Expressions.Expression<Func<T, bool>> expression) => ShopDbContext.Set<T>().Where(expression).AsNoTracking();

        public async void Update(T entity) => ShopDbContext.Set<T>().Update(entity);
    }
}

