using System.Security.Claims;
using IdentityModel;
using ScarletAuth.IDP.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Duende.IdentityServer.EntityFramework.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Duende.IdentityServer.EntityFramework.DbContexts;
using ScarletAuth.Admin.EntityFramework.Shared.Entities.Identity;

namespace ScarletAuth.IDP;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        SeedUsers(app);
        SeedIDP(app);
    }

    private static void SeedUsers(WebApplication app)
    {
        using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.Migrate();

            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<UserIdentity>>();

            foreach(var testUser in TestUsers.Users)
            {
                
                var user = userMgr.FindByNameAsync(testUser.Username).Result;
                if (user is null)
                {
                    user = new UserIdentity
                    {
                        UserName = testUser.Username,
                        Email = testUser.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Email).Value,
                        EmailConfirmed = true,
                    };
                    var result = userMgr.CreateAsync(user, testUser.Password).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    List<Claim> claims = new();
                    foreach(var claim in testUser.Claims)
                    {
                        claims.Add(claim);
                    }
                    result = userMgr.AddClaimsAsync(user, claims).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    Log.Debug($"{user.UserName} created");
                }
                else
                {
                    Log.Debug($"{user.UserName} already exists");
                }
            }
        }
    }

    private static void SeedIDP(WebApplication app)
    {
        using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

            var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
            context.Database.Migrate();
            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients)
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources)
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var resource in Config.ApiScopes)
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in Config.ApiResources)
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }
    }

    public static void EnsureSeedIDPData(IServiceProvider serviceProvider)
    {
        using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            context.Database.Migrate();
            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients)
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources)
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var resource in Config.ApiScopes)
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in Config.ApiResources)
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }
    }
}
