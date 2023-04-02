using System;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Models;
using OLXFakedBackend.Models.Db;

namespace OLXFakedBackend.Repository
{
	public class ImageItemRepository: RepositoryBase<ItemImage>, IImageItemRepository
	{
		public ImageItemRepository(ShopDbContext _shopDbContext): base(_shopDbContext)
		{
		}
	}
}

