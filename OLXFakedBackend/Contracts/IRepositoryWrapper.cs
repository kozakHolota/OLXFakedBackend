using System;
using OLXFakedBackend.Controllers;

namespace OLXFakedBackend.Contracts
{
	public interface IRepositoryWrapper
	{
		ICitiesRepository CitiesRepository { get; }
		ICategoryRepository CategoryRepository { get; }
        IItemsViewRepository ItemsViewRepository { get; }
        ITokensRepository TokensRepository { get; }
        IUserPreferencesRpository UserPreferencesRpository { get; }
        IAspNetUsersRepository AspNetUsersRepository { get; }
        IContactPersonRepository ContactPersonRepository { get; }
        IImageRepository ImageRepository { get; }
        IRequisitesRepository RequisitesRepository { get; }
        ICityRepository CityRepository { get; }
        IUserUnitedRepository UserUnitedRepository { get; }

        Task SaveAsync();
    }
}

