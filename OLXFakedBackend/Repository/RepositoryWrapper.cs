﻿using System;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Models;

namespace OLXFakedBackend.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ShopDbContext _shopDbContext;
        private ICitiesRepository _citiesRepository;
        private ICategoryRepository _categoryRepository;
        private IItemsViewRepository _itemsRepository;

        public ICitiesRepository CitiesRepository
        {
            get
            {
                if (_citiesRepository == null) _citiesRepository = new CitiesRepository(_shopDbContext);

                return _citiesRepository;
            }
        }

        public ICategoryRepository CategoryRepository {
            get {
                if (_categoryRepository == null) _categoryRepository = new CategoryRepository(_shopDbContext);

                return _categoryRepository;
            }
        }

        public IItemsViewRepository ItemsViewRepository {
            get
            {
                if (_itemsRepository == null) _itemsRepository = new ItemsViewRepository(_shopDbContext);

                return _itemsRepository;
            }
        }

        public RepositoryWrapper(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }

        public async Task SaveAsync()
        {
            await _shopDbContext.SaveChangesAsync();
        }
    }
}
