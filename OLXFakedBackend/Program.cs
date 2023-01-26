//This is first my comment
using Microsoft.EntityFrameworkCore;
using OLXFakedBackend;
using OLXFakedBackend.Models;

var builder = WebApplication.CreateBuilder(args);

//Initialize MS SQL DB
builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShopDbContext")));

//Add Repository Wrapper Handler
builder.Services.ConfigureRepositoryWrapper();

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

