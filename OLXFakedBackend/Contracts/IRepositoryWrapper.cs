using System;
namespace OLXFakedBackend.Contracts
{
	public interface IRepositoryWrapper
	{
		ICitiesRepository CitiesRepository { get; }

		void SaveAsync();
	}
}

