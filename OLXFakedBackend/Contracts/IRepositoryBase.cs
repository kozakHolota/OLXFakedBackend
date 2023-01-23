using System;
using System.Linq.Expressions;

namespace OLXFakedBackend.Contracts
{
	public interface IRepositoryBase<T>
    {
        ValueTask<List<T>> FindAll();
        ValueTask<List<T>> FindByCondition(Expression<Func<T, bool>> expression);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}

