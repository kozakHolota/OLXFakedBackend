using System;
namespace OLXFakedBackend.Contracts
{
	public interface IRepositoryWrapper
	{
		ICitiesRepository CitiesRepository { get; }
		ICategoryRepository CategoryRepository { get; }

		Task SaveAsync();
	}
}

