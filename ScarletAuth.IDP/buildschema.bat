rmdir /S /Q "Data/Migrations"

dotnet ef migrations add CreateUser -o Data/Migrations/User -c ApplicationDbContext
dotnet ef migrations add CreatePersistedGrant -o Data/Migrations/IdentityServer -c ScarletPersistedGrantDbContext
dotnet ef migrations add CreateConfiguration -o Data/Migrations/IdentityServer -c ScarletConfigurationDbContext
