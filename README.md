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
cd docker-compose && docker compose up -d
```

From the _Visual Studio_ the same thing can be done simply by running the project.

# Create the Database through Entity Framework migrations
After _REST API_ environment is up, we can start to create the _Database_ via _EF Migrations_. To do so, type next comands in the terminal inside of solution folder:

```
cd OLXFakedBackend
dotnet ef migrations add initial
dotnet ef database update
```