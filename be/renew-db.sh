dotnet ef database drop --force
dotnet ef migrations remove
dotnet ef migrations add Init
dotnet ef database update
