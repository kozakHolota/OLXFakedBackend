namespace CoreLibrary.Contracts
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
        IItemRepository ItemRepository { get; }
        IUserItemRepository UserItemRepository { get; }
        IImageItemRepository ImageItemRepository { get; }

        Task SaveAsync();
    }
}

