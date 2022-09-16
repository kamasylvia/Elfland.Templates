# Introduction
A template for creating a Dapr service.

# Prerequisite
1. [.NET SDK](https://dotnet.microsoft.com/en-us/download)
2. [Docker](https://www.docker.com/get-started/)
3. One of the following database provider:
   1. [PostgreSQL](https://hub.docker.com/_/postgres/)
   2. [MySQL](https://hub.docker.com/_/mysql/)
   3. Microsoft SQL Server
   4. SQLite
4. One of the following system logger.
   1. [Expressionless](https://exceptionless.com/docs/self-hosting/docker/)
   2. [Seq](https://docs.datalust.co/docs/getting-started-with-docker)

# Usage

## Installation
Install the template from nuget.org.
```sh
dotnet new --install Elfland.Templates
```

## New project
```
dotnet new elfdapr -o <path> [options]
```

### Options
| Name             | Description                                                | Arguments                              | Default         |
| ---------------- | ---------------------------------------------------------- | -------------------------------------- | --------------- |
| `init` or `seed` | Seed data after migrations.                                |                                        |
| `--db`           | Choose a database provider for the project.                | `postgres`, `mssql`, `mysql`, `sqlite` | `postgres`      |
| `--https`        | Whether to turn on HTTPS.                                  | bool                                   | `false`         |
| `--git`          | Whether to initialize the project as a git repo on create. | bool                                   | `false`         |
| `--ef`           | Whether to install `dotnet-ef`.                            | bool                                   | `false`         |
| `--formatter`    | Whether to install the code formatter `csharpier`.         | bool                                   | `false`         |
| `--logger`       | Choose a system logger for the project.                    | `seq`, `exceptionless`, `none`         | `exceptionless` |
| `--grpc`         | Whether to use gRPC.                                       | bool                                   | `false`         |
| `--mode`         | To create a Dapr client, server, or both.                  | `client`,`server`,`clientServer`       | `client`        |

## How to run
### Development environment
#### Migrations
```sh
dapr run --app-id <project name> \
   --app-port <port> \
   --components-path <components path> \
   -- dotnet ef migrations add <commit> -c <ApplicationDbContext> -p <project> --verbose
```
#### Run
```sh
dapr run --dapr-http-port 3500 \
   --app-id <project name> \
   --components-path <components path> \
   -- dotnet run --project <project path>
```
#### Watch run
```sh
dapr run --dapr-http-port 3500 \
   --app-id <project name> \
   --app-port <port> \
   --components-path <components path> \
   -- dotnet watch run --project <project path> --no-hot-reload
```

- The default `<components path>` is `dapr/components`. If this folder is moved, the secret file path in `dapr/components/secretStore.yaml` should be modified.

### Production environment
Docker-compose or Kubernetes