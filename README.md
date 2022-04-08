# Introduction
Elfland.Templates includes
- Elfland.WebApi: Create a WebAPI project with some predefined NuGet references
  - A user defined database provider:
    - `postgres`: PostgreSQL (default)
    - `mssql`: Microsoft SQL Server
    - `mysql`: MySQL
    - `sqlite`: SQLite
  - `Serilog`: Simple .NET logging with fully-structured events.
  - `AutoMapper`: A convention-based object-object mapper.
  - `MediatR`: Simple, unambitious mediator implementation in .NET

# How to start
1. Install Elfland.Templates

```sh
dotnet new --install Elfland.Templates
```

2. New project from the template.

```sh
dotnet new elfwebapi -o <project_name>
```

3. Install and run your database provider

```sh
cd <project_root_path>
docker run -d \
    --name=PostgreSQL \
    -e POSTGRES_DB=db \
    -e POSTGRES_USER=root \
    -e POSTGRES_PASSWORD=123456 \
    -e TZ="Asia/Shanghai" \
    -e PGTZ="Asia/Shanghai" \
    -p 5432:5432  \
    -v ${PWD}/Database:/var/lib/postgresql/data \
    postgres:alpine
```

2. Use migrations to create a database. Make sure `dotnet-ef` is already installedd.

```sh
dotnet ef migrations add InitialMigration -c ApplicationDbContext --verbose
```

3. Run

```sh
docker run
```
