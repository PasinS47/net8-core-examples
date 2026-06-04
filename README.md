# NET8 Core API Examples

This repository is created to demonstrate usage of ASP.NET Web API (.NET8) and MySql

## Features
- Basic CRUD functionality with table user
- Documentation via swagger (`dotnet watch run`)
- MySql connection

### Packages
- MySql.EntityFrameworkCore
- Microsoft.EntityFrameworkCore
- DotNetEnv
- Newtonsoft.Json
- Microsoft.AspNetCore.Mvc.NewtonsoftJson

**Make sure to create a .env file as example before running**

## Endpoints

### GET

- /api/accounts
- /api/accounts/{id}
- /api/users
- /api/users/{id}

### POST

- /api/accounts
- /api/users

### PUT

- /api/accounts/{id}
- /api/users/{id}

### DELETE

- /api/accounts/{id}
- /api/users/{id}