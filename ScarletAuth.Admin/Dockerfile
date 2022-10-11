FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ScarletAuth.Admin/ScarletAuth.Admin.csproj", "src/ScarletAuth.Admin/"]
COPY ["src/ScarletAuth.Admin.EntityFramework.Shared/ScarletAuth.Admin.EntityFramework.Shared.csproj", "src/ScarletAuth.Admin.EntityFramework.Shared/"]
COPY ["src/ScarletAuth.Admin.EntityFramework.SqlServer/ScarletAuth.Admin.EntityFramework.SqlServer.csproj", "src/ScarletAuth.Admin.EntityFramework.SqlServer/"]
COPY ["src/ScarletAuth.Shared/ScarletAuth.Shared.csproj", "src/ScarletAuth.Shared/"]
COPY ["src/ScarletAuth.Admin.EntityFramework.PostgreSQL/ScarletAuth.Admin.EntityFramework.PostgreSQL.csproj", "src/ScarletAuth.Admin.EntityFramework.PostgreSQL/"]
COPY ["src/ScarletAuth.Admin.EntityFramework.MySql/ScarletAuth.Admin.EntityFramework.MySql.csproj", "src/ScarletAuth.Admin.EntityFramework.MySql/"]
RUN dotnet restore "src/ScarletAuth.Admin/ScarletAuth.Admin.csproj"
COPY . .
WORKDIR "/src/src/ScarletAuth.Admin"
RUN dotnet build "ScarletAuth.Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ScarletAuth.Admin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "ScarletAuth.Admin.dll"]