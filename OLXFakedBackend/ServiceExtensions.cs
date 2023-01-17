using System;
using OLXFakedBackend.Contracts;
using OLXFakedBackend.Repository;

namespace OLXFakedBackend
{
	public static class ServiceExtensions
	{
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}

