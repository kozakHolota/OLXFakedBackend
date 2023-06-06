using System.Linq.Expressions;
using CoreLibrary.Contracts;
using CoreLibrary.DbContexts;
using CoreLibrary.Models.Api.Product.Responses;
using CoreLibrary.Utils;
using Microsoft.EntityFrameworkCore;

namespace CoreLibrary.Repository
{
    public class UserPreferencesRepository : RepositoryBase<UserPreferences>, IUserPreferencesRpository
    {
        public UserPreferencesRepository(ShopDbContext shopDbContext) : base(shopDbContext)
        {
        }

        private IQueryable<UserPreferences> GetUserPreferencesQuery(List<Expression<Func<UserPreferences, bool>>> expression = null)
        {
            var query = ShopDbContext.UserUnited
                .Include(c => c.User).Include(c => c.ContactPerson).Include(c => c.Image).Include(c => c.Requisites).Select(
                s => new UserPreferences
                {
                    UserId = s.UserId,
                    Email = s.User.Email,
                    PhoneNumber = s.User.PhoneNumber,
                    ContactPersonName = s.ContactPerson.Name,
                    ContactCity = s.ContactPerson.City.Name,
                    ImagePath = s.Image.Path,
                    LowName = s.Requisites.LowName,
                    LowAddress = s.Requisites.LowAddress,
                    ZipCode = s.Requisites.ZipCode,
                    RequisitesCity = s.Requisites.City.Name,
                    SingleRegId = s.Requisites.SingleRegId,
                    IsTaxesPayer = s.Requisites.IsTaxesPayer,
                    TaxationId = s.Requisites.TaxationId,
                    RequisitesContactPersonName = s.Requisites.ContactPersonName
                }
               );
            if (expression != null)
            {
                foreach (var condition in expression) query.Where(condition);
            }

            return query;
        }

        public async ValueTask<List<UserPreferences>> FindAll(Paginator<UserPreferences> paginator = null, int pageNum = 1) => await (GetUserPreferencesQuery().AsNoTracking()).ToListAsync();
        public async ValueTask<List<UserPreferences>> FindByConditions(List<System.Linq.Expressions.Expression<Func<UserPreferences, bool>>> expressions, Paginator<UserPreferences> paginator = null, int pageNum = 1) => await (GetUserPreferencesQuery(expressions).AsNoTracking()).ToListAsync();
    }
}

