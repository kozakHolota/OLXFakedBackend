using System;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Controllers;
using OLXFakedBackend.Models;

namespace OLXFakedBackend.Repository
{
	public class ContactPersonRepository: RepositoryBase<ContactPerson>, IContactPersonRepository
    {
		public ContactPersonRepository(ShopDbContext _shopDbContext): base(_shopDbContext)
		{
		}
	}
}

