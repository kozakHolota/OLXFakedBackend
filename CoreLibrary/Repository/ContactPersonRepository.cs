using CoreLibrary.Contracts;
using CoreLibrary.DbContexts;
using CoreLibrary.Models.Db;

namespace CoreLibrary.Repository
{
	public class ContactPersonRepository: RepositoryBase<ContactPerson>, IContactPersonRepository
    {
		public ContactPersonRepository(ShopDbContext _shopDbContext): base(_shopDbContext)
		{
		}
	}
}

