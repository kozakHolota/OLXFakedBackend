using CoreLibrary.Contracts;
using CoreLibrary.DbContexts;
using CoreLibrary.Models.Db;

namespace CoreLibrary.Repository
{
	public class RequisitesRepository: RepositoryBase<Requisites>, IRequisitesRepository
    {
		public RequisitesRepository(ShopDbContext _shopDbContext): base(_shopDbContext)
		{
		}
	}
}

