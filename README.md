# OLXFakedBackend
Backend for OLX Faked project for IT Step Diploma

# Pre-requisites
To deploy the application we need next things to be installed:
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- Visual Studio 2019 or hihgher
- .NET 7

# Get OLXFakedBackend Docker Environment up
We can get the docker environment up typing in the command terminal from the solution folder:

```
docker compose up -d
```

From the _Visual Studio_ the same thing can be done simply by running the project.

# Create the Database through Entity Framework migrations
After _REST API_ environment is up, we can start to create the _Database_ via _EF Migrations_.
Before doing any migration, please, uncomment line _10_ in _appsettings.json_:

```
//"ShopDbContext": "Server=localhost,1433;Database=ShopDb;User ID=sa;Password=test3210!;TrustServerCertificate=true;MultipleActiveResultSets=true"
```

and comment line _11_:

```
"ShopDbContext": "Server=172.17.0.1,1433;Database=ShopDb;User ID=sa;Password=test3210!;TrustServerCertificate=true;MultipleActiveResultSets=true"
```

After it, type next comands in the terminal inside of solution folder:

```
cd OLXFakedBackend
dotnet ef migrations add <migration name>
dotnet ef database update
```

After migration is done, comment line _10_ in _appsettings.json_ and comment line _11_.