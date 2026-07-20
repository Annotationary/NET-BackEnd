# Annotationary-API

## Some usefull command for running project


### Creating & Managing Migrations
```shell
dotnet ef migrations add InitialCreate --project .\Jso.Annotationary.Infrastructure\ --startup-project .\Jso.Annotationary.API\
```

### Database Updates & Rollbacks:
```shell
dotnet ef database update --project .\Jso.Annotationary.Infrastructure\ --startup-project .\Jso.Annotationary.API\
```

Delete the entire target database instantly without asking for confirmation
```shell
dotnet ef database drop -f
```

### Production & SQL Generation