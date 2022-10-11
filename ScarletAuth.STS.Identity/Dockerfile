FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ScarletAuth.STS.Identity/ScarletAuth.STS.Identity.csproj", "src/ScarletAuth.STS.Identity/"]
COPY ["src/ScarletAuth.Admin.EntityFramework.Shared/ScarletAuth.Admin.EntityFramework.Shared.csproj", "src/ScarletAuth.Admin.EntityFramework.Shared/"]
COPY ["src/ScarletAuth.Shared/ScarletAuth.Shared.csproj", "src/ScarletAuth.Shared/"]
RUN dotnet restore "src/ScarletAuth.STS.Identity/ScarletAuth.STS.Identity.csproj"
COPY . .
WORKDIR "/src/src/ScarletAuth.STS.Identity"
RUN dotnet build "ScarletAuth.STS.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ScarletAuth.STS.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "ScarletAuth.STS.Identity.dll"]