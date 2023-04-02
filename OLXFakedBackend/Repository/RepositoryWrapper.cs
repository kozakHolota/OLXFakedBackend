using System;
using Microsoft.EntityFrameworkCore;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Controllers;
using OLXFakedBackend.Models;

namespace OLXFakedBackend.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ShopDbContext _shopDbContext;
        private ICitiesRepository _citiesRepository;
        private ICategoryRepository _categoryRepository;
        private IItemsViewRepository _itemsRepository;
        private ITokensRepository _tokensRepository;
        private IUserPreferencesRpository _userPreferencesRpository;
        private IAspNetUsersRepository _aspNetUsersRepository;
        private IContactPersonRepository _contactPersonRepository;
        private IImageRepository _imageRepository;
        private IRequisitesRepository _requisitesRepository;
        private ICityRepository _cityRepository;
        private IUserUnitedRepository _userUnitedRepository;
        private IItemRepository _itemRepository;
        private IUserItemRepository _userItemRepository;
        private IImageItemRepository _imageItemRepository;

        private bool isDbChanged;

        private bool IsDbChanged {
            get {
                if (isDbChanged == null) isDbChanged = false;

                return isDbChanged;
            }
            set { isDbChanged = value; }
        }


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

        public ITokensRepository TokensRepository {
            get
            {
                if (_tokensRepository == null) _tokensRepository = new TokensRepository(_shopDbContext);

                return _tokensRepository;
            }
        }

        public IUserPreferencesRpository UserPreferencesRpository {
            get
            {
                if (_userPreferencesRpository == null) _userPreferencesRpository = new UserPreferencesRepository(_shopDbContext);

                return _userPreferencesRpository;
            }
        }

        public IAspNetUsersRepository AspNetUsersRepository {
            get
            {
                if (_aspNetUsersRepository == null) _aspNetUsersRepository = new AspNetUsersRepository(_shopDbContext);

                return _aspNetUsersRepository;
            }
        }

        public IContactPersonRepository ContactPersonRepository {
            get
            {
                if (_contactPersonRepository == null) _contactPersonRepository = new ContactPersonRepository(_shopDbContext);

                return _contactPersonRepository;
            }
        }

        public IImageRepository ImageRepository {
            get
            {
                if (_imageRepository == null) _imageRepository = new ImageRepository(_shopDbContext);

                return ImageRepository;
            }
        }

        public IRequisitesRepository RequisitesRepository {
            get
            {
                if (_requisitesRepository == null) _requisitesRepository = new RequisitesRepository(_shopDbContext);

                return _requisitesRepository;
            }
        }

        public ICityRepository CityRepository {
            get
            {
                if (_cityRepository == null) _cityRepository = new CityRepository(_shopDbContext);

                return _cityRepository;
            }
        }

        public IUserUnitedRepository UserUnitedRepository {
            get {
                if (_userUnitedRepository == null) _userUnitedRepository = new UserUnitedRepository(_shopDbContext);

                return _userUnitedRepository;
            }
        }

        public IItemRepository ItemRepository {
            get
            {
                if (_itemRepository == null) _itemRepository = new ItemRepository(_shopDbContext);

                return _itemRepository;
            }
        }

        public IUserItemRepository UserItemRepository {
            get {
                if (_userItemRepository == null) _userItemRepository = new UserItemRepository(_shopDbContext);

                return _userItemRepository;
            }
        }

        public IImageItemRepository ImageItemRepository { get {
                if (_imageItemRepository == null) _imageItemRepository = new ImageItemRepository(_shopDbContext);

                return _imageItemRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _shopDbContext.SaveChangesAsync();
        }
    }
}

