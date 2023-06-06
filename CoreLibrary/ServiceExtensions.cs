using CoreLibrary.Contracts;
using CoreLibrary.Repository;

namespace CitiesService
{
	public static class ServiceExtensions
	{
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}

