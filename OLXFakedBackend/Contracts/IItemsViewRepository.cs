using System;
using OLXFakedBackend.Models;
using OLXFakedBackend.Models.Api;

namespace OLXFakedBackend.Contracts
{
	public interface IItemsViewRepository : IRepositoryBase<ItemApi>
    {
        Task<IQueryable<ItemApi>> GetItemsQuery();

    }
}

