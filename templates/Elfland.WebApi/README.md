# Introduction
This project is created from Elfland.WebAPI template.

# How to start
1. Install and run your database provider
    - Note: For PostgreSQL, the db name `POSTGRES_DB` can't be `postgres` EF Core is applied.
      - [DataContext.Database.EnsureDeleted() results in "terminating connection due to administrator command" · Issue #1926 · npgsql/efcore.pg](https://github.com/npgsql/efcore.pg/issues/1926#issuecomment-887031894)

```sh
$ cd <project_root_path>
$ docker run -d \
    --name=PostgreSQL \
    -e POSTGRES_DB=db \
    -e POSTGRES_USER=postgres \
    -e POSTGRES_PASSWORD=password \
    -e TZ="Asia/Shanghai" \
    -e PGTZ="Asia/Shanghai" \
    -p 5432:5432  \
    -v ${PWD}/Database:/var/lib/postgresql/data \
    --restart=always \
    postgres:alpine
```

2. Install and run Exceptionless if specified. (default enabled)
```sh
$ docker run --rm -it \
    --name=Exceptionless \
    -p 10000:80 \
    --restart=always \
    exceptionless/exceptionless:latest

// With data persisted
$ docker run --rm -it -p 10000:80 \
    -v ${PWD}/Exceptionless/data:/usr/share/elasticsearch/data \
    exceptionless/exceptionless:latest
```

3. Browse your Exceptionless host to create your Exceptionless API key, then copy it to `appsettings.json`
```cs
"Serilog" : {
    "WriteTo" : [
        {
            "Name" : "Exceptionless",
            "Args" : {
                "apiKey" : "Your Exceptionless API key",
                "serverUrl" : "Your self-hosted Exceptionless server url"
            }
        }
    ]
}
```

4. Use migrations to create a database. Make sure `dotnet-ef` is already installed.

```sh
$ dotnet ef migrations add InitialMigration -c ApplicationDbContext --verbose
```

5. First time run.

```sh
$ docker run init
// OR
$ docker run seed
```

If failed to connect database, delete `Database` folder (not `Data` folder) then restart your database docker container.

# Options
```sh
$ dotnet new elfapi -o <project_root_path> [options]
```

| Name          | Description                                                | Arguments                              | Default         |
| ------------- | ---------------------------------------------------------- | -------------------------------------- | --------------- |
| `--db`        | Choose a database provider for the project.                | `postgres`, `mssql`, `mysql`, `sqlite` | `postgres`      |
| `--https`     | Whether to turn on HTTPS.                                  | bool                                   | `false`         |
| `--git`       | Whether to initialize the project as a git repo on create. | bool                                   | `false`         |
| `--ef`        | Whether to install `dotnet-ef`.                            | bool                                   | `false`         |
| `--formatter` | Whether to install the code formatter `csharpier`.         | bool                                   | `false`         |
| `--syslog`    | Whether to use Exceptionless.                              | `seq`, `exceptionless`, `none`         | `exceptionless` |

# Arguments
```sh
$ dotnet run <args>
```
| Name   | Description |
| ------ | ----------- |
| `init` | Seed data   |
| `seed` | Seed data   |

# Docker-compose example
```yaml
version: "3.4"

services:
    webapi-db:
        image: postgres:alpine
        container_name: PostgreSQL
        environment:
           POSTGRES_DB: db
           POSTGRES_USER: postgres
           POSTGRES_PASSWORD: password
           TZ: "Asia/Shanghai"
           PGTZ: "Asia/Shanghai"
        volumes:
            - ./PostgreSQL/WebAPI:/var/lib/postgresql/data
        restart: always

    exceptionless:
        image: exceptionless/exceptionless
        container_name: Exceptionless
        ports:
            - 10000:80
        volumes:
            - ./Exceptionless:/usr/share/elasticsearch/data
        restart: always

    webapi:
        image: dockerhub_username/project_name:version
        container_name: ContainerName
        environment:
            - ASPNETCORE_ENVIRONMENT=Production
            - ASPNETCORE_URLS=http://+:80
        ports:
            - 10001:80
        depends_on:
            - webapi-db
            - exceptionless
        links:
            - webapi-db
            - exceptionless
        restart: always
```
