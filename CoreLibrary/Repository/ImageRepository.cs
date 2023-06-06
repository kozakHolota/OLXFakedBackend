using CoreLibrary.Contracts;
using CoreLibrary.DbContexts;
using CoreLibrary.Models.Db;

namespace CoreLibrary.Repository
{
	public class ImageRepository: RepositoryBase<Image>, IImageRepository
    {
		public ImageRepository(ShopDbContext _shopDbContext): base(_shopDbContext)
		{
		}
	}
}

