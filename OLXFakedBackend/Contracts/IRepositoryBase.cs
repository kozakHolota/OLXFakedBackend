using System;
using System.Linq.Expressions;
using OLXFakedBackend.Utils;

namespace OLXFakedBackend.Contracts
{
	public interface IRepositoryBase<T>
    {
        ValueTask<List<T>> FindAll(Paginator<T> paginator = null, int pageNum=1);
        ValueTask<List<T>> FindByConditions(List<Expression<Func<T, bool>>> expression, Paginator<T> paginator = null, int pageNum = 1);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}

