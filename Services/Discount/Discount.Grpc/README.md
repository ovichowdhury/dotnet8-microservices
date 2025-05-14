## Installing dotnet ef globally

```bash
dotnet tool install --global dotnet-ef
```

## Creating a migration in the Discount.Grpc project

```bash
dotnet ef migrations add InitialCreate
```

## Updating the database in the Discount.Grpc project

```bash
dotnet ef database update
```