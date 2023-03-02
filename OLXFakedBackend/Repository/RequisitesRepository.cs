using System;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Models;

namespace OLXFakedBackend.Repository
{
	public class RequisitesRepository: RepositoryBase<Requisites>, IRequisitesRepository
    {
		public RequisitesRepository(ShopDbContext _shopDbContext): base(_shopDbContext)
		{
		}
	}
}

