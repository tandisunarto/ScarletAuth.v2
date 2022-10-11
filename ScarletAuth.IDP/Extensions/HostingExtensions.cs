using Duende.IdentityServer;
using ScarletAuth.IDP.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;
using Duende.IdentityServer.EntityFramework.DbContexts;
using ScarletAuth.Admin.EntityFramework.Shared.Entities.Identity;
using ScarletAuth.Admin.EntityFramework.Shared.DbContexts;

namespace ScarletAuth.IDP.Extensions;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var migrationAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
        builder.Services.AddRazorPages();

        builder.Services.AddDbContext<AdminIdentityDbContext>(options =>
            // options.UseSqlite(builder.Configuration.GetConnectionString("IdentityDbConnection")));
            options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityDbConnection")));

        builder.Services.AddIdentity<UserIdentity, UserIdentityRole>()
            .AddEntityFrameworkStores<AdminIdentityDbContext>()
            .AddDefaultTokenProviders();

        builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            // .AddInMemoryIdentityResources(Config.IdentityResources)
            // .AddInMemoryApiScopes(Config.ApiScopes)
            // .AddInMemoryApiResources(Config.ApiResources)
            // .AddInMemoryClients(Config.Clients)
            .AddAspNetIdentity<UserIdentity>()
            .AddConfigurationStore<IdentityServerConfigurationDbContext>(options => {
                options.ConfigureDbContext = config => 
                    config.UseNpgsql(builder.Configuration.GetConnectionString("ConfigurationDbConnection"),
                        option => option.MigrationsAssembly(migrationAssembly));
            })
            .AddOperationalStore<IdentityServerPersistedGrantDbContext>(options => {
                options.ConfigureDbContext = config => 
                    config.UseNpgsql(builder.Configuration.GetConnectionString("PersistedGrantDbConnection"),
                        option => option.MigrationsAssembly(migrationAssembly));
            });
        
        builder.Services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                // register your IdentityServer with Google at https://console.developers.google.com
                // enable the Google+ API
                // set the redirect URI to https://localhost:5001/signin-google
                options.ClientId = "copy client ID from Google here";
                options.ClientSecret = "copy client secret from Google here";
            });

        return builder.Build();
    }
    
    public static WebApplication ConfigurePipeline(this WebApplication app)
    { 
        app.UseSerilogRequestLogging();
    
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();
        
        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }
}