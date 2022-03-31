using System.Reflection;
using Login.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Login.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Login.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddIdentityServerConfiguration(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        var connectionString = configuration.GetConnectionString("Login");
        services.AddDbContext<LoginDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<LoginDbContext>();
        var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

        // configure identity server with in-memory stores, keys, clients and scopes
        services.AddIdentityServer(options =>
            options.UserInteraction = new IdentityServer4.Configuration.UserInteractionOptions()
            {
                LoginUrl = "/Identity/Account/Login",
                LogoutUrl = "/Identity/Account/Logout"
            }
        )

        // add certificate
        .AddCertificate(configuration)

       // this adds the config data from DB (clients, resources)
       .AddConfigurationStore(options =>
       {
           options.ConfigureDbContext = builder =>
               builder.UseSqlite(connectionString,
                   sql => sql.MigrationsAssembly(migrationsAssembly)
           );
       })

       // this adds the operational data from DB (codes, tokens, consents)
       .AddOperationalStore(options =>
       {
           options.ConfigureDbContext = builder =>
               builder.UseSqlite(connectionString,
                   sql => sql.MigrationsAssembly(migrationsAssembly));

           // this enables automatic token cleanup. this is optional.
           options.EnableTokenCleanup = true;
           options.TokenCleanupInterval = 30;
       });
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddTransient<ILogger, Logger>();
    }
}