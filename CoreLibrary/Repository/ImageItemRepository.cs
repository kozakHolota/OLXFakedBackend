using CoreLibrary.Contracts;
using CoreLibrary.DbContexts;
using CoreLibrary.Models.Db;

namespace CoreLibrary.Repository
{
	public class ImageItemRepository: RepositoryBase<ItemImage>, IImageItemRepository
	{
		public ImageItemRepository(ShopDbContext _shopDbContext): base(_shopDbContext)
		{
		}
	}
}

