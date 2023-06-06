using CoreLibrary.Models.Api.Product.Requests;

namespace CoreLibrary.Contracts
{
	public interface IItemRepository : IRepositoryBase<ItemAddRequestDb>
    {
        Task Delete(int id, string userId);

    }
}

