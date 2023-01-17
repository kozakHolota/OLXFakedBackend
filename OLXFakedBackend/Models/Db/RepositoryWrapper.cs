using System;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Models;

namespace OLXFakedBackend.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ShopDbContext _shopDbContext;
        private ICitiesRepository _citiesRepository;

        public ICitiesRepository CitiesRepository
        {
            get
            {
                if (_citiesRepository == null) _citiesRepository = new CitiesRepository(_shopDbContext);

                return _citiesRepository;
            }
        }

        public RepositoryWrapper(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public void Save()
        {
            _shopDbContext.SaveChanges();
        }
    }
}

