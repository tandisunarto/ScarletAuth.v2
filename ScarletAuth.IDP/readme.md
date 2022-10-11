dotnet ef migrations add CreateUser -o Data/Migrations/User -c ApplicationDbContext
dotnet ef migrations add CreatePersistedGrant -o Data/Migrations/IdentityServer -c PersistedGrantDbContext
dotnet ef migrations add CreateConfiguration -o Data/Migrations/IdentityServer -c ConfigurationDbContext

dotnet ef database update -c ApplicationDbContext
dotnet ef database update -c PersistedGrantDbContext
dotnet ef database update -c ConfigurationDbContext
dotnet run /seed