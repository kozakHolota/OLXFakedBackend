using CitiesService;
using CoreLibrary.Contracts;
using CoreLibrary.DbContexts;
using CoreLibrary.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Initialize MS SQL DB
builder.Services.AddDbContext<ShopDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("ShopDbContext"));
        //options.LogTo(message => new CustomSqlLogger().Log(LogLevel.Information, new EventId(), message, null, (state, _) => state.ToString()), LogLevel.Information);
        options.LogTo(Console.WriteLine, LogLevel.Information);
    }
);

//Add Repository Wrapper Handler
builder.Services.ConfigureRepositoryWrapper();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();