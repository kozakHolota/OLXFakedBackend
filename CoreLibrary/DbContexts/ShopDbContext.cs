using ChoETL;
using CoreLibrary.Models.Db;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreLibrary.DbContexts
{
	public class ShopDbContext : IdentityDbContext
    {
		public DbSet<City> City { get; set; }
        public DbSet<ContactPerson> ContactPerson { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Requisites> Requisites { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ContactData> ContactData { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<ItemImage> ItemImage { get; set; }
        public DbSet<UserItem> UserItem { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<IdentityUser>? IdentityUser { get; set; }
        public DbSet<UserUnited> UserUnited { get; set; }

        public List<string> tables;

        public ShopDbContext(DbContextOptions options) : base(options) {
            tables = new List<string>() { "District", "City", "Category", "ContactPerson", "Requisites", "Image", "ContactData", "Item", "ItemImage", "UserUnited", "UserItem" };
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            List<District> districts = new List<District>();
            using (var districtReader = new ChoCSVReader<District>("DeploymentData/UkrainianDistricts.csv").Configure(c => c.IgnoreEmptyLine = true)
                .Configure(c => c.Delimiter = ",")
                .Configure(c => c.IgnoreEmptyLine = true)
                .Configure(c => c.HasExcelSeparator = false).Configure(c => c.QuoteAllFields = true).WithFirstLineHeader())
            {
                foreach (dynamic districtItem in districtReader)
                {
                    districts.Add(districtItem);
                    modelBuilder.Entity<District>().HasData(districtItem);
                }
            }

            using (var cityReader = new ChoCSVReader("DeploymentData/UkrainianCities.csv").Configure(c => c.IgnoreEmptyLine = true)
                .Configure(c => c.Delimiter = ",")
                .Configure(c => c.IgnoreEmptyLine = true)
                .Configure(c => c.HasExcelSeparator = false).Configure(c => c.QuoteAllFields = true).WithFirstLineHeader())
            {
                foreach (dynamic cityItem in cityReader)
                {
                    var district = districts.Select(dItem => dItem).Where(dItem => dItem.Name == cityItem["District"]).First();
                    modelBuilder.Entity<City>().HasData(
                        new City
                        {
                            CityId = int.Parse(cityItem["CityId"]),
                            Name = cityItem["Name"],
                            DistrictId = district.DistrictId
                        }
                     );
                }
            }


            using (var reader = new ChoCSVReader<Category>("DeploymentData/Categories.csv").Configure(c => c.IgnoreEmptyLine = true)
                .Configure(c => c.Delimiter = ",")
                .Configure(c => c.IgnoreEmptyLine = true)
                .Configure(c => c.HasExcelSeparator = false).WithFirstLineHeader().ThrowAndStopOnMissingField(false).Configure(c=> c.QuoteAllFields = true))
            {
                foreach (dynamic item in reader)
                {                   
                    modelBuilder.Entity<Category>().HasData(item);
                }
            }
        }
    }
}

