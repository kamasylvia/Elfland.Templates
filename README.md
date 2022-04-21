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
  - A system logger:
    - `exceptionless`: Exceptionless provides real-time error, feature, and log reporting for your ASP.NET, Web API, WebForms, WPF, Console, and MVC apps. It organizes the gathered information into simple actionable data that will help your app become exceptionless. **Best of all, it’s open source!**
    - `seq`: Seq creates the visibility you need to quickly identify and diagnose problems in complex applications and microservices.
- Elfland.Dapr: Create a microservice project based on Elfland.WebApi and Dapr.

# Building
```sh
dotnet pack
```

# Installation
## Local installation
```sh
dotnet new --install bin/Debug/Elfland.Templates.1.0.0.nupkg
```
## NuGet installation
```sh
dotnet new --install Elfland.Templates
```
