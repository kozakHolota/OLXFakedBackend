using System;
using Microsoft.EntityFrameworkCore;
using OLXFakedBackend.Models;
using OLXFakedBackend.Models.Api;
using OLXFakedBackend.Utils;

namespace OLXFakedBackend.Contracts
{
	public class CitiesRepository: RepositoryBase<CityApi>, ICitiesRepository
    {
		public CitiesRepository(ShopDbContext shopDbContext): base(shopDbContext)
		{
		}

        public async Task<IQueryable<CityApi>> GetCitiesQuery(List<System.Linq.Expressions.Expression<Func<CityApi, bool>>> expressions = null)
        {
            var query = ShopDbContext.City.Include(nameof(ShopDbContext.District)).Select(cd => new CityApi
            {
                cityID = cd.CityId,
                name = cd.Name,
                district = cd.District.Name
            }
          );

            if (expressions != null)
            {
                foreach (var expression in expressions)
                {
                    query = query.Where(expression);
                }
            }

            return query;
        }

        public async ValueTask<List<CityApi>> FindAll(Paginator<CityApi> paginator = null, int pageNum = 1)
		{
            
            if (paginator != null) return await paginator.Get(pageNum, (await GetCitiesQuery()).AsNoTracking()).ToListAsync();
            return await (await GetCitiesQuery()).AsNoTracking().ToListAsync();
        }

        public async ValueTask<List<CityApi>> FindByConditions(
            List<System.Linq.Expressions.Expression<Func<CityApi, bool>>> expressions,
            Paginator<CityApi> paginator = null, int pageNum = 1
            )
        {
            IQueryable<CityApi> query = await GetCitiesQuery(expressions);
            if (paginator != null) query = paginator.Get(pageNum, query);
            return await query.AsNoTracking().ToListAsync();
        }

    }
}

