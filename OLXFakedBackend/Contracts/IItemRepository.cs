using System;
using OLXFakedBackend.Models;
using OLXFakedBackend.Models.Api.Authentication.Requests;

namespace OLXFakedBackend.Contracts
{
	public interface IItemRepository : IRepositoryBase<ItemAddRequestDb>
    {
        Task Delete(int id, string userId);

    }
}

