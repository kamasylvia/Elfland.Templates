# Introduction
This project is created from Elfland.WebAPI template.

# How to start
1. Install and run your database provider
    - Note: For PostgreSQL, the db name `POSTGRES_DB` can't be `postgres` when we use EF Core.
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

2. Use migrations to create a database. Make sure `dotnet-ef` is already installed.

```sh
$ dotnet ef migrations add InitialMigration -c ApplicationDbContext --verbose
```

3. Run

```sh
$ docker run
```

# Options
```sh
$ dotnet new elfapi -o <project_root_path> [options]
```

| Name      | Description                                                | Arguments                              | Default    |
| --------- | ---------------------------------------------------------- | -------------------------------------- | ---------- |
| `--db`    | Choose a database provider for the project.                | `postgres`, `mssql`, `mysql`, `sqlite` | `postgres` |
| `--https` | Whether to turn on HTTPS.                                  | bool                                   | `false`    |
| `--git`   | Whether to initialize the project as a git repo on create. | bool                                   | `false`    |
| `--ef`    | Whether to install `dotnet-ef`.                        | bool                                   | `false`    |
| `--formatter`    | Whether to install the code formatter `csharpier`.                        | bool                                   | `false`    |