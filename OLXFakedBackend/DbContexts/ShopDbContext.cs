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
        public DbSet<Requisites> Requisites { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ContactData> ContactData { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<UserItem> UserItem { get; set; }
        public DbSet<ClientIdentity> ClientIdentity { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }

        public ShopDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            using (var reader = new ChoCSVReader<City>("DeploymentData/UkrainianCities.csv").Configure(c => c.IgnoreEmptyLine = true)
                .Configure(c => c.Delimiter = ",")
                .Configure(c => c.IgnoreEmptyLine = true)
                .Configure(c => c.HasExcelSeparator = false).WithFirstLineHeader()) {
                foreach (dynamic item in reader)
                {
                    modelBuilder.Entity<City>().HasData(item);
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

