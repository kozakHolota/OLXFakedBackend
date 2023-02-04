using System;
using System.Formats.Asn1;
using System.Text;
using ChoETL;
using Microsoft.EntityFrameworkCore;
using OLXFakedBackend.Models.Db;

namespace OLXFakedBackend.Models
{
	public class ShopDbContext : DbContext
    {
		public DbSet<City> City { get; set; }
        public DbSet<ContactPerson> ContactPerson { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Requisites> Requisites { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ContactData> ContactData { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<ItemImage> ItemImage { get; set; }
        public DbSet<UserItem> UserItem { get; set; }
        public DbSet<ClientIdentity> ClientIdentity { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }

        public ShopDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
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

