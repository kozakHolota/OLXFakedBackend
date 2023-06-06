using CoreLibrary.Models.Api.Product.Responses;

namespace CoreLibrary.Contracts
{
	public interface IItemsViewRepository : IRepositoryBase<ItemApi>
    {
        Task<IQueryable<ItemApi>> GetItemsQuery(List<System.Linq.Expressions.Expression<Func<ItemApi, bool>>> expressions = null);
    }
}

