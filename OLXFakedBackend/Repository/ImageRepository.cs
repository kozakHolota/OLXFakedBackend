using System;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Models;

namespace OLXFakedBackend.Repository
{
	public class ImageRepository: RepositoryBase<Image>, IImageRepository
    {
		public ImageRepository(ShopDbContext _shopDbContext): base(_shopDbContext)
		{
		}
	}
}

